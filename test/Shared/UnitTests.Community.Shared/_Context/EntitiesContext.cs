using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTests.Community.Shared._Context
{
    public class EntitiesContext : DbContext
    {
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<Shipper> Shippers { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().HasRequired(x => x.Supplier).WithOptional(x => x.Product);
            base.OnModelCreating(modelBuilder);
        }

        public static void DeleteAll<T>(EntitiesContext ctx, Func<EntitiesContext, DbSet<T>> func) where T : class
        {
            var sets = func(ctx);
            sets.RemoveRange(sets);
        }

        public static void DeleteAll<T>(Func<EntitiesContext, DbSet<T>> func) where T : class
        {
            var ctx = new EntitiesContext();
            var sets = func(ctx);
            sets.RemoveRange(sets);
            ctx.SaveChanges();

            Assert.AreEqual(0, sets.Count());
        }

        public static void InsertFactory<T>(T item, int i)
        {
            if (typeof(T) == typeof(Customer))
            {
                var customer = (Customer)(object) item;
                customer.ColumnInt = i;
                customer.ColumnString = "z_" + i;
            }
        }

        public static List<T> Insert<T>(Func<EntitiesContext, DbSet<T>> func, int count) where T : class, new()
        {
            var ctx = new EntitiesContext();
            var sets = func(ctx);


            var countBefore = sets.Count();

            var list = new List<T>();
            for (var i = 0; i < count; i++)
            {
                var item = new T();
                InsertFactory(item, i);
                list.Add(item);
            }

            sets.AddRange(list);
            ctx.SaveChanges();

            Assert.AreEqual(count + countBefore, sets.Count());

            return list;
        }


        [Table("Customer_Alias")]
        public class Customer
        {
            [Column("CustomerID_Alias")]
            public int CustomerID { get; set; }

            [Column("ColumnInt_Alias")]
            public int ColumnInt { get; set; }

            [Column("ColumnString_Alias")]
            public string ColumnString { get; set; }

            public List<Order> Orders { get; set; }
        }

        [Table("Order_Alias")]
        public class Order
        {
            [Column("OrderID_Alias")]
            public int OrderID { get; set; }

            [Column("ColumnInt_Alias")]
            public int ColumnInt { get; set; }

            [Column("ColumnString_Alias")]
            public string ColumnString { get; set; }

            public List<OrderItem> Items { get; set; }
            public Shipper Shipper { get; set; }
        }

        [Table("OrderItem_Alias")]
        public class OrderItem
        {
            [Column("OrderItemID_Alias")]
            public int OrderItemID { get; set; }

            [Column("ColumnInt_Alias")]
            public int ColumnInt { get; set; }

            [Column("ColumnString_Alias")]
            public string ColumnString { get; set; }

            public Product Product { get; set; }
        }

        [Table("Product_Alias")]
        public class Product
        {
            [Column("ProductID_Alias")]
            public int ProductID { get; set; }

            [Column("ColumnInt_Alias")]
            public int ColumnInt { get; set; }

            [Column("ColumnString_Alias")]
            public string ColumnString { get; set; }

            public Category Category { get; set; }
            public List<Tag> Tags { get; set; }
            public Supplier Supplier { get; set; }
        }

        [Table("Category_Alias")]
        public class Category
        {
            [Column("CategoryID_Alias")]
            public int CategoryID { get; set; }

            [Column("ColumnInt_Alias")]
            public int ColumnInt { get; set; }

            [Column("ColumnString_Alias")]
            public string ColumnString { get; set; }
        }

        //[Table("ProductTag_Alias")]
        //public class ProductTag
        //{
        //    [Column("ProductTagID_Alias")]
        //    public int ProductTagID { get; set; }

        //    [Column("ColumnInt_Alias")]
        //    public int ColumnInt { get; set; }

        //    [Column("ColumnString_Alias")]
        //    public string ColumnString { get; set; }
        //}

        [Table("Tag_Alias")]
        public class Tag
        {
            [Column("TagID_Alias")]
            public int TagID { get; set; }

            [Column("ColumnInt_Alias")]
            public int ColumnInt { get; set; }

            [Column("ColumnString_Alias")]
            public string ColumnString { get; set; }

            public List<Product> Products { get; set; }
        }

        [Table("Supplier_Alias")]
        public class Supplier
        {
            [Column("SupplierID_Alias")]
            public int SupplierID { get; set; }

            [Column("ColumnInt_Alias")]
            public int ColumnInt { get; set; }

            [Column("ColumnString_Alias")]
            public string ColumnString { get; set; }

            public Product Product { get; set; }
        }

        [Table("Shipper_Alias")]
        public class Shipper
        {
            [Column("ShipperID_Alias")]
            public int ShipperID { get; set; }

            [Column("ColumnInt_Alias")]
            public int ColumnInt { get; set; }

            [Column("ColumnString_Alias")]
            public string ColumnString { get; set; }
        }
    }
}