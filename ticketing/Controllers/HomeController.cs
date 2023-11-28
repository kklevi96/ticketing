using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using ticketing.Models;

namespace ticketing.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        public List<string> _gondok;

        public IActionResult Index()
        {
            List<string> gondok = ReadGondokFromFile();
            return View(gondok);
        }

        private readonly string _gondokFilePath = "wwwroot/gondok.txt";

        [HttpPost]
        public IActionResult GondSubmitted(string gond)
        {
            // Itt kezeld a beküldött "gond"-ot (mentés, stb.)
            // Például mentés egy adatbázisban vagy listában

            // Frissítsd a gondok listát
            List<string> gondok = ReadGondokFromFile();
            gondok.Add(gond);

            // Írd vissza a frissített gondokat a fájlba
            WriteGondokToFile(gondok);

            return RedirectToAction("Index");
        }

        private List<string> ReadGondokFromFile()
        {
            if (System.IO.File.Exists(_gondokFilePath))
            {
                return System.IO.File.ReadAllLines(_gondokFilePath).ToList();
            }
            else
            {
                return new List<string>();
            }
        }

        private void WriteGondokToFile(List<string> gondok)
        {
            System.IO.File.WriteAllLines(_gondokFilePath, gondok);
        }

        public IActionResult Privacy()
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