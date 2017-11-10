using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebCLI.Core.Client
{
    public class ClientResourceManager
    {
        #region Fields

        public const string WebCLI_js = "WebCLI.Core.Client.js.webCLI.js";
        public const string WebCLI_css = "WebCLI.Core.Client.css.webCLI.css";
        public const string WebCLI_html = "WebCLI.Core.Client.html.webCLI.html";

        #endregion

        #region Properties

        public Dictionary<string, string> Resources { get; set; }

        #endregion

        #region Methods

        public ClientResourceManager()
        {
            Resources = new Dictionary<string, string>()
            {
                { WebCLI_js, "/js/webCLI.js" },
                { WebCLI_css, "/css/webCLI.css" },
                { WebCLI_html, "/webCLI.html" }
            };
        }

        #endregion

    }
}
