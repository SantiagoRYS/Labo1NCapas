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
    public class SupplierController : ControllerBase, ISupplierService
    {
        private readonly Suppliers _bll;

        public SupplierController(Suppliers bll)
        {
            _bll = bll;
        }




        [HttpGet]
        public async Task<ActionResult<List<Supplier>>> GetAll()
        {
            try
            {
                var result = await _bll.RetrieveAllAsync();
                return Ok(result);
            }
            catch (SupplierExceptions ex)
            {

                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "An unexpected error ocurred.");
            }
        }

        [HttpPost]
        public async Task<ActionResult<Supplier>> CreateAsync([FromBody] Supplier toCreate)
        {
            try
            {
                var supplier = await _bll.CreateAsync(toCreate);

                return CreatedAtRoute("RetrieveAsync", new { id = supplier.Id }, supplier);
            }
            catch (SupplierExceptions ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An unexpected error ocurred.");
            }
        }

        [HttpGet("{id}", Name = "RetrieveSupplierAsync")]
        public async Task<ActionResult<Supplier>> RetrieveAsync(int id)
        {
            try
            {
                var supplier = await _bll.RetrieveByIDAsync(id);
                if (supplier == null)
                {
                    return NotFound("Supplier not found");
                }
                return Ok(supplier);
            }
            catch (SupplierExceptions ce)
            {

                return BadRequest(ce.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An unexpected error ocurred.");
            }

        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(int id, [FromBody] Supplier toUpdate)
        {
            toUpdate.Id = id;
            try
            {
                var result = await _bll.UpdateAsync(toUpdate);
                if (!result)
                {
                    return NotFound("Supplier not found or update failed.");
                }
                return NoContent();
            }
            catch (SupplierExceptions ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An unexpected error ocurred.");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            try
            {
                var result = await _bll.DeleteAsync(id);
                if (!result)
                {
                    return NotFound("Supplier not found or deletion failed.");
                }
                return NoContent();
            }
            catch (SupplierExceptions ex)
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
