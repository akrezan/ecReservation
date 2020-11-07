namespace ec.Reservation.Entities.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addmigrationdeletPassUser : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Users", "Password");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Users", "Password", c => c.String());
        }
    }
}
