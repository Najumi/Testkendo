using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;
using Testkendo.Models;

namespace Testkendo.Controllers
{
    public class ProductController : ApiController
    {
        SRestaurantEntities db = new SRestaurantEntities();
        public IHttpActionResult Get()
        {
            List<getCategory_Result> result = db.getCategory().ToList();
            return Json(result);
        }

        public IHttpActionResult GetItem()
        {
            List<spGetItems_Result> result = db.spGetItems().ToList();
            return Json(result);
        }

        public IHttpActionResult DeleteItem(Product product)
        {
            ObjectParameter responseMessage = new ObjectParameter("responseMessage", typeof(string));
            //List<Product> pro = JsonConvert.DeserializeObject<List<Product>>(product);

            //var result = db.DeleteItem(Convert.ToInt32(product["PId"]), responseMessage);
            var result = db.DeleteItem(product.PId, responseMessage);
            db.SaveChanges();
            return Json(result);
        }

        public IHttpActionResult AddItem(Product product)
        {
            ObjectParameter responseMessage = new ObjectParameter("responseMessage", typeof(string));
            var result = db.AddItems(product.PName,(int)product.PPrice,product.CId,product.PDesc,responseMessage);
            return Json(result);
        }



    }
}
