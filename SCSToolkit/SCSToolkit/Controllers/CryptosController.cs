using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Microsoft.Ajax.Utilities;
using SCSToolkit.Domains.Extensions;
using SCSToolkit.Models;

namespace SCSToolkit.Controllers
{
    public class CryptosController : Controller
    {
        // GET: Cryptos
        public ActionResult Index(CryptoViewModel cvm = null)
        {
            if (cvm == null)
            {
                cvm = new CryptoViewModel();
            }
            if (cvm?.PlainText.IsNullOrWhiteSpace() == false)
            {
                cvm.EncodedText = cvm.PlainText.ToEncodedBase64UrlSafe();
            }
            else
            {
                if (cvm?.EncodedText.IsNullOrWhiteSpace() == false)
                {
                    cvm.PlainText = cvm.EncodedText.ToPlainText();
                }
            }
            return View(cvm);
        }
    }
}