namespace REMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class First : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Addresses",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Address1 = c.String(),
                        Address2 = c.String(),
                        City = c.String(),
                        State = c.String(),
                        Zip = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Complexes",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(),
                        AddressId = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Addresses", t => t.AddressId)
                .Index(t => t.AddressId);
            
            CreateTable(
                "dbo.Owners",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(),
                        ContactInfoId = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Contacts", t => t.ContactInfoId)
                .Index(t => t.ContactInfoId);
            
            CreateTable(
                "dbo.Contacts",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        AddressId = c.Guid(nullable: false),
                        Phone1 = c.String(),
                        Phone2 = c.String(),
                        Email = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Addresses", t => t.AddressId, cascadeDelete: true)
                .Index(t => t.AddressId);
            
            CreateTable(
                "dbo.StaffMembers",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        UserId = c.String(),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Complex_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Complexes", t => t.Complex_Id)
                .Index(t => t.Complex_Id);
            
            CreateTable(
                "dbo.Units",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(),
                        ComplexId = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Complexes", t => t.ComplexId)
                .Index(t => t.ComplexId);
            
            CreateTable(
                "dbo.Tenants",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        UserId = c.String(),
                        FirstName = c.String(),
                        LastName = c.String(),
                        ContactInfoId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Contacts", t => t.ContactInfoId, cascadeDelete: true)
                .Index(t => t.ContactInfoId);
            
            CreateTable(
                "dbo.OwnerComplexes",
                c => new
                    {
                        Owner_Id = c.Guid(nullable: false),
                        Complex_Id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.Owner_Id, t.Complex_Id })
                .ForeignKey("dbo.Owners", t => t.Owner_Id)
                .ForeignKey("dbo.Complexes", t => t.Complex_Id)
                .Index(t => t.Owner_Id)
                .Index(t => t.Complex_Id);
            
            CreateTable(
                "dbo.StaffMemberOwners",
                c => new
                    {
                        StaffMember_Id = c.Guid(nullable: false),
                        Owner_Id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.StaffMember_Id, t.Owner_Id })
                .ForeignKey("dbo.StaffMembers", t => t.StaffMember_Id)
                .ForeignKey("dbo.Owners", t => t.Owner_Id)
                .Index(t => t.StaffMember_Id)
                .Index(t => t.Owner_Id);
            
            CreateTable(
                "dbo.TenantUnits",
                c => new
                    {
                        Tenant_Id = c.Guid(nullable: false),
                        Unit_Id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.Tenant_Id, t.Unit_Id })
                .ForeignKey("dbo.Tenants", t => t.Tenant_Id)
                .ForeignKey("dbo.Units", t => t.Unit_Id)
                .Index(t => t.Tenant_Id)
                .Index(t => t.Unit_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TenantUnits", "Unit_Id", "dbo.Units");
            DropForeignKey("dbo.TenantUnits", "Tenant_Id", "dbo.Tenants");
            DropForeignKey("dbo.Tenants", "ContactInfoId", "dbo.Contacts");
            DropForeignKey("dbo.Units", "ComplexId", "dbo.Complexes");
            DropForeignKey("dbo.StaffMembers", "Complex_Id", "dbo.Complexes");
            DropForeignKey("dbo.StaffMemberOwners", "Owner_Id", "dbo.Owners");
            DropForeignKey("dbo.StaffMemberOwners", "StaffMember_Id", "dbo.StaffMembers");
            DropForeignKey("dbo.Owners", "ContactInfoId", "dbo.Contacts");
            DropForeignKey("dbo.Contacts", "AddressId", "dbo.Addresses");
            DropForeignKey("dbo.OwnerComplexes", "Complex_Id", "dbo.Complexes");
            DropForeignKey("dbo.OwnerComplexes", "Owner_Id", "dbo.Owners");
            DropForeignKey("dbo.Complexes", "AddressId", "dbo.Addresses");
            DropIndex("dbo.TenantUnits", new[] { "Unit_Id" });
            DropIndex("dbo.TenantUnits", new[] { "Tenant_Id" });
            DropIndex("dbo.StaffMemberOwners", new[] { "Owner_Id" });
            DropIndex("dbo.StaffMemberOwners", new[] { "StaffMember_Id" });
            DropIndex("dbo.OwnerComplexes", new[] { "Complex_Id" });
            DropIndex("dbo.OwnerComplexes", new[] { "Owner_Id" });
            DropIndex("dbo.Tenants", new[] { "ContactInfoId" });
            DropIndex("dbo.Units", new[] { "ComplexId" });
            DropIndex("dbo.StaffMembers", new[] { "Complex_Id" });
            DropIndex("dbo.Contacts", new[] { "AddressId" });
            DropIndex("dbo.Owners", new[] { "ContactInfoId" });
            DropIndex("dbo.Complexes", new[] { "AddressId" });
            DropTable("dbo.TenantUnits");
            DropTable("dbo.StaffMemberOwners");
            DropTable("dbo.OwnerComplexes");
            DropTable("dbo.Tenants");
            DropTable("dbo.Units");
            DropTable("dbo.StaffMembers");
            DropTable("dbo.Contacts");
            DropTable("dbo.Owners");
            DropTable("dbo.Complexes");
            DropTable("dbo.Addresses");
        }
    }
}
