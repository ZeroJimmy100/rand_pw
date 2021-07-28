using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RandomPasscode.Models;
using Microsoft.AspNetCore.Http;

namespace RandomPasscode.Controllers
{
    public class HomeController : Controller
    {
        private static Random rand = new Random();
        public static string RandomString()
        {
            const string characters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(characters, 14).Select(s => s[rand.Next(s.Length)]).ToArray());
        }

        [HttpGet("")]
        public IActionResult Index(int count, string passcode)
        {
            HttpContext.Session.SetInt32("generate", count);
            int ? generate = HttpContext.Session.GetInt32("generate");
            ViewBag.num = generate;
            ViewBag.pass = passcode;
            return View();
        }

        [HttpGet("new")]
        public IActionResult Generate()
        {
            int ? count = HttpContext.Session.GetInt32("generate");
            count ++;
            string pass = RandomString();
            return RedirectToAction("Index", new {count, passcode = pass});
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
