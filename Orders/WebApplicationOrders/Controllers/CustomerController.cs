using Microsoft.AspNetCore.Mvc;
using ProxyServer.Interfaces;
using ProxyServer;
using Entities.Models;

namespace WebApplicationOrders.Controllers
{
    public class CustomersController : Controller
    {
        private readonly ICustomerProxy _proxy;

        public CustomersController()
        {
            this._proxy = new CustomerProxy();
        }
        public async Task<IActionResult> Index()
        {
            var customers = await _proxy.GetAllAsync();
            return View(customers);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Create([Bind("Id, FirstName, LastName, City, Country, Phone")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var result = await _proxy.CreateAsync(customer);
                    if (result == null)
                    {
                        return RedirectToAction("Error", new {message = "El cliente con el mismo nombre y apellido ya existe"});
                    }
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    return RedirectToAction("Error", new { message = ex.Message });
                }
            }
            return View(customer);
        }


        public async Task<IActionResult> Edit(int Id)
        {
            var customer = await _proxy.GetByIdAsync(Id);
            if (customer == null)
            {
                return NotFound();
            }
            return View(customer);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id, FirstName, LastName, City, Country, Phone")] Customer customer)
        {
            if (id != customer.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var result = await _proxy.UpdateAsync(id, customer);
                    if (!result)
                    {
                        return RedirectToAction("Error", new { message = "No se puede realizar la edición porque hay duplicidad de nombre con otro Cliente" });
                    }
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    return RedirectToAction("Error", new {message = ex.Message });
                }
            }
            return View(customer);
        }

        public async Task<IActionResult> Details(int Id)
        {
            var customer = await _proxy.GetByIdAsync(Id);
            if (customer == null)
            {
                return NotFound();
            }
            return View(customer);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var customer = await _proxy.GetByIdAsync(id);
            if (customer == null)
            {
                return NotFound();
            }
            return View(customer);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var result = await _proxy.DeleteAsync(id);
                if (!result)
                {
                    return RedirectToAction("Error", new { message = "No se puede eliminar el cliente porque tiene facturas asociadas." });
                }
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error", new { message = ex.Message });
            }
        }


        public IActionResult Error(string message)
        {
            ViewBag.ErrorMessage = message;
            return View();
        }



    }
}