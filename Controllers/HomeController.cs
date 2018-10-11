using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using QuotingDojo.Models;
using DbConnection;

namespace QuotingDojo.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        [Route("")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Route("quotes")]
        public IActionResult Quotes()
        {
            List<Dictionary<string, object>> AllQuotes = DbConnector.Query("SELECT * FROM quotes");

            ViewBag.quotes = AllQuotes;

            return View("quotes");
        }

        [HttpPost]
        [Route("quote")]
        public IActionResult addQuotes(string name, string quote)
        {
            string sql = $"INSERT INTO quotes(Name, Quote, Created_At, Updated_At) VALUES('{name}', '{quote}', NOW(), NOW())";
            DbConnector.Execute(sql);
            List<Dictionary<string, object>> AllQuotes = DbConnector.Query("SELECT * FROM quotes");
            ViewBag.quotes = AllQuotes;
            return View("quotes");
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
