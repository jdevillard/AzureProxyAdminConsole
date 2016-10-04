# AzureProxyAdminConsole
Use Application Request Routing available on Azure Web App to create a proxy gateway configurable with an Azure Web App Extensions (kudu)

To use this extensions : 
- clone the repo
- execute build.cmd

This will create a Artifacts/Release folder with 2 components : 
- proxyadminconsole.1.0.0.nupkg
- a zip folder with the content of the extension and a cmd to deploy the extension on your website.


Once the extensions is deployed, you can take a look at the Extension manager in the Kudu Administration console

![alt HelpImage](https://jeremiedevillard.files.wordpress.com/2016/10/image_thumb1.png?w=1731&h=729)

This will route you to yoursite.scm.azurewebsites.net/ProxyAdminConsole

This sub site is just an MVC application that provide a list of the redirection and a form to create new redirection  :

![alt HelpImage](https://jeremiedevillard.files.wordpress.com/2016/10/image_thumb2.png?w=1224&h=759)

This will use the Microsoft.Web.Administration library to edit & configure the web.config of the site.

For example, here is how I get the different rewrite rule to provide a list of configured rules:

private static string webSiteName = Environment.ExpandEnvironmentVariables("%APPSETTING_WEBSITE_SITE_NAME%");

```csharp
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

```

[link](https://jeremiedevillard.wordpress.com/2016/10/04/azure-site-extensions-application-request-routing/#more-1364) to my blog post