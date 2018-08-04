using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Text;

namespace UnitTests.Community.Shared._Context
{
    public class TablePerHierachyContext : DbContext
    {
        [Table("TablePerHierachyContext_Billing_Alias")]
        public class Billing
        {
            [Column("BillingID_Alias")]
            public int BillingID { get; set; }

            [Column("BillingColumnInt_Alias")]
            public int BillingColumnInt { get; set; }

            [Column("BillingColumnString_Alias")]
            public string BillingColumnString { get; set; }
        }

        public class BankAccount : Billing
        {
            [Column("BankAccountColumnInt_Alias")]
            public int BankAccountColumnInt { get; set; }

            [Column("BankAccountColumnString_Alias")]
            public string BankAccountColumnString { get; set; }
        }

        public class CreditCard : Billing
        {
            [Column("CreditCardColumnInt_Alias")]
            public int CreditCardColumnInt { get; set; }

            [Column("CreditCardColumnString_Alias")]
            public string CreditCardColumnString { get; set; }
        }

        public class MasterCard : CreditCard
        {
            [Column("MasterCardColumnInt_Alias")]
            public int MasterCardColumnInt { get; set; }

            [Column("MasterCardColumnString_Alias")]
            public string MasterCardColumnString { get; set; }
        }
    }
}
