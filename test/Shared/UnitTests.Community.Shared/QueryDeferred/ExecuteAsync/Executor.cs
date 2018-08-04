using Microsoft.VisualStudio.TestTools.UnitTesting;
using UnitTests.Community.Shared._Context;

namespace UnitTests.Community.Shared.Net45.QueryDeferred
{
    public partial class QueryDeferred_ExecuteAsync
    {
        [TestMethod]
        public void Executor()
        {
            EntitiesContext.DeleteAll(x => x.Customers);
            EntitiesContext.Insert(x => x.Customers, 10);

            using (var ctx = new EntitiesContext())
            {
                var deferred = ctx.Customers.DeferredCount();

                var task = deferred.ExecuteAsync();
                var count = task.Result;

                Assert.AreEqual(10, count);
            }
        }
    }
}