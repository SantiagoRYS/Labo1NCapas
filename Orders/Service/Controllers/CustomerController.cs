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

        public Task<ActionResult<Customer>> CreateAsync([FromBody] Customer toCreate)
        {
            throw new NotImplementedException();
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

                return StatusCode(StatusCodes.Status500InternalServerError, "An unexpected error ocurred");
            }
        }

        public Task<ActionResult<Customer>> RetrieveAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ActionResult> UpdateAsync(int id, [FromBody] Customer toUpdate)
        {
            throw new NotImplementedException();
        }
    }
}
