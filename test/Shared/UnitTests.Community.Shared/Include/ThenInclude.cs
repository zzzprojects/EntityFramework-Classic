using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UnitTests.Community.Shared.Net45.Context.SingleMany;

namespace UnitTests.Community.Shared.Net45.Include
{
    /// <summary>
    ///     Summary description for AlsoInclude
    /// </summary>
    [TestClass]
    public class ThenInclude
    {
        #region IncludeMany_ThenMany_XYZ

        [TestMethod]
        public void IncludeMany_ThenMany()
        {
            var ctx = new SingleManyContext();

            var root = ctx.Entity_Root
                .Include(x => x.Entity_M)
                .ThenInclude(x => x.Entity_M_M)
                .First();

            Assert.IsNotNull(root);
            Assert.IsTrue(root.Entity_M.Any());

            foreach (var item in root.Entity_M)
            {
                Assert.IsTrue(item.Entity_M_M.Any());
            }
        }

        [TestMethod]
        public void IncludeMany_ThenMany_ThenMany()
        {
            var ctx = new SingleManyContext();

            var root = ctx.Entity_Root
                .Include(x => x.Entity_M)
                .ThenInclude(x => x.Entity_M_M)
                .ThenInclude(x => x.Entity_M_M_M)
                .First();

            Assert.IsNotNull(root);
            Assert.IsTrue(root.Entity_M.Any());

            foreach (var entity_M in root.Entity_M)
            {
                Assert.IsTrue(entity_M.Entity_M_M.Any());

                foreach (var entity_M_M in entity_M.Entity_M_M)
                {
                    Assert.IsTrue(entity_M_M.Entity_M_M_M.Any());
                }
            }
        }

        [TestMethod]
        public void IncludeMany_ThenMany_ThenSingle()
        {
            var ctx = new SingleManyContext();

            var root = ctx.Entity_Root
                .Include(x => x.Entity_M)
                .ThenInclude(x => x.Entity_M_M)
                .ThenInclude(x => x.Entity_M_M_S)
                .First();

            Assert.IsNotNull(root);
            Assert.IsTrue(root.Entity_M.Any());

            foreach (var entity_M in root.Entity_M)
            {
                Assert.IsTrue(entity_M.Entity_M_M.Any());

                foreach (var entity_M_M in entity_M.Entity_M_M)
                {
                    Assert.IsNotNull(entity_M_M.Entity_M_M_S);
                }
            }
        }

        [TestMethod]
        public void IncludeMany_ThenMany_SelectMany()
        {
            var ctx = new SingleManyContext();

            var root = ctx.Entity_Root
                .Include(x => x.Entity_M)
                .ThenInclude(x => x.Entity_M_M.Select(y => y.Entity_M_M_M))
                .First();

            Assert.IsNotNull(root);
            Assert.IsTrue(root.Entity_M.Any());

            foreach (var entity_M in root.Entity_M)
            {
                Assert.IsTrue(entity_M.Entity_M_M.Any());

                foreach (var entity_M_M in entity_M.Entity_M_M)
                {
                    Assert.IsTrue(entity_M_M.Entity_M_M_M.Any());
                }
            }
        }

        [TestMethod]
        public void IncludeMany_ThenMany_SelectSingle()
        {
            var ctx = new SingleManyContext();

            var root = ctx.Entity_Root
                .Include(x => x.Entity_M)
                .ThenInclude(x => x.Entity_M_M.Select(y => y.Entity_M_M_S))
                .First();

            Assert.IsNotNull(root);
            Assert.IsTrue(root.Entity_M.Any());

            foreach (var entity_M in root.Entity_M)
            {
                Assert.IsTrue(entity_M.Entity_M_M.Any());

                foreach (var entity_M_M in entity_M.Entity_M_M)
                {
                    Assert.IsNotNull(entity_M_M.Entity_M_M_S);
                }
            }
        }

        #endregion

        #region IncludeMany_ThenSingle_XYZ

        [TestMethod]
        public void IncludeMany_ThenSingle()
        {
            var ctx = new SingleManyContext();

            var root = ctx.Entity_Root
                .Include(x => x.Entity_M)
                .ThenInclude(x => x.Entity_M_S)
                .First();

            Assert.IsNotNull(root);
            Assert.IsTrue(root.Entity_M.Any());

            foreach (var item in root.Entity_M)
            {
                Assert.IsNotNull(item.Entity_M_S);
            }
        }

