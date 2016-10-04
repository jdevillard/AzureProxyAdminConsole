using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.Ajax.Utilities;
using Microsoft.Web.Administration;
using ProxyAdminConsole.Models;
using WebGrease.Css.Extensions;

namespace ProxyAdminConsole.Controllers
{
    public class ConfigureController : Controller
    {
        private static string webSiteName = Environment.ExpandEnvironmentVariables("%APPSETTING_WEBSITE_SITE_NAME%");
        
        // GET: Configure
        public ActionResult Index()
        {
            var list = new List<ConfigModel>();

            using (var serverManager = new ServerManager(Environment.ExpandEnvironmentVariables(@"%APP_POOL_CONFIG%")))
            {
                var path = @"Web.config";

                var sites = serverManager.Sites;


                var webConfig = serverManager.GetWebConfiguration(webSiteName);
                string rulesSection = "rules";
                var inboundRulesCollection =
                    webConfig.GetSection("system.webServer/rewrite/" + rulesSection).GetCollection();
                var outboundRulesSection = webConfig.GetSection("system.webServer/rewrite/outboundRules");
                var outboundRulesCollection = outboundRulesSection.GetCollection();
                var preConditionsCollection = outboundRulesSection.GetCollection("preConditions");

                inboundRulesCollection.ForEach(
                    r =>
                    {
                        var conditions = r.GetCollection("conditions");
                        var condition = conditions.FirstOrDefault(c => c.GetAttribute("input").Value.ToString() == "{HTTP_HOST}");


                        list.Add(new ConfigModel()
                        {
                            RuleName = r.GetAttribute("name").Value.ToString(),
                            Pattern = condition != null ? condition.GetAttribute("pattern").Value.ToString() : string.Empty,

                        });
                    })
                    ;

            }
            return View(list);
        }

      
        public ActionResult Delete(String id)
        {
            using (var serverManager = new ServerManager(Environment.ExpandEnvironmentVariables(@"%APP_POOL_CONFIG%")))
            {
                var path = @"Web.config";

                var sites = serverManager.Sites;
                
                var webConfig = serverManager.GetWebConfiguration(webSiteName);
                string rulesSection = "rules";
                var inboundRulesCollection =
                    webConfig.GetSection("system.webServer/rewrite/" + rulesSection).GetCollection();

                var ruleToDelete = inboundRulesCollection.FirstOrDefault(i => i.GetAttribute("name").Value.ToString() == id);
                if(ruleToDelete != null)
                    inboundRulesCollection.Remove(ruleToDelete);

                serverManager.CommitChanges();
            }
            
            return RedirectToAction("Index");
        }

        public ActionResult Detail(string id)
        {
            var model = GetConfigModelFromId(id);

            return View(model);
        }

        private static ConfigModel GetConfigModelFromId(string id)
        {
            ConfigModel model;
            using (var serverManager = new ServerManager(Environment.ExpandEnvironmentVariables(@"%APP_POOL_CONFIG%")))
            {
                var path = @"Web.config";

                var sites = serverManager.Sites;


                var webConfig = serverManager.GetWebConfiguration(webSiteName);
                string rulesSection = "rules";
                var inboundRulesCollection =
                    webConfig.GetSection("system.webServer/rewrite/" + rulesSection).GetCollection();
                var outboundRulesSection = webConfig.GetSection("system.webServer/rewrite/outboundRules");
                var outboundRulesCollection = outboundRulesSection.GetCollection();
                var preConditionsCollection = outboundRulesSection.GetCollection("preConditions");

                var rule = inboundRulesCollection.First(
                    r => r.GetAttribute("name").Value.ToString() == id);


                var conditions = rule.GetCollection("conditions");
                var condition = conditions.FirstOrDefault(c => c.GetAttribute("input").Value.ToString() == "{HTTP_HOST}");

                var action = rule.GetChildElement("action");

                model = new ConfigModel()
                {
                    Pattern = condition != null ? condition.GetAttribute("pattern").Value.ToString() : string.Empty,
                    RuleName = id,
                    UrlRewrite = action.GetAttributeValue("url").ToString()
                };
            }
            return model;
        }


        public ActionResult Edit(String id)
        {
            var model = GetConfigModelFromId(id);
            return View(model);
        }


