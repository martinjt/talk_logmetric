using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using logmetric.Models;
using App.Metrics;
using App.Metrics.Counter;
using Microsoft.Extensions.Logging;

namespace logmetric.Controllers
{
    public class HomeController : Controller
    {
        private readonly IMetrics metrics;
        private readonly ILogger<HomeController> logger;
        private CounterOptions _counterOptions = new CounterOptions {
            MeasurementUnit = Unit.Calls,
            Name = "Home Counter",
            ResetOnReporting = true
        };

        public HomeController(IMetrics metrics, ILogger<HomeController> logger)
        {
            this.metrics = metrics;
            this.logger = logger;
        }

        public IActionResult Index()
        {
            logger.LogInformation("Help ME!!!!!!!!!!!!");

            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult Increment(string tag = null)
        {
            var tags =  new MetricTags("userTag", string.IsNullOrEmpty(tag) ? "undefined" : tag);
            metrics.Measure.Counter.Increment(_counterOptions, tags);
            return Ok("done");
        }
    }
}
