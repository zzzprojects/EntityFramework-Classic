using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;

namespace UnitTests.Community.Shared._Context
{
    public class TablePerConcretContext : DbContext
    {
        public abstract class Billing
        {
            [Column("BillingID_Alias")]
            public int BillingID { get; set; }

            [Column("BillingColumnInt_Alias")]
            public int BillingColumnInt { get; set; }

            [Column("BillingColumnString_Alias")]
            public string BillingColumnString { get; set; }
        }

        [Table("TablePerConcretContext_BankAccount_Alias")]
        public class BankAccount : Billing
        {
            [Column("BankAccountColumnInt_Alias")]
            public int BankAccountColumnInt { get; set; }

            [Column("BankAccountColumnString_Alias")]
            public string BankAccountColumnString { get; set; }
        }

        [Table("TablePerConcretContext_CreditCard_Alias")]
        public class CreditCard : Billing
        {
            [Column("CreditCardColumnInt_Alias")]
            public int CreditCardColumnInt { get; set; }

            [Column("CreditCardColumnString_Alias")]
            public string CreditCardColumnString { get; set; }
        }

        [Table("TablePerConcretContext_MasterCard_Alias")]
        public class MasterCard : CreditCard
        {
            [Column("MasterCardColumnInt_Alias")]
            public int MasterCardColumnInt { get; set; }

            [Column("MasterCardColumnString_Alias")]
            public string MasterCardColumnString { get; set; }
        }
    }
}