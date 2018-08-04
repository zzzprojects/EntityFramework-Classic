using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UnitTests.Community.Shared.Net45.Context.SingleMany;

namespace UnitTests.Community.Shared.Net45.Include
{
    /// <summary>
    /// Summary description for AlsoInclude
    /// </summary>
    [TestClass]
    public class AlsoInclude
    {
        #region IncludeMany_AlsoIncludeXYZ

        [TestMethod]
        public void IncludeMany_AlsoIncludeMany()
        {
            var ctx = new SingleManyContext();

            var root = ctx.Entity_Root
                .Include(x => x.Entity_M)
                .AlsoInclude(x => x.Entity_M_M)
                .First();

            Assert.IsNotNull(root);
            Assert.IsTrue(root.Entity_M.Any());

            foreach (var item in root.Entity_M)
            {
                Assert.IsTrue(item.Entity_M_M.Any());
            }
        }

        [TestMethod]
        public void IncludeMany_AlsoIncludeSingle()
        {
            var ctx = new SingleManyContext();

            var root = ctx.Entity_Root
                .Include(x => x.Entity_M)
                .AlsoInclude(x => x.Entity_M_S)
                .First();

            Assert.IsNotNull(root);
            Assert.IsTrue(root.Entity_M.Any());

            foreach (var item in root.Entity_M)
            {
                Assert.IsNotNull(item.Entity_M_S);
            }
        }

        [TestMethod]
        public void IncludeMany_AlsoIncludeMany_AlsoIncludeSingle()
        {
            var ctx = new SingleManyContext();

            var root = ctx.Entity_Root
                .Include(x => x.Entity_M)
                .AlsoInclude(x => x.Entity_M_M)
                .AlsoInclude(x => x.Entity_M_S)
                .First();

            Assert.IsNotNull(root);
            Assert.IsTrue(root.Entity_M.Any());

            foreach (var item in root.Entity_M)
            {
                Assert.IsTrue(item.Entity_M_M.Any());
                Assert.IsNotNull(item.Entity_M_S);
            }
        }

        #endregion

        #region IncludeSingle_AlsoIncludeXYZ

        [TestMethod]
        public void IncludeSingle_AlsoIncludeMany()
        {
            var ctx = new SingleManyContext();

            var root = ctx.Entity_Root
                .Include(x => x.Entity_S)
                .AlsoInclude(x => x.Entity_S_M)
                .First();

            Assert.IsNotNull(root);
            Assert.IsNotNull(root.Entity_S);
            Assert.IsTrue(root.Entity_S.Entity_S_M.Any());
        }

        [TestMethod]
        public void IncludeSingle_AlsoIncludeSingle()
        {
            var ctx = new SingleManyContext();

            var root = ctx.Entity_Root
                .Include(x => x.Entity_S)
                .AlsoInclude(x => x.Entity_S_S)
                .First();

            Assert.IsNotNull(root);
            Assert.IsNotNull(root.Entity_S);
            Assert.IsNotNull(root.Entity_S.Entity_S_S);
        }

        [TestMethod]
        public void IncludeSingle_AlsoIncludeMany_AlsoIncludeSingle()
        {
            var ctx = new SingleManyContext();

            var root = ctx.Entity_Root
                .Include(x => x.Entity_S)
                .AlsoInclude(x => x.Entity_S_M)
                .AlsoInclude(x => x.Entity_S_S)
                .First();

            Assert.IsNotNull(root);
            Assert.IsNotNull(root.Entity_S);
            Assert.IsTrue(root.Entity_S.Entity_S_M.Any());
            Assert.IsNotNull(root.Entity_S.Entity_S_S);
        }

        #endregion
    }
}
