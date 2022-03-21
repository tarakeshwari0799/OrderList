using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using OrderItem.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace OrderItem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {

        [HttpGet("{id}")]
        public IActionResult GetCart(int id)
        {
            using(HttpClient c=new HttpClient())
            {
                c.BaseAddress = new Uri("http://localhost:32768/api");
                var response = c.GetAsync("MenuItem");
                response.Wait();
                var result = response.Result;
                List<Cart> item = new List<Cart>();
                if (result.IsSuccessStatusCode)
                {
                    string jsondata = result.Content.ReadAsStringAsync().Result;
                    item = JsonConvert.DeserializeObject<List<Cart>>(jsondata);
                    Cart o = item.SingleOrDefault(i => i.Id == id);
                    o.MenuItemId = 1;
                    o.UserId = 1;
                    return Ok(o);
                }
                else
                {
                    return BadRequest();

                }

            };
        }
    }
}
