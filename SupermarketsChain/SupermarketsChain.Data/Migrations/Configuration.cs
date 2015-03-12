namespace SupermarketsChain.Data.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    using SupermarketsChain.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<SupermarketsChain.Data.SupermarketsChainDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(SupermarketsChain.Data.SupermarketsChainDbContext context)
        {
            context.Measures.Add(new Measure() { Name = "kilogram" });
        }
    }
}
