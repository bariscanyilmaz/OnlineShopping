using DataAccesLAYER_DAL_.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace OnlineShopping.Controllers
{
    public class ShopListController : Controller
    {
        // GET: ShopList
        public async Task<ActionResult> Shopping()
        {
            Session["UserID"] = 1;
            using (HttpClient client = new HttpClient())
            {
                var response=await client.GetAsync("http://localhost:54384/api/service");
                var model = JsonConvert.DeserializeObject<List<Product>>(
                    response.Content.ReadAsStringAsync().Result);
                return View(model);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<bool> Buy(FormCollection form)
        {
            bool result = false;
            if (form.Count>1)
            {
                List<ShopHistory> shopList = new List<ShopHistory>();
                for (int i = 1; i < form.Count; i++)
                {
                    ShopHistory _shophistory = new ShopHistory();
                    _shophistory.ProductId = int.Parse(form[i]);
                    _shophistory.UserId = (int)Session["UserID"];
                    _shophistory.CreatedDate = DateTime.Now;
                    shopList.Add(_shophistory);
                }

                using (HttpClient client = new HttpClient())
                {
                    var data = JsonConvert.SerializeObject(shopList);

                    HttpContent content = new StringContent(data,System.Text.Encoding.UTF8,"application/json");

                    var returnResult=await client.PostAsync("http://localhost:54384/api/service",content);
                    result = bool.Parse(returnResult.Content.ReadAsStringAsync().Result);
                }
            }

            return result;
        }

        public string Mesaj()
        {
            return "<h1>Açık Akademi MVC shopping<h1/>";
        }

    }
}