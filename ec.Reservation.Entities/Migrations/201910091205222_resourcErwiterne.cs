namespace ec.Reservation.Entities.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class resourcErwiterne : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Resources", "Code", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Resources", "Code");
        }
    }
}
