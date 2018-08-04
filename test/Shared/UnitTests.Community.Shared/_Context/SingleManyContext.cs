using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace UnitTests.Community.Shared.Net45.Context.SingleMany
{
    public class SingleManyContext : DbContext
    {
        static SingleManyContext()
        {
            Seed();
        }

        public static void Seed()
        {
            // CLEAN  
            using (var context = new SingleManyContext())
            {
                context.Entity_M_M_M.RemoveRange(context.Entity_M_M_M);
                context.Entity_M_M_S.RemoveRange(context.Entity_M_M_S);
                context.Entity_M_S_M.RemoveRange(context.Entity_M_S_M);
                context.Entity_M_S_S.RemoveRange(context.Entity_M_S_S);
                context.Entity_S_M_M.RemoveRange(context.Entity_S_M_M);
                context.Entity_S_M_S.RemoveRange(context.Entity_S_M_S);
                context.Entity_S_S_M.RemoveRange(context.Entity_S_S_M);
                context.Entity_M_M.RemoveRange(context.Entity_M_M);
                context.Entity_M_S.RemoveRange(context.Entity_M_S);
                context.Entity_S_M.RemoveRange(context.Entity_S_M);
                context.Entity_S_S.RemoveRange(context.Entity_S_S);
                context.Entity_M.RemoveRange(context.Entity_M);
                context.Entity_S.RemoveRange(context.Entity_S);
                context.Entity_Root.RemoveRange(context.Entity_Root);

                context.SaveChanges();
            }

            // SEED  
            using (var context = new SingleManyContext())
            {
                var root = context.Entity_Root.Add(new Entity_Root {ColumnInt = 1, Entity_M = new List<Entity_M>()});

                // single
                {
                    var single = new Entity_S() {ColumnInt = 1, Entity_S_M = new List<Entity_S_M>()};
                    root.Entity_S = single;

                    // single_single
                    {
                        var single_single = new Entity_S_S() {ColumnInt = 1, Entity_S_S_M = new List<Entity_S_S_M>()};
                        single.Entity_S_S = single_single;

                        // single_single_single
                        {
                            single_single.Entity_S_S_S = new Entity_S_S_S() {ColumnInt = 1};
                        }

                        // single_single_many
                        {
                            for (var k = 0; k < 3; k++)
                            {
                                single_single.Entity_S_S_M.Add(new Entity_S_S_M() {ColumnInt = k});
                            }
                        }
                    }

                    // single_many
                    {
                        for (var j = 0; j < 3; j++)
                        {
                            var single_many = new Entity_S_M() {ColumnInt = j, Entity_S_M_M = new List<Entity_S_M_M>()};
                            single.Entity_S_M.Add(single_many);

                            // single_many_single
                            {
                                single_many.Entity_S_M_S = new Entity_S_M_S() {ColumnInt = j};
                            }

                            // single_many_many
                            {
                                for (var k = 0; k < 3; k++)
                                {
                                    single_many.Entity_S_M_M.Add(new Entity_S_M_M() {ColumnInt = k});
                                }
                            }
                        }
                    }
                }


                // many
                {
                    for (var i = 0; i < 3; i++)
                    {
                        var many = new Entity_M() {ColumnInt = i, Entity_M_M = new List<Entity_M_M>()};
                        root.Entity_M.Add(many);

                        // many_single
                        {
                            var many_single = new Entity_M_S() {ColumnInt = 1, Entity_M_S_M = new List<Entity_M_S_M>()};
                            many.Entity_M_S = many_single;

                            // single_single_single
                            {
                                many_single.Entity_M_S_S = new Entity_M_S_S() {ColumnInt = 1};
                            }

                            // single_single_many
                            {
                                for (var k = 0; k < 3; k++)
                                {
                                    many_single.Entity_M_S_M.Add(new Entity_M_S_M() {ColumnInt = k});
                                }
                            }
                        }

                        // many_many
                        {
                            for (var j = 0; j < 3; j++)
                            {
                                var many_many = new Entity_M_M() {ColumnInt = j, Entity_M_M_M = new List<Entity_M_M_M>()};
                                many.Entity_M_M.Add(many_many);

                                // many_many_single
                                {
                                    many_many.Entity_M_M_S = new Entity_M_M_S() {ColumnInt = j};
                                }

                                // many_many_many
                                {
                                    for (var k = 0; k < 3; k++)
                                    {
                                        many_many.Entity_M_M_M.Add(new Entity_M_M_M() {ColumnInt = k});
                                    }
                                }
                            }
                        }
                    }
                }

                context.SaveChanges();
            }
        }

        public SingleManyContext() : base("SingleManyContext")
        {
        }

        public DbSet<Entity_Root> Entity_Root { get; set; }
        public DbSet<Entity_S> Entity_S { get; set; }
        public DbSet<Entity_S_S> Entity_S_S { get; set; }
        public DbSet<Entity_S_M> Entity_S_M { get; set; }
        public DbSet<Entity_S_S_S> Entity_S_S_S { get; set; }
        public DbSet<Entity_S_S_M> Entity_S_S_M { get; set; }
        public DbSet<Entity_S_M_S> Entity_S_M_S { get; set; }
        public DbSet<Entity_S_M_M> Entity_S_M_M { get; set; }
        public DbSet<Entity_M> Entity_M { get; set; }
        public DbSet<Entity_M_S> Entity_M_S { get; set; }
        public DbSet<Entity_M_S_S> Entity_M_S_S { get; set; }
        public DbSet<Entity_M_S_M> Entity_M_S_M { get; set; }
        public DbSet<Entity_M_M> Entity_M_M { get; set; }
        public DbSet<Entity_M_M_S> Entity_M_M_S { get; set; }
        public DbSet<Entity_M_M_M> Entity_M_M_M { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Entity_Root>().HasOptional(X => X.Entity_S).WithOptionalDependent(X => X.Parent);
            modelBuilder.Entity<Entity_S>().HasOptional(X => X.Entity_S_S).WithOptionalDependent(X => X.Parent);
            modelBuilder.Entity<Entity_M>().HasOptional(X => X.Entity_M_S).WithOptionalDependent(X => X.Parent);
            modelBuilder.Entity<Entity_S_S>().HasOptional(X => X.Entity_S_S_S).WithOptionalDependent(X => X.Parent);
            modelBuilder.Entity<Entity_S_M>().HasOptional(X => X.Entity_S_M_S).WithOptionalDependent(X => X.Parent);
            modelBuilder.Entity<Entity_M_S>().HasOptional(X => X.Entity_M_S_S).WithOptionalDependent(X => X.Parent);
            modelBuilder.Entity<Entity_M_M>().HasOptional(X => X.Entity_M_M_S).WithOptionalDependent(X => X.Parent);

            base.OnModelCreating(modelBuilder);
        }
    }

    public class Entity_Root
    {
        public int ID { get; set; }
        public int ColumnInt { get; set; }
        public string ColumnString { get; set; }
        public Entity_S Entity_S { get; set; }
        public List<Entity_M> Entity_M { get; set; }
    }

    public class Entity_S
    {
        public int ID { get; set; }
        public int ColumnInt { get; set; }
        public string ColumnString { get; set; }
        public Entity_S_S Entity_S_S { get; set; }
        public List<Entity_S_M> Entity_S_M { get; set; }
        public Entity_Root Parent { get; set; }
    }

    public class Entity_M
    {
        public int ID { get; set; }
        public int ColumnInt { get; set; }
        public string ColumnString { get; set; }
        public Entity_M_S Entity_M_S { get; set; }
        public List<Entity_M_M> Entity_M_M { get; set; }
        public Entity_Root Parent { get; set; }
    }

    public class Entity_S_S
    {
        public int ID { get; set; }
        public int ColumnInt { get; set; }
        public string ColumnString { get; set; }
        public Entity_S_S_S Entity_S_S_S { get; set; }
        public List<Entity_S_S_M> Entity_S_S_M { get; set; }
        public Entity_S Parent { get; set; }
    }

    public class Entity_S_M
    {
        public int ID { get; set; }
        public int ColumnInt { get; set; }
        public string ColumnString { get; set; }
        public Entity_S_M_S Entity_S_M_S { get; set; }
        public List<Entity_S_M_M> Entity_S_M_M { get; set; }
        public Entity_S Parent { get; set; }
    }

    public class Entity_M_S
    {
        public int ID { get; set; }
        public int ColumnInt { get; set; }
        public string ColumnString { get; set; }
        public Entity_M_S_S Entity_M_S_S { get; set; }
        public List<Entity_M_S_M> Entity_M_S_M { get; set; }
        public Entity_M Parent { get; set; }
    }

    public class Entity_M_M
    {
        public int ID { get; set; }
        public int ColumnInt { get; set; }
        public string ColumnString { get; set; }
        public Entity_M_M_S Entity_M_M_S { get; set; }
        public List<Entity_M_M_M> Entity_M_M_M { get; set; }
        public Entity_M Parent { get; set; }
    }

    public class Entity_S_S_S
    {
        public int ID { get; set; }
        public int ColumnInt { get; set; }
        public string ColumnString { get; set; }
        public Entity_S_S Parent { get; set; }
    }

    public class Entity_S_S_M
    {
        public int ID { get; set; }
        public int ColumnInt { get; set; }
        public string ColumnString { get; set; }
        public Entity_S_S Parent { get; set; }
    }

    public class Entity_S_M_S
    {
        public int ID { get; set; }
        public int ColumnInt { get; set; }
        public string ColumnString { get; set; }
        public Entity_S_M Parent { get; set; }
    }

    public class Entity_S_M_M
    {
        public int ID { get; set; }
        public int ColumnInt { get; set; }
        public string ColumnString { get; set; }
        public Entity_S_M Parent { get; set; }
    }

    public class Entity_M_S_S
    {
        public int ID { get; set; }
        public int ColumnInt { get; set; }
        public string ColumnString { get; set; }
        public Entity_M_S Parent { get; set; }
    }

    public class Entity_M_S_M
    {
        public int ID { get; set; }
        public int ColumnInt { get; set; }
        public string ColumnString { get; set; }
        public Entity_M_S Parent { get; set; }
    }

    public class Entity_M_M_S
    {
        public int ID { get; set; }
        public int ColumnInt { get; set; }
        public string ColumnString { get; set; }
        public Entity_M_M Parent { get; set; }
    }

    public class Entity_M_M_M
    {
        public int ID { get; set; }
        public int ColumnInt { get; set; }
        public string ColumnString { get; set; }
        public Entity_M_M Parent { get; set; }
    }
}