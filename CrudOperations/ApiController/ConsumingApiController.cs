using PMS.Models.Models;
using PMS.Models.Models.DTO;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace PMS.Controllers
{
    public class ConsumingApiController : Controller
    {
        // GET: ConsumingApi
        HttpClient client = new HttpClient();

        public ActionResult Index()
        {
           List<Product> products = new List<Product>();
            client.BaseAddress = new Uri("https://localhost:44370/api/Products");

            var response = client.GetAsync("Products");
            response.Wait();
            var test = response.Result;

            

            if (test.IsSuccessStatusCode)
            {
                var display = test.Content.ReadAsStringAsync();
                display.Wait();

                JavaScriptSerializer scriptSerializer = new JavaScriptSerializer();

                string json = scriptSerializer.Deserialize<string>(display.Result);

                products = scriptSerializer.Deserialize<List<Product>>(json);

            }
            return View(products);
        }
    }
}