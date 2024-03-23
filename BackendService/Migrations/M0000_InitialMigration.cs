using FluentMigrator;
using FluentMigrator.Snowflake;

namespace EducationService.Migrations;

[Migration(1)]
public class M0000_InitialMigration : Migration
{
    public override void Up()
    {
        Create.Table("users")
            .WithColumn("Id").AsInt64().Identity().NotNullable().PrimaryKey()
            .WithColumn("Username").AsString().Unique().NotNullable()
            .WithColumn("Photo").AsString().Nullable().WithDefaultValue(null)
            .WithColumn("Password").AsString().NotNullable()
            .WithColumn("Email").AsString().Unique().NotNullable()
            .WithColumn("RatingScore").AsInt64().NotNullable().WithDefaultValue(0)
            .WithColumn("Role").AsString().NotNullable().WithDefaultValue("user");
    }

    public override void Down()
    {
        Delete.Table("users");
    }
}
