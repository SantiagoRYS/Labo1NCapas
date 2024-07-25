using BLL; 
using Entities.Models; 
using Microsoft.AspNetCore.Mvc; 
using SLC; 
using System.Linq.Expressions; 
using Microsoft.AspNetCore.Http; 
using BLL.Exceptions;

namespace Services.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase, ICustomerService
    {
        private readonly Customers _bll;

        public CustomerController(Customers bll)
        {
            _bll = bll;
        }

        [HttpPost]
        public async Task<ActionResult<Customer>> CreateAsync([FromBody] Customer toCreate)
        {
            try
            {
                var customer = await _bll.CreateAsync(toCreate);

                return CreatedAtRoute("RetrieveAsync", new { id = customer.Id }, customer);
            }
            catch (CustomerExceptions ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An unexpected error ocurred.");
            }
        }

        public Task<ActionResult> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<ActionResult<List<Customer>>> GetAll()
        {
            try
            {
                var result = await _bll.RetrieveAllAsync();
                return Ok(result);
            }
            catch (CustomerExceptions ex)
            {

                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "An unexpected error ocurred.");
            }
        }

        [HttpGet("{id}", Name = "RetrieveAsync")]
        public async Task<ActionResult<Customer>> RetrieveAsync(int id)
        {
            try
            {
                var customer = await _bll.RetrieveByIDAsync(id);
                if (customer == null)
                {
                    return NotFound("Customer not found");
                }
                return Ok(customer);
            }
            catch (CustomerExceptions ce)
            {

                return BadRequest(ce.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An unexpected error ocurred.");
            }

        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateAsync(int id, [FromBody] Customer toUpdate)
        {
            toUpdate.Id = id;
            try
            {
                var result = await _bll.UpdateAsync(toUpdate);
                if (!result)
                {
                    return NotFound("Customers not found or update failed.");
                }
                return NoContent();
            }
            catch (CustomerExceptions ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An unexpected error ocurred.");
            }
        }
    }
}
