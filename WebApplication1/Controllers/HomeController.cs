using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebApplication1.Data;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _db;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext db)
        {
            _logger = logger;
            _db = db;
        }

        public void RecordClick(string str1,string str2)
        {
            ClickInfo obj = new ClickInfo { count = 1, TrackName = str1, TrackURL = str2 };
            _db.ClickInfo.Add(obj);
            _db.SaveChanges();
        }

        public IActionResult ClickInfo()
        {
            List<ClickInfo> objList = _db.ClickInfo.GroupBy(o => new { o.TrackName, o.TrackURL })
                .Select(g => new ClickInfo
                {
                    TrackName = g.Key.TrackName,
                    count = g.Count(),
                    TrackURL = g.Key.TrackURL
                }).ToList();
            return View(objList);
        }

        public IActionResult Books()
        {
            return View();
        }

        public IActionResult Movies()
        {
            return View();
        }

        public IActionResult MusicVideo()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
