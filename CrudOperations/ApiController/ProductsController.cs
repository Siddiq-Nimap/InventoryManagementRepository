using PMS.Models.Models;
using PMS.Services.Services;
using System;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Script.Serialization;

namespace PMS.Controllers
{
    public class ProductsController : ApiController 
    {
        readonly ProductService _productService;
        public ProductsController(ProductService productService)
        {
            _productService = productService;

        }

        // GET: api/Products
        [HttpGet]
        public async Task<IHttpActionResult> GetProducts()
        {
            var data = await _productService.GetModel();

            JavaScriptSerializer scriptSerializer = new JavaScriptSerializer();

            var jsonstr = scriptSerializer.Serialize(data);
            
            return Ok(jsonstr);
        }

        // GET: api/Products/5
        [HttpGet]
        public IHttpActionResult GetProduct(int id)
        {
            var products =  _productService.GetModelById(id);

            if (products == null)
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
                 _productService.UpdateModel(pro);
                await _productService.Save();
            }

            catch (Exception E){return BadRequest(E.Message);}

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Products
        [HttpPost]
        public async Task<IHttpActionResult> PostProduct(Product products)
        {
            if (!ModelState.IsValid){return BadRequest(ModelState);}

           _productService.InsertModel(products);

            var check = await _productService.Save();

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
            var products = _productService.GetModelById(id);
            if (products == null)
            {
                return NotFound();
            }
             _productService.DeleteModel(id);
            var check = await _productService.Save();
            if(check == true) { return Ok(products); } else { return BadRequest(); }

            
        }


    }
}