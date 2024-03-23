using FluentMigrator;
using FluentMigrator.Oracle;

namespace EducationService.Migrations;

[Migration(3)]
public class M0002_AddAchievementMIgration: Migration
{
    public override void Up()
    {
        Create.Table("Achievements")
            .WithColumn("Id").AsInt64().NotNullable().Identity().PrimaryKey()
            .WithColumn("Photo").AsString().Nullable()
            .WithColumn("Name").AsString().NotNullable()
            .WithColumn("Description").AsString();
        Create.Table("Portfolio")
            .WithColumn("AchievementId").AsInt64().NotNullable()
            .WithColumn("UserId").AsInt64().NotNullable();
        Create.ForeignKey().FromTable("Portfolio").ForeignColumn("AchievementId").ToTable("Achievements")
            .PrimaryColumn("Id");
        Create.ForeignKey().FromTable("Portfolio").ForeignColumn("UserId").ToTable("users").PrimaryColumn("Id");
    }

    public override void Down()
    {
        Delete.Table("Achievement");
    }
}
