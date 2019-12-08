using FluentMigrator;

namespace Transactions.Infrastructure.Migrations.MySQL
{
    [Migration(2)]
    public class InsertMasterData : Migration
    {
        public override void Up()
        {
            Execute.EmbeddedScript("2_InsertMasterData.sql");
        }

        public override void Down()
        {
        }
    }
}