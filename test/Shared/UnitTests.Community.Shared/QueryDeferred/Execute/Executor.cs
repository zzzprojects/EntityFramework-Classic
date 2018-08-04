using Microsoft.VisualStudio.TestTools.UnitTesting;
using UnitTests.Community.Shared._Context;

namespace UnitTests.Community.Shared.Net45.QueryDeferred
{
    public partial class QueryDeferred_Execute
    {
        [TestMethod]
        public void Executor()
        {
            EntitiesContext.DeleteAll(x => x.Customers);
            EntitiesContext.Insert(x => x.Customers, 10);

            using (var ctx = new EntitiesContext())
            {
                var deferred = ctx.Customers.DeferredCount();

                var count = deferred.Execute();
                Assert.AreEqual(10, count);
            }
        }
    }
}