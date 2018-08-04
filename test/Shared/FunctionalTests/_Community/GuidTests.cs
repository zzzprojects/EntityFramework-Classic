using System;
using System.Data.Entity;
using System.Data.Entity.TestModels.ArubaModel;
using System.Linq;
using Xunit;

namespace FunctionalTests.Shared._Community
{
    public class CommunityFunctionsTests : FunctionalTestBase
    {
        public class CommunityStringFunctions : FunctionalTestBase
        {
            [Fact]
            public void GuidConstructor_translated_to_correct_function_in_database()
            {
                using (var context = new ArubaContext())
                {
                    var query = context.Owners.Select(o => new Guid("4b44ce33-b60e-4afd-85ad-59d3d7c53f75"));
                    Assert.Contains("CAST('4B44CE33-B60E-4AFD-85AD-59D3D7C53F75' AS UNIQUEIDENTIFIER)", query.ToString().ToUpperInvariant());
                }
            }

            [Fact]
            public void GuidConvert_translated_to_correct_function_in_database()
            {
                using (var context = new ArubaContext())
                {
                    var query = context.AllTypes.Select(o => new Guid(o.c13_varchar_512_));
                    Assert.Contains("CAST( [EXTENT1].[C13_VARCHAR_512_] AS UNIQUEIDENTIFIER)", query.ToString().ToUpperInvariant());
                }
            }
        }
    }
}