        [TestMethod]
        public void IncludeMany_ThenSingle_ThenMany()
        {
            var ctx = new SingleManyContext();

            var root = ctx.Entity_Root
                .Include(x => x.Entity_M)
                .ThenInclude(x => x.Entity_M_S)
                .ThenInclude(x => x.Entity_M_S_M)
                .First();

            Assert.IsNotNull(root);
            Assert.IsTrue(root.Entity_M.Any());

            foreach (var item in root.Entity_M)
            {
                Assert.IsNotNull(item.Entity_M_S);
                Assert.IsTrue(item.Entity_M_S.Entity_M_S_M.Any());
            }
        }

        [TestMethod]
        public void IncludeMany_ThenSingle_ThenSingle()
        {
            var ctx = new SingleManyContext();

            var root = ctx.Entity_Root
                .Include(x => x.Entity_M)
                .ThenInclude(x => x.Entity_M_S)
                .ThenInclude(x => x.Entity_M_S_S)
                .First();

            Assert.IsNotNull(root);
            Assert.IsTrue(root.Entity_M.Any());

            foreach (var item in root.Entity_M)
            {
                Assert.IsNotNull(item.Entity_M_S);
                Assert.IsNotNull(item.Entity_M_S.Entity_M_S_S);
            }
        }


        [TestMethod]
        public void IncludeMany_ThenSingle_SelectMany()
        {
            var ctx = new SingleManyContext();

            var root = ctx.Entity_Root
                .Include(x => x.Entity_M)
                .ThenInclude(x => x.Entity_M_S.Entity_M_S_M)
                .First();

            Assert.IsNotNull(root);
            Assert.IsTrue(root.Entity_M.Any());

            foreach (var item in root.Entity_M)
            {
                Assert.IsNotNull(item.Entity_M_S);
                Assert.IsTrue(item.Entity_M_S.Entity_M_S_M.Any());
            }
        }

        [TestMethod]
        public void IncludeMany_ThenSingle_SelectSingle()
        {
            var ctx = new SingleManyContext();

            var root = ctx.Entity_Root
                .Include(x => x.Entity_M)
                .ThenInclude(x => x.Entity_M_S.Entity_M_S_S)
                .First();

            Assert.IsNotNull(root);
            Assert.IsTrue(root.Entity_M.Any());

            foreach (var item in root.Entity_M)
            {
                Assert.IsNotNull(item.Entity_M_S);
                Assert.IsNotNull(item.Entity_M_S.Entity_M_S_S);
            }
        }

        #endregion

        #region IncludeSingle_ThenMany_XYZ

        [TestMethod]
        public void IncludeSingle_ThenMany()
        {
            var ctx = new SingleManyContext();

            var root = ctx.Entity_Root
                .Include(x => x.Entity_S)
                .ThenInclude(x => x.Entity_S_M)
                .First();

            Assert.IsNotNull(root);
            Assert.IsNotNull(root.Entity_S);
            Assert.IsTrue(root.Entity_S.Entity_S_M.Any());
        }

        [TestMethod]
        public void IncludeSingle_ThenMany_ThenMany()
        {
            var ctx = new SingleManyContext();

            var root = ctx.Entity_Root
                .Include(x => x.Entity_S)
                .ThenInclude(x => x.Entity_S_M)
                .ThenInclude(x => x.Entity_S_M_M)
                .First();

            Assert.IsNotNull(root);
            Assert.IsNotNull(root.Entity_S);
            Assert.IsTrue(root.Entity_S.Entity_S_M.Any());

            foreach (var entity_S_M in root.Entity_S.Entity_S_M)
            {
                Assert.IsTrue(entity_S_M.Entity_S_M_M.Any());
            }
        }

        [TestMethod]
        public void IncludeSingle_ThenMany_ThenSingle()
        {
            var ctx = new SingleManyContext();

            var root = ctx.Entity_Root
                .Include(x => x.Entity_S)
                .ThenInclude(x => x.Entity_S_M)
                .ThenInclude(x => x.Entity_S_M_S)
                .First();

            Assert.IsNotNull(root);
            Assert.IsNotNull(root.Entity_S);
            Assert.IsTrue(root.Entity_S.Entity_S_M.Any());

            foreach (var entity_S_M in root.Entity_S.Entity_S_M)
            {
                Assert.IsNotNull(entity_S_M.Entity_S_M_S);
            }
        }

