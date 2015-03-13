namespace SupermarketsChain.Data.Migrations
{
    using System.Data.Entity.Migrations;

    using SupermarketsChain.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<SupermarketsChain.Data.SupermarketsChainDbContext>
    {
        public Configuration()
        {
            this.AutomaticMigrationsEnabled = true;
            this.AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(SupermarketsChain.Data.SupermarketsChainDbContext context)
        {
            context.Measures.Add(new Measure() { Name = "kilogram" });
        }
    }
}
