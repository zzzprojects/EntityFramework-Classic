using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;

namespace UnitTests.Community.Shared._Context
{
    public class ComplexTypeContext : DbContext
    {
        [ComplexType]
        public class Address
        {
            public int ColumnInt { get; set; }

            public string ColumnString { get; set; }
        }

        [ComplexType]
        public class AddressType
        {
            public int ColumnInt { get; set; }

            public string ColumnString { get; set; }

            public Address Business { get; set; }
            public Address Home { get; set; }
        }

        [Table("ComplexTypeContext_Customer_Alias")]
        public class Customer
        {
            [Column("CustomerID_Alias")]
            public int CustomerID { get; set; }

            [Column("ColumnInt_Alias")]
            public int ColumnInt { get; set; }

            [Column("ColumnString_Alias")]
            public string ColumnString { get; set; }

            public AddressType AddressType { get; set; }
        }

        [Table("ComplexTypeContext_Employee_Alias")]
        public class Employee
        {
            [Column("CustomerID_Alias")]
            public int CustomerID { get; set; }

            [Column("ColumnInt_Alias")]
            public int ColumnInt { get; set; }

            [Column("ColumnString_Alias")]
            public string ColumnString { get; set; }

            public Address HomeAddress { get; set; }
        }
    }
}