using System.Collections.Generic;
using System.Linq; 
using Microsoft.AspNetCore.Mvc;

namespace EFClassic.Demo.DatabaseFirst.WebCore
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
	            Seed(context);

				return context.Customers.ToList();
            }
        }

	    protected void Seed(EntityContext context)
	    {
		    // ADD 2 new customers
		    context.Customers.Add(new Customer { Name = "Customer_A", Description = "Description", IsActive = true });
		    context.Customers.Add(new Customer { Name = "Customer_B", Description = "Description", IsActive = true });

		    context.SaveChanges();
	    }
	}
}