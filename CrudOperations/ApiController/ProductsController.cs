using BusinessLayer;
using DAL.Models;
using System;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Script.Serialization;

namespace CrudOperations.Controllers
{
    public class ProductsController : ApiController 
    {
        readonly IAllRepository<Product> product;
        public ProductsController(IAllRepository<Product> product)
        {
            this.product = product;

        }

        // GET: api/Products
        [HttpGet]
        public async Task<IHttpActionResult> GetProducts()
        {
            var data = await product.GetModel();

            JavaScriptSerializer scriptSerializer = new JavaScriptSerializer();

            var jsonstr = scriptSerializer.Serialize(data);
            
            return Ok(jsonstr);
        }

        // GET: api/Products/5
        [HttpGet]
        public IHttpActionResult GetProduct(int id)
        {
            var products =  product.GetModelById(id);

            if (product == null)
            {
                return BadRequest();
            }
            return Ok(products);
        }

        // PUT: api/Products/5
        [HttpPut]
        public async Task<IHttpActionResult> PutProduct(int id, Product pro)
        {
            if (!ModelState.IsValid){return BadRequest(ModelState);}

            if (id != pro.Id){return BadRequest();}

            try
            {
                 product.UpdateModel(pro);
                await product.Save();
            }

            catch (Exception E){return BadRequest(E.Message);}

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Products
        [HttpPost]
        public async Task<IHttpActionResult> PostProduct(Product products)
        {
            if (!ModelState.IsValid){return BadRequest(ModelState);}

           product.InsertModel(products);

            var check = await product.Save();

            if (check == true)
            {
                //return Created(new Uri(Request.RequestUri + "/" + new { id = products.Id }), products);
                return CreatedAtRoute("DefaultApi", new { id = products.Id }, products);
            }
            else{return BadRequest(ModelState);}
        }

        // DELETE: api/Products/5
        [HttpDelete]
        public async Task<IHttpActionResult> DeleteProduct(int id)
        {
            var products = product.GetModelById(id);
            if (products == null)
            {
                return NotFound();
            }
             product.DeleteModel(id);
            var check = await product.Save();
            if(check == true) { return Ok(products); } else { return BadRequest(); }

            
        }


    }
}