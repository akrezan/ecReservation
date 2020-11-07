namespace ec.Reservation.Entities.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SecondMigration : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.CheckLists", "ModifiedDateTime", c => c.DateTime());
            AlterColumn("dbo.Reservations", "ModifiedDateTime", c => c.DateTime());
            AlterColumn("dbo.Users", "ModifiedDateTime", c => c.DateTime());
            AlterColumn("dbo.Polls", "ModifiedDateTime", c => c.DateTime());
            AlterColumn("dbo.Resources", "ModifiedDateTime", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Resources", "ModifiedDateTime", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Polls", "ModifiedDateTime", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Users", "ModifiedDateTime", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Reservations", "ModifiedDateTime", c => c.DateTime(nullable: false));
            AlterColumn("dbo.CheckLists", "ModifiedDateTime", c => c.DateTime(nullable: false));
        }
    }
}
