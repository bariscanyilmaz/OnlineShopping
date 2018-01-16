using DataAccesLAYER_DAL_;
using DataAccesLAYER_DAL_.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Service.Controllers
{
    public class ServiceController : ApiController
    {
        // GET: api/Service
        public List<Product> Get()
        {
            using(ShopContext db=new ShopContext())
            {
                return db.Product.ToList();
            }
            
        }

        // GET: api/Service/5
        public Product Get(int id)
        {
            using (ShopContext db = new ShopContext())
            {
                return db.Product.Single(d => d.id == id);
            }
        }

        // POST: api/Service
        public bool Post(List<ShopHistory> shoppingList)
        {
            try
            {
                //throw new Exception("Bakımdayız");

                using(ShopContext context=new ShopContext())
                {
                    context.ShopData.AddRange(shoppingList);
                    context.SaveChanges();
                    return true;
                }

               
            }
            catch (Exception)
            {

               return false;
            }
        }

        
    }
}
