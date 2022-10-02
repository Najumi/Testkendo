using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Newtonsoft.Json;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Testkendo.Models;
using Testkendo.Models.Model;
using Product = Testkendo.Models.Product;

namespace Testkendo.Controllers
{
    public class HomeController : Controller
    {
        HttpClient hc = new HttpClient();
        public async Task<ActionResult> Index()
        {

            ViewModel vm = new ViewModel();
            hc.BaseAddress = new Uri("https://localhost:44336/");

            HttpResponseMessage message = await hc.GetAsync("api/product/GetItem/");
            if (message.IsSuccessStatusCode)
            {
                var display = message.Content.ReadAsAsync<List<spGetItems_Result>>();
                vm.getItem = display.Result;
            }
            HttpResponseMessage mess = await hc.GetAsync("api/product/Get/");
            if (mess.IsSuccessStatusCode)
            {
                var display = mess.Content.ReadAsAsync<List<getCategory_Result>>();
                vm.getcategory = display.Result;
            }

            return View(vm);
            

            
        }

        public ActionResult Secondpage()
        {
            NewViewModel nvm = new NewViewModel();
            return View(nvm);
        }

        public ActionResult Thirdpage()
        {
            NewViewModel nvm = new NewViewModel();
            return View(nvm);
        }



        public async Task<JsonResult> GetCategory()
        {
            List<getCategory_Result> list = new List<getCategory_Result>();


            hc.BaseAddress = new Uri("https://localhost:44336/");

            HttpResponseMessage message = await hc.GetAsync("api/product/Get/");

            if (message.IsSuccessStatusCode)
            {
                var display = message.Content.ReadAsAsync<List<getCategory_Result>>();
                list = display.Result;
            }
            return Json(list, JsonRequestBehavior.AllowGet);
        }


        public async Task<ActionResult> EditingPopup_Read([DataSourceRequest] DataSourceRequest request)
        {
            List<spGetItems_Result> list = new List<spGetItems_Result>();
            

            hc.BaseAddress = new Uri("https://localhost:44336/");

            HttpResponseMessage message = await hc.GetAsync("api/product/getItem/");

            if (message.IsSuccessStatusCode)
            {
                var display = message.Content.ReadAsAsync<List<spGetItems_Result>>();
                list = display.Result;
            }
            return Json(list.ToDataSourceResult(request));
            //return Json(ProductService.Read().ToDataSourceResult(request));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public async Task<ActionResult> EditingPopup_Create([DataSourceRequest] DataSourceRequest request, Product product)
        {
            if (product != null && ModelState.IsValid)
            {
                hc.BaseAddress = new Uri("https://localhost:44336/");

                var stringContent = new StringContent(JsonConvert.SerializeObject(product));

                HttpResponseMessage message = await hc.PostAsync("api/product/AddItem/", stringContent);
            }

            return Json(new[] { product }.ToDataSourceResult(request, ModelState));
        }

        //[AcceptVerbs(HttpVerbs.Post)]
        //public ActionResult EditingPopup_Update([DataSourceRequest] DataSourceRequest request, ProductViewModel product)
        //{
        //    if (product != null && ModelState.IsValid)
        //    {
        //        productService.Update(product);
        //    }

        //    return Json(new[] { product }.ToDataSourceResult(request, ModelState));
        //}

        [AcceptVerbs(HttpVerbs.Post)]
        public async Task<ActionResult> EditingPopup_Destroy([DataSourceRequest] DataSourceRequest request, Product product)
        {
            if (product != null)
            {
                hc.BaseAddress = new Uri("https://localhost:44336/");

                var stringContent = new StringContent(JsonConvert.SerializeObject(product));

                HttpResponseMessage message = await hc.PostAsync("api/product/deleteItem/", stringContent);
            }

            return Json(new[] { product }.ToDataSourceResult(request, ModelState));
        }
    }
}