        [HttpPost]
        public ActionResult Edit(ConfigModel model)
        {
            using (var serverManager = new ServerManager(Environment.ExpandEnvironmentVariables(@"%APP_POOL_CONFIG%")))
            {
                var path = @"Web.config";

                var sites = serverManager.Sites;


                var webConfig = serverManager.GetWebConfiguration(webSiteName);
                string rulesSection = "rules";
                var inboundRulesCollection =
                    webConfig.GetSection("system.webServer/rewrite/" + rulesSection).GetCollection();
                var outboundRulesSection = webConfig.GetSection("system.webServer/rewrite/outboundRules");
                var outboundRulesCollection = outboundRulesSection.GetCollection();
                var preConditionsCollection = outboundRulesSection.GetCollection("preConditions");

                var rule = inboundRulesCollection.First(
                    r => r.GetAttribute("name").Value.ToString() == model.RuleName);


                var conditions = rule.GetCollection("conditions");
                var condition = conditions.FirstOrDefault(c => c.GetAttribute("input").Value.ToString() == "{HTTP_HOST}");

                var action = rule.GetChildElement("action");
                action.GetAttribute("url").Value = model.UrlRewrite;

                condition.GetAttribute("pattern").Value = model.Pattern;

                serverManager.CommitChanges();
            }
            return View();
        }

        public ActionResult Create()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Create(ConfigModel model)
        {
            using (var serverManager = new ServerManager(Environment.ExpandEnvironmentVariables(@"%APP_POOL_CONFIG%")))
            {
                var path = @"Web.config";

                var sites = serverManager.Sites;


                var webConfig = serverManager.GetWebConfiguration(webSiteName);
                string rulesSection = "rules";
                var inboundRulesCollection =
                    webConfig.GetSection("system.webServer/rewrite/" + rulesSection).GetCollection();
                var outboundRulesSection = webConfig.GetSection("system.webServer/rewrite/outboundRules");
                var outbBoundRulesCollection = outboundRulesSection.GetCollection();
                var preConditionsCollection = outboundRulesSection.GetCollection("preConditions");

                inboundRulesCollection.AddAt(inboundRulesCollection.Count - 1,
                CreateInboundGwRemoteUserRule(model, inboundRulesCollection.CreateElement("rule")));
                

                serverManager.CommitChanges();
            }


            return RedirectToAction("Index");
        }


        private static ConfigurationElement CreateInboundGwRemoteUserRule(ConfigModel model, ConfigurationElement ruleElement)
        { 
            ruleElement["name"] = model.RuleName;
            ruleElement["patternSyntax"] = "ECMAScript";
            ruleElement["stopProcessing"] = true;

            var matchElement = ruleElement.GetChildElement("match");
            matchElement["url"] = "(.*)";

            var conditionsElement = ruleElement.GetChildElement("conditions");
          
            var conditionsCollection = conditionsElement.GetCollection();

          
            //Get the origin requested : 
            var inputElement = conditionsCollection.CreateElement("add");
            inputElement["input"] = "{" + ServerVariable.SERVER_VARIABLE_HTTP_HOST + "}";
            inputElement["pattern"] = model.Pattern;
            conditionsCollection.Add(inputElement);

            string arrServer = Environment.ExpandEnvironmentVariables(@"%APPSETTING_WEBSITE_SITE_NAME%");
            // set the server variable
            var variablesCollection = ruleElement.GetChildElement("serverVariables").GetCollection();
            AddServerVariable(variablesCollection, ServerVariable.SERVER_VARIABLE_HTTP_X_UNPROXIED_URL, arrServer + "{R:1}");
            AddServerVariable(variablesCollection, ServerVariable.SERVER_VARIABLE_HTTP_X_ORIGINAL_ACCEPT_ENCODING, "{" + ServerVariable.SERVER_VARIABLE_ACCEPT_ENCODING + "}");
            AddServerVariable(variablesCollection, ServerVariable.SERVER_VARIABLE_HTTP_X_ORIGINAL_HOST, "{" + ServerVariable.SERVER_VARIABLE_HTTP_HOST + "}");
            AddServerVariable(variablesCollection, ServerVariable.SERVER_VARIABLE_ACCEPT_ENCODING, "");
            AddServerVariable(variablesCollection, ServerVariable.SERVER_VARIABLE_HTTP_REFERER, model.UrlRewrite + "/{R:1}");

            var actionElement = ruleElement.GetChildElement("action");
            actionElement["type"] = "Rewrite";
            actionElement["url"] = model.UrlRewrite+"/{R:1}";

            return ruleElement;
        }
        private static void AddServerVariable(ConfigurationElementCollection variablesCollection, string name,
          string value)
        {
            var variableElement = variablesCollection.CreateElement("set");
            variableElement["name"] = name;
            variableElement["value"] = value;
            variablesCollection.Add(variableElement);
        }

        private static string FormatUrl(string url)
        {
            if (url.EndsWith("/"))
                return url.Substring(0, url.Length - 1);
            return url;

        }
    }
}