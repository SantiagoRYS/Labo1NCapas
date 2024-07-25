﻿using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SLC
{
    public interface ICustomerService
    {
        Task<ActionResult<Customer>> CreateAsync([FromBody] Customer toCreate);

        Task<ActionResult> DeleteAsync(int id);

        Task<ActionResult<List<Customer>>> GetAll();
    }
}
