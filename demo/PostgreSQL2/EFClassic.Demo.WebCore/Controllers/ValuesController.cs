using System.Collections.Generic;
using System.Linq;
using EFClassic.Demo.WebCore.Data;
using EFClassic.Demo.WebCore.Entities;
using Microsoft.AspNetCore.Mvc;

namespace EFClassic.Demo.WebCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<Customer>> Get()
        {
            using (var context = new EntityContext())
            {
                return context.Customers.ToList();
            }
        }
    }
}