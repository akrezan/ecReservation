namespace ec.Reservation.Entities.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class repairLongIds : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Reservations", new[] { "Creator_Id" });
            DropIndex("dbo.Reservations", new[] { "Resource_Id" });
            DropIndex("dbo.Polls", new[] { "Reservation_Id" });
            DropIndex("dbo.Polls", new[] { "User_Id" });
            DropColumn("dbo.Reservations", "CreatorId");
            DropColumn("dbo.Reservations", "ResourceId");
            DropColumn("dbo.Polls", "ReservationId");
            DropColumn("dbo.Polls", "UserId");
            RenameColumn(table: "dbo.Reservations", name: "Creator_Id", newName: "CreatorId");
            RenameColumn(table: "dbo.Polls", name: "Reservation_Id", newName: "ReservationId");
            RenameColumn(table: "dbo.Reservations", name: "Resource_Id", newName: "ResourceId");
            RenameColumn(table: "dbo.Polls", name: "User_Id", newName: "UserId");
            AlterColumn("dbo.Reservations", "ResourceId", c => c.Long(nullable: false));
            AlterColumn("dbo.Reservations", "CreatorId", c => c.Long(nullable: false));
            AlterColumn("dbo.Reservations", "CreatorId", c => c.Long(nullable: false));
            AlterColumn("dbo.Reservations", "ResourceId", c => c.Long(nullable: false));
            AlterColumn("dbo.Polls", "UserId", c => c.Long(nullable: false));
            AlterColumn("dbo.Polls", "ReservationId", c => c.Long(nullable: false));
            AlterColumn("dbo.Polls", "ReservationId", c => c.Long(nullable: false));
            AlterColumn("dbo.Polls", "UserId", c => c.Long(nullable: false));
            CreateIndex("dbo.Reservations", "ResourceId");
            CreateIndex("dbo.Reservations", "CreatorId");
            CreateIndex("dbo.Polls", "UserId");
            CreateIndex("dbo.Polls", "ReservationId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Polls", new[] { "ReservationId" });
            DropIndex("dbo.Polls", new[] { "UserId" });
            DropIndex("dbo.Reservations", new[] { "CreatorId" });
            DropIndex("dbo.Reservations", new[] { "ResourceId" });
            AlterColumn("dbo.Polls", "UserId", c => c.Long());
            AlterColumn("dbo.Polls", "ReservationId", c => c.Long());
            AlterColumn("dbo.Polls", "ReservationId", c => c.Int(nullable: false));
            AlterColumn("dbo.Polls", "UserId", c => c.Int(nullable: false));
            AlterColumn("dbo.Reservations", "ResourceId", c => c.Long());
            AlterColumn("dbo.Reservations", "CreatorId", c => c.Long());
            AlterColumn("dbo.Reservations", "CreatorId", c => c.Int(nullable: false));
            AlterColumn("dbo.Reservations", "ResourceId", c => c.Int(nullable: false));
            RenameColumn(table: "dbo.Polls", name: "UserId", newName: "User_Id");
            RenameColumn(table: "dbo.Reservations", name: "ResourceId", newName: "Resource_Id");
            RenameColumn(table: "dbo.Polls", name: "ReservationId", newName: "Reservation_Id");
            RenameColumn(table: "dbo.Reservations", name: "CreatorId", newName: "Creator_Id");
            AddColumn("dbo.Polls", "UserId", c => c.Int(nullable: false));
            AddColumn("dbo.Polls", "ReservationId", c => c.Int(nullable: false));
            AddColumn("dbo.Reservations", "ResourceId", c => c.Int(nullable: false));
            AddColumn("dbo.Reservations", "CreatorId", c => c.Int(nullable: false));
            CreateIndex("dbo.Polls", "User_Id");
            CreateIndex("dbo.Polls", "Reservation_Id");
            CreateIndex("dbo.Reservations", "Resource_Id");
            CreateIndex("dbo.Reservations", "Creator_Id");
        }
    }
}
