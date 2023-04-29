using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.JSInterop;
using WebApiCrud.Data;
using WebApiCrud.Model;

namespace WebApiCrud.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ApplicationContext context;
        public CustomerController(ApplicationContext context) {

            this.context = context;
        
        }
        [HttpGet]
        [Route("GetAllCustomer")]
        public ActionResult GetAllCustomer()
        {
            var data=context.Customers.ToList();
            if (data.Count() == 0)
            {
                return NotFound();
            }
            else
            {
                return Ok(data);
            }
        }
        [HttpGet]
        [Route("GetCustomerById/{id}")]
        public IActionResult GetCustomerById(int id)
        {
            if(id==0)
            {
                return NotFound();
            }
            else
            {
                var data=context.Customers.Where(e=>e.Id==id).SingleOrDefault();
                if (data==null)
                {
                    return BadRequest();
                }
                else
                {
                    return Ok(data);
                }
            }
        }
        [HttpPost]
        [Route("AddCustomer")]
        public IActionResult AddCustomer([FromBody]Customer model)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            else
            {
                //  context.Customers.Add(model);
                var data = new Customer
                {
                    Name = model.Name,
                    Gender = model.Gender,
                    IsActive = model.IsActive
                };
                context.Customers.Add(data);
                context.SaveChanges();
                return Ok();
            }
        }
        [HttpPut]
        [Route("UpdateCustomer")]
        public IActionResult UpdateCustomer([FromBody] Customer model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);

            }
            else
            {
                var data=context.Customers.Where(e=>e.Id == model.Id).SingleOrDefault();
                if(data==null)
                {
                    return BadRequest();
                }
                else
                {
                    data.Name = model.Name;
                    data.Gender = model.Gender;
                    data.IsActive = model.IsActive;
                   // var newdata = new Customer()
                   // {
                    //    Id=model.Id,
                    //    Name=model.Name,
                   //    Gender=model.Gender,
                    //    IsActive=model.IsActive
                   // };
                    context.Customers.Update(data);
                    context.SaveChanges() ;
                    return Ok();
                }
            }
        }
        [HttpDelete]
        [Route("DeleteCustomer/{id}")]
        public IActionResult DeleteCustomer(int id)
        {
            if (id != 0)
            {
                var data=context.Customers.Where(e=>e.Id==id).SingleOrDefault();
                if(data==null)
                {
                    return BadRequest();
                }
                else
                {
                    context.Customers.Remove(data);
                    context.SaveChanges();
                }
            }
            else
            {
                return BadRequest();
            }
            return Ok();    
        }
    }
}
