using System;
using System.Net;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using IDESGidp.Models;
using System.Collections.Specialized;
using IdentityServer4.Services;
using Microsoft.Extensions.Primitives;
//using Microsoft.Extensions.Primitives;

namespace IDESGidp.Controllers
{
    public class HomeController : Controller
    {
        private IIdentityServerInteractionService _interaction;

        public HomeController(IIdentityServerInteractionService interaction)
        {
            _interaction = interaction;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Web authentication application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Contact page.";

            return View();
        }

        public async Task<IActionResult> Error(string errorID)
        {
            string sMessage = "No message found";
            var errorContext = await _interaction.GetErrorContextAsync(errorID);
            if (errorContext != null)
            {
                string eResult = errorContext.Error;
                sMessage = errorContext.ErrorDescription;
                return View(new ErrorViewModel { ErrorID = errorContext.Error, ErrorMessage = sMessage });
            }

           string sError = HttpContext.Request.QueryString.ToString();
           var qsParsed = Microsoft.AspNetCore.WebUtilities.QueryHelpers.ParseQuery(sError);
           try
            {
                var qsError = qsParsed["errorID"];
                if (qsError.Count > 0)
                {
                    var decodeContent = System.Net.WebUtility.UrlDecode(qsError[0]);
                    return View(new ErrorViewModel { ErrorID = qsError[0], ErrorMessage = decodeContent });
                }
            }
            finally { };

            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
