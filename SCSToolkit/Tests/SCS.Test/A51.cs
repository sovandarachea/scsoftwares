using SCS.Test.EFM;

namespace SCS.Test
{
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class A51 : DbContext
    {
        // Your context has been configured to use a 'A51' connection string from your application's 
        // configuration file (App.config or Web.config). By default, this connection string targets the 
        // 'SCS.Test.A51' database on your LocalDb instance. 
        // 
        // If you wish to target a different database and/or database provider, modify the 'A51' 
        // connection string in the application configuration file.
        public A51()
            : base("name=A51")
        {
        }

        // Add a DbSet for each entity type that you want to include in your model. For more information 
        // on configuring and using a Code First model, see http://go.microsoft.com/fwlink/?LinkId=390109.

        // public virtual DbSet<MyEntity> MyEntities { get; set; }
        public virtual DbSet<FileLink> FileLinks { get; set; }
        public virtual DbSet<FileLoader> FileLoaders { get; set; }
    }

    //public class MyEntity
    //{
    //    public int Id { get; set; }
    //    public string Name { get; set; }
    //}
}