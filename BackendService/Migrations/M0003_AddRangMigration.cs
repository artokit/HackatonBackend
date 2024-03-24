using FluentMigrator;

namespace EducationService.Migrations;

[Migration(4)]
public class M0003_AddRangMigration : AutoReversingMigration
{
    public override void Up()
    {
        Create.Table("Rangs")
            .WithColumn("Id").AsInt64().Identity().PrimaryKey()
            .WithColumn("Name").AsString()
            .WithColumn("ImagePath").AsString().Nullable()
            .WithColumn("MinScore").AsInt64().NotNullable()
            .WithColumn("MaxScore").AsInt64().NotNullable();

        
        
        // Create.ForeignKey()
        //     .FromTable("users").ForeignColumn("RangId")
        //     .ToTable("Rangs").PrimaryColumn("Id");
    }
}
