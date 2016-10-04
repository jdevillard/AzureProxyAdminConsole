using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProxyAdminConsole.Models
{
    /*
      <rule name="Proxy" stopProcessing="true">
            <match url="(.*)" />
            <conditions>
              <add input="{HTTP_HOST}" pattern="jdev.mycrosscut.net" />
            </conditions>
            <action type="Rewrite" url="https://crosscut-jdev.azurewebsites.net/{R:1}" />
            <serverVariables>
              <set name="HTTP_X_UNPROXIED_URL" value="https://jdev-testARR.azurewebsites.net/{R:1}" />
              <set name="HTTP_X_ORIGINAL_ACCEPT_ENCODING" value="{HTTP_ACCEPT_ENCODING}" />
              <set name="HTTP_X_ORIGINAL_HOST" value="{HTTP_HOST}" />
              <set name="HTTP_ACCEPT_ENCODING" value="" />
            </serverVariables>
          </rule>
    */
    

    public class ConfigModel
    {
        public String RuleName { get; set; }
        public String Pattern { get; set; }

        public String UrlRewrite { get; set; }

    }
}