        [TestMethod]
        public void IncludeSingle_ThenMany_SelectMany()
        {
            var ctx = new SingleManyContext();

            var root = ctx.Entity_Root
                .Include(x => x.Entity_S)
                .ThenInclude(x => x.Entity_S_M.Select(y => y.Entity_S_M_M))
                .First();

            Assert.IsNotNull(root);
            Assert.IsNotNull(root.Entity_S);
            Assert.IsTrue(root.Entity_S.Entity_S_M.Any());

            foreach (var entity_S_M in root.Entity_S.Entity_S_M)
            {
                Assert.IsTrue(entity_S_M.Entity_S_M_M.Any());
            }
        }

        [TestMethod]
        public void IncludeSingle_ThenMany_SelectSingle()
        {
            var ctx = new SingleManyContext();

            var root = ctx.Entity_Root
                .Include(x => x.Entity_S)
                .ThenInclude(x => x.Entity_S_M.Select(y => y.Entity_S_M_S))
                .First();

            Assert.IsNotNull(root);
            Assert.IsNotNull(root.Entity_S);
            Assert.IsTrue(root.Entity_S.Entity_S_M.Any());

            foreach (var entity_S_M in root.Entity_S.Entity_S_M)
            {
                Assert.IsNotNull(entity_S_M.Entity_S_M_S);
            }
        }

        #endregion

        #region IncludeSingle_ThenSingle_XYZ

        [TestMethod]
        public void IncludeSingle_ThenSingle()
        {
            var ctx = new SingleManyContext();

            var root = ctx.Entity_Root
                .Include(x => x.Entity_S)
                .ThenInclude(x => x.Entity_S_S)
                .First();

            Assert.IsNotNull(root);
            Assert.IsNotNull(root.Entity_S);
            Assert.IsNotNull(root.Entity_S.Entity_S_S);
        }

        [TestMethod]
        public void IncludeSingle_ThenSingle_ThenMany()
        {
            var ctx = new SingleManyContext();

            var root = ctx.Entity_Root
                .Include(x => x.Entity_S)
                .ThenInclude(x => x.Entity_S_S)
                .ThenInclude(x => x.Entity_S_S_M)
                .First();

            Assert.IsNotNull(root);
            Assert.IsNotNull(root.Entity_S);
            Assert.IsNotNull(root.Entity_S.Entity_S_S);
            Assert.IsTrue(root.Entity_S.Entity_S_S.Entity_S_S_M.Any());
        }

        [TestMethod]
        public void IncludeSingle_ThenSingle_ThenSingle()
        {
            var ctx = new SingleManyContext();

            var root = ctx.Entity_Root
                .Include(x => x.Entity_S)
                .ThenInclude(x => x.Entity_S_S)
                .ThenInclude(x => x.Entity_S_S_S)
                .First();

            Assert.IsNotNull(root);
            Assert.IsNotNull(root.Entity_S);
            Assert.IsNotNull(root.Entity_S.Entity_S_S);
            Assert.IsNotNull(root.Entity_S.Entity_S_S.Entity_S_S_S);
        }


        [TestMethod]
        public void IncludeSingle_ThenSingle_SelectMany()
        {
            var ctx = new SingleManyContext();

            var root = ctx.Entity_Root
                .Include(x => x.Entity_S)
                .ThenInclude(x => x.Entity_S_S.Entity_S_S_M)
                .First();

            Assert.IsNotNull(root);
            Assert.IsNotNull(root.Entity_S);
            Assert.IsNotNull(root.Entity_S.Entity_S_S);
            Assert.IsTrue(root.Entity_S.Entity_S_S.Entity_S_S_M.Any());
        }

        [TestMethod]
        public void IncludeSingle_ThenSingle_SelectSingle()
        {
            var ctx = new SingleManyContext();

            var root = ctx.Entity_Root
                .Include(x => x.Entity_S)
                .ThenInclude(x => x.Entity_S_S.Entity_S_S_S)
                .First();

            Assert.IsNotNull(root);
            Assert.IsNotNull(root.Entity_S);
            Assert.IsNotNull(root.Entity_S.Entity_S_S);
            Assert.IsNotNull(root.Entity_S.Entity_S_S.Entity_S_S_S);
        }

        #endregion
    }
}