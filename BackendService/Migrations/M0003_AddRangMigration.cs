using FluentMigrator;

namespace EducationService.Migrations;

[Migration(4)]
public class M0003_AddRangMigration : AutoReversingMigration
{
    public override void Up()
    {
        Create.Table("Rangs")
            .WithColumn("Id").AsInt64().Identity()
            .WithColumn("Name").AsString()
            .WithColumn("ImagePath").AsString().Nullable()
            .WithColumn("MinScore").AsInt64().NotNullable()
            .WithColumn("MaxScore").AsInt64().NotNullable();

        Alter.Table("users").AddColumn("RangId").AsInt64().WithDefaultValue(0); // TODO: WithDefault убрать на проде
        
        Create.ForeignKey()
            .FromTable("users").ForeignColumn("RangId")
            .ToTable("Rangs").PrimaryColumn("Id");
    }
}
