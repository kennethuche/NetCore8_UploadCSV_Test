using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Globalization;
using TestP.Data.Context;
using TestP.Data.Interface;
using TestP.Data.Model;
using TestP.Helper;
using TestP.Models;

namespace TestP.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IPersonRepository _personRepository;
        public HomeController(ILogger<HomeController> logger, IPersonRepository personRepository)
        {
            _logger = logger;
            _personRepository = personRepository;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<JsonResult> UploadCsv(IFormFile csvFile)
        {
            if (csvFile == null || Path.GetExtension(csvFile.FileName).ToLower() != ".csv")
            {
                return Json(new { success = false, message = "Invalid file type. Please upload a CSV file." });
            }

            var persons = new List<Person>();
            var errors = new List<string>();

            try
            {
                using (var stream = new StreamReader(csvFile.OpenReadStream()))
                using (var csv = new CsvReader(stream, new CsvConfiguration(CultureInfo.InvariantCulture)
                {
                    BadDataFound = null, // Ignore bad data found
                    MissingFieldFound = null // Ignore missing fields
                }))
                {
                    csv.Context.RegisterClassMap<PersonMap>();

                    while (csv.Read())
                    {
                        try
                        {
                            var person = csv.GetRecord<Person>();
                            persons.Add(person);
                        }
                        catch (TypeConverterException ex)
                        {
                            errors.Add($"{ex.Message}");
                        }
                        catch (CsvHelperException ex)
                        {
                            errors.Add($"{ex.Message}");
                        }
                    }
                }


                await _personRepository.AddPersonsAsync(persons);
                return Json(new { success = true, message = "File uploaded and data saved successfully." });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error uploading CSV file");
                return Json(new { success = false, message = "An error occurred while uploading the file." });
            }
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
