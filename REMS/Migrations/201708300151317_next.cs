namespace REMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class next : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.OwnerComplexes", newName: "ComplexOwners");
            DropPrimaryKey("dbo.ComplexOwners");
            AddPrimaryKey("dbo.ComplexOwners", new[] { "Complex_Id", "Owner_Id" });
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.ComplexOwners");
            AddPrimaryKey("dbo.ComplexOwners", new[] { "Owner_Id", "Complex_Id" });
            RenameTable(name: "dbo.ComplexOwners", newName: "OwnerComplexes");
        }
    }
}
