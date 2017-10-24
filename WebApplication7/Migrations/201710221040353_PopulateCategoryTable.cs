namespace WebApplication7.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PopulateCategoryTable : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO CATEGORIES (ID, NAME) VALUES (1, 'NEWS')");
            Sql("INSERT INTO CATEGORIES (ID, NAME) VALUES (2, 'SPORT')");
            Sql("INSERT INTO CATEGORIES (ID, NAME) VALUES (3, 'WEATHER')");
            Sql("INSERT INTO CATEGORIES (ID, NAME) VALUES (4, 'MUSIC')");
        }
        
        public override void Down()
        {
        }
    }
}
