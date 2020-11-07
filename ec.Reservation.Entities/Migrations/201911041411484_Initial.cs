namespace ec.Reservation.Entities.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CheckLists",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(),
                        Details = c.String(),
                        CreationDateTime = c.DateTime(nullable: false),
                        ModifiedDateTime = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Reservations",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(),
                        StartTime = c.DateTime(nullable: false),
                        EndTime = c.DateTime(nullable: false),
                        ResourceId = c.Long(nullable: false),
                        CreatorId = c.Long(nullable: false),
                        CreationDateTime = c.DateTime(nullable: false),
                        ModifiedDateTime = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.CreatorId)
                .ForeignKey("dbo.Resources", t => t.ResourceId)
                .Index(t => t.ResourceId)
                .Index(t => t.CreatorId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                        UserName = c.String(),
                        Password = c.String(),
                        Email = c.String(),
                        UserType = c.Int(nullable: false),
                        CreationDateTime = c.DateTime(nullable: false),
                        ModifiedDateTime = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Polls",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Answer = c.Int(nullable: false),
                        Comment = c.String(),
                        UserId = c.Long(nullable: false),
                        ReservationId = c.Long(nullable: false),
                        CreationDateTime = c.DateTime(nullable: false),
                        ModifiedDateTime = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Reservations", t => t.ReservationId)
                .ForeignKey("dbo.Users", t => t.UserId)
                .Index(t => t.UserId)
                .Index(t => t.ReservationId);
            
            CreateTable(
                "dbo.Resources",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(),
                        Code = c.String(),
                        CreationDateTime = c.DateTime(nullable: false),
                        ModifiedDateTime = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.UserReservations",
                c => new
                    {
                        ReservationId = c.Long(nullable: false),
                        UserId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => new { t.ReservationId, t.UserId })
                .ForeignKey("dbo.Reservations", t => t.ReservationId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.ReservationId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.ReservationCheckLists",
                c => new
                    {
                        Reservation_Id = c.Long(nullable: false),
                        CheckList_Id = c.Long(nullable: false),
                    })
                .PrimaryKey(t => new { t.Reservation_Id, t.CheckList_Id })
                .ForeignKey("dbo.Reservations", t => t.Reservation_Id, cascadeDelete: true)
                .ForeignKey("dbo.CheckLists", t => t.CheckList_Id, cascadeDelete: true)
                .Index(t => t.Reservation_Id)
                .Index(t => t.CheckList_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Reservations", "ResourceId", "dbo.Resources");
            DropForeignKey("dbo.Reservations", "CreatorId", "dbo.Users");
            DropForeignKey("dbo.ReservationCheckLists", "CheckList_Id", "dbo.CheckLists");
            DropForeignKey("dbo.ReservationCheckLists", "Reservation_Id", "dbo.Reservations");
            DropForeignKey("dbo.UserReservations", "UserId", "dbo.Users");
            DropForeignKey("dbo.UserReservations", "ReservationId", "dbo.Reservations");
            DropForeignKey("dbo.Polls", "UserId", "dbo.Users");
            DropForeignKey("dbo.Polls", "ReservationId", "dbo.Reservations");
            DropIndex("dbo.ReservationCheckLists", new[] { "CheckList_Id" });
            DropIndex("dbo.ReservationCheckLists", new[] { "Reservation_Id" });
            DropIndex("dbo.UserReservations", new[] { "UserId" });
            DropIndex("dbo.UserReservations", new[] { "ReservationId" });
            DropIndex("dbo.Polls", new[] { "ReservationId" });
            DropIndex("dbo.Polls", new[] { "UserId" });
            DropIndex("dbo.Reservations", new[] { "CreatorId" });
            DropIndex("dbo.Reservations", new[] { "ResourceId" });
            DropTable("dbo.ReservationCheckLists");
            DropTable("dbo.UserReservations");
            DropTable("dbo.Resources");
            DropTable("dbo.Polls");
            DropTable("dbo.Users");
            DropTable("dbo.Reservations");
            DropTable("dbo.CheckLists");
        }
    }
}
