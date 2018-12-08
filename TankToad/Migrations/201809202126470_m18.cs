namespace TankToad.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class m18 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Diagnostics", "LowLevel", c => c.String());
            AlterColumn("dbo.Diagnostics", "Zeros", c => c.String());
            AlterColumn("dbo.Diagnostics", "GraceWindowExceeded", c => c.String());
            AlterColumn("dbo.Diagnostics", "LowBattery", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Diagnostics", "LowBattery", c => c.Boolean(nullable: false));
            AlterColumn("dbo.Diagnostics", "GraceWindowExceeded", c => c.Boolean(nullable: false));
            AlterColumn("dbo.Diagnostics", "Zeros", c => c.Boolean(nullable: false));
            AlterColumn("dbo.Diagnostics", "LowLevel", c => c.Boolean(nullable: false));
        }
    }
}
