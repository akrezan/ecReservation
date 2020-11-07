namespace ec.Reservation.Entities.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class makeIdLong : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Reservations", "CreatorId", "dbo.Users");
            DropForeignKey("dbo.Polls", "ReservationId", "dbo.Reservations");
            DropForeignKey("dbo.Reservations", "ResourceId", "dbo.Resources");
            DropForeignKey("dbo.Polls", "UserId", "dbo.Users");
            DropForeignKey("dbo.ReservationCheckLists", "CheckList_Id", "dbo.CheckLists");
            DropForeignKey("dbo.UserReservations", "ReservationId", "dbo.Reservations");
            DropForeignKey("dbo.ReservationCheckLists", "Reservation_Id", "dbo.Reservations");
            DropForeignKey("dbo.UserReservations", "UserId", "dbo.Users");
            DropIndex("dbo.Reservations", new[] { "ResourceId" });
            DropIndex("dbo.Reservations", new[] { "CreatorId" });
            DropIndex("dbo.Polls", new[] { "UserId" });
            DropIndex("dbo.Polls", new[] { "ReservationId" });
            DropIndex("dbo.UserReservations", new[] { "ReservationId" });
            DropIndex("dbo.UserReservations", new[] { "UserId" });
            DropIndex("dbo.ReservationCheckLists", new[] { "Reservation_Id" });
            DropIndex("dbo.ReservationCheckLists", new[] { "CheckList_Id" });
            DropPrimaryKey("dbo.CheckLists");
            DropPrimaryKey("dbo.Reservations");
            DropPrimaryKey("dbo.Users");
            DropPrimaryKey("dbo.Polls");
            DropPrimaryKey("dbo.Resources");
            DropPrimaryKey("dbo.ReservationCheckLists");
            DropPrimaryKey("dbo.UserReservations");
            AddColumn("dbo.Reservations", "Creator_Id", c => c.Long());
            AddColumn("dbo.Reservations", "Resource_Id", c => c.Long());
            AddColumn("dbo.Polls", "Reservation_Id", c => c.Long());
            AddColumn("dbo.Polls", "User_Id", c => c.Long());
            AlterColumn("dbo.CheckLists", "Id", c => c.Long(nullable: false, identity: true));
            AlterColumn("dbo.Reservations", "Id", c => c.Long(nullable: false, identity: true));
            AlterColumn("dbo.Users", "Id", c => c.Long(nullable: false, identity: true));
            AlterColumn("dbo.Polls", "Id", c => c.Long(nullable: false, identity: true));
            AlterColumn("dbo.Resources", "Id", c => c.Long(nullable: false, identity: true));
            AlterColumn("dbo.ReservationCheckLists", "Reservation_Id", c => c.Long(nullable: false));
            AlterColumn("dbo.ReservationCheckLists", "CheckList_Id", c => c.Long(nullable: false));
            AlterColumn("dbo.UserReservations", "ReservationId", c => c.Long(nullable: false));
            AlterColumn("dbo.UserReservations", "UserId", c => c.Long(nullable: false));
            AddPrimaryKey("dbo.CheckLists", "Id");
            AddPrimaryKey("dbo.Reservations", "Id");
            AddPrimaryKey("dbo.Users", "Id");
            AddPrimaryKey("dbo.Polls", "Id");
            AddPrimaryKey("dbo.Resources", "Id");
            AddPrimaryKey("dbo.ReservationCheckLists", new[] { "Reservation_Id", "CheckList_Id" });
            AddPrimaryKey("dbo.UserReservations", new[] { "ReservationId", "UserId" });
            CreateIndex("dbo.Reservations", "Creator_Id");
            CreateIndex("dbo.Reservations", "Resource_Id");
            CreateIndex("dbo.Polls", "Reservation_Id");
            CreateIndex("dbo.Polls", "User_Id");
            CreateIndex("dbo.UserReservations", "ReservationId");
            CreateIndex("dbo.UserReservations", "UserId");
            CreateIndex("dbo.ReservationCheckLists", "Reservation_Id");
            CreateIndex("dbo.ReservationCheckLists", "CheckList_Id");
            AddForeignKey("dbo.Reservations", "Creator_Id", "dbo.Users", "Id");
            AddForeignKey("dbo.Polls", "Reservation_Id", "dbo.Reservations", "Id");
            AddForeignKey("dbo.Reservations", "Resource_Id", "dbo.Resources", "Id");
            AddForeignKey("dbo.Polls", "User_Id", "dbo.Users", "Id");
            AddForeignKey("dbo.ReservationCheckLists", "CheckList_Id", "dbo.CheckLists", "Id", cascadeDelete: true);
            AddForeignKey("dbo.UserReservations", "ReservationId", "dbo.Reservations", "Id", cascadeDelete: true);
            AddForeignKey("dbo.ReservationCheckLists", "Reservation_Id", "dbo.Reservations", "Id", cascadeDelete: true);
            AddForeignKey("dbo.UserReservations", "UserId", "dbo.Users", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserReservations", "UserId", "dbo.Users");
            DropForeignKey("dbo.ReservationCheckLists", "Reservation_Id", "dbo.Reservations");
            DropForeignKey("dbo.UserReservations", "ReservationId", "dbo.Reservations");
            DropForeignKey("dbo.ReservationCheckLists", "CheckList_Id", "dbo.CheckLists");
            DropForeignKey("dbo.Polls", "User_Id", "dbo.Users");
            DropForeignKey("dbo.Reservations", "Resource_Id", "dbo.Resources");
            DropForeignKey("dbo.Polls", "Reservation_Id", "dbo.Reservations");
            DropForeignKey("dbo.Reservations", "Creator_Id", "dbo.Users");
            DropIndex("dbo.ReservationCheckLists", new[] { "CheckList_Id" });
            DropIndex("dbo.ReservationCheckLists", new[] { "Reservation_Id" });
            DropIndex("dbo.UserReservations", new[] { "UserId" });
            DropIndex("dbo.UserReservations", new[] { "ReservationId" });
            DropIndex("dbo.Polls", new[] { "User_Id" });
            DropIndex("dbo.Polls", new[] { "Reservation_Id" });
            DropIndex("dbo.Reservations", new[] { "Resource_Id" });
            DropIndex("dbo.Reservations", new[] { "Creator_Id" });
            DropPrimaryKey("dbo.UserReservations");
            DropPrimaryKey("dbo.ReservationCheckLists");
            DropPrimaryKey("dbo.Resources");
            DropPrimaryKey("dbo.Polls");
            DropPrimaryKey("dbo.Users");
            DropPrimaryKey("dbo.Reservations");
            DropPrimaryKey("dbo.CheckLists");
            AlterColumn("dbo.UserReservations", "UserId", c => c.Int(nullable: false));
            AlterColumn("dbo.UserReservations", "ReservationId", c => c.Int(nullable: false));
            AlterColumn("dbo.ReservationCheckLists", "CheckList_Id", c => c.Int(nullable: false));
            AlterColumn("dbo.ReservationCheckLists", "Reservation_Id", c => c.Int(nullable: false));
            AlterColumn("dbo.Resources", "Id", c => c.Int(nullable: false, identity: true));
            AlterColumn("dbo.Polls", "Id", c => c.Int(nullable: false, identity: true));
            AlterColumn("dbo.Users", "Id", c => c.Int(nullable: false, identity: true));
            AlterColumn("dbo.Reservations", "Id", c => c.Int(nullable: false, identity: true));
            AlterColumn("dbo.CheckLists", "Id", c => c.Int(nullable: false, identity: true));
            DropColumn("dbo.Polls", "User_Id");
            DropColumn("dbo.Polls", "Reservation_Id");
            DropColumn("dbo.Reservations", "Resource_Id");
            DropColumn("dbo.Reservations", "Creator_Id");
            AddPrimaryKey("dbo.UserReservations", new[] { "ReservationId", "UserId" });
            AddPrimaryKey("dbo.ReservationCheckLists", new[] { "Reservation_Id", "CheckList_Id" });
            AddPrimaryKey("dbo.Resources", "Id");
            AddPrimaryKey("dbo.Polls", "Id");
            AddPrimaryKey("dbo.Users", "Id");
            AddPrimaryKey("dbo.Reservations", "Id");
            AddPrimaryKey("dbo.CheckLists", "Id");
            CreateIndex("dbo.ReservationCheckLists", "CheckList_Id");
            CreateIndex("dbo.ReservationCheckLists", "Reservation_Id");
            CreateIndex("dbo.UserReservations", "UserId");
            CreateIndex("dbo.UserReservations", "ReservationId");
            CreateIndex("dbo.Polls", "ReservationId");
            CreateIndex("dbo.Polls", "UserId");
            CreateIndex("dbo.Reservations", "CreatorId");
            CreateIndex("dbo.Reservations", "ResourceId");
            AddForeignKey("dbo.UserReservations", "UserId", "dbo.Users", "Id", cascadeDelete: true);
            AddForeignKey("dbo.ReservationCheckLists", "Reservation_Id", "dbo.Reservations", "Id", cascadeDelete: true);
            AddForeignKey("dbo.UserReservations", "ReservationId", "dbo.Reservations", "Id", cascadeDelete: true);
            AddForeignKey("dbo.ReservationCheckLists", "CheckList_Id", "dbo.CheckLists", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Polls", "UserId", "dbo.Users", "Id");
            AddForeignKey("dbo.Reservations", "ResourceId", "dbo.Resources", "Id");
            AddForeignKey("dbo.Polls", "ReservationId", "dbo.Reservations", "Id");
            AddForeignKey("dbo.Reservations", "CreatorId", "dbo.Users", "Id");
        }
    }
}
