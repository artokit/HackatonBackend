using FluentMigrator;

namespace EducationService.Migrations;
[Migration(5)]
public class M0004_AddProgressMigration: Migration 
{
    public override void Up()
    {
        Create.Table("Progress")
            .WithColumn("UserId").AsInt64().NotNullable()
            .WithColumn("TaskId").AsInt64().NotNullable();
        Create.ForeignKey().FromTable("Progress").ForeignColumn("UserId").ToTable("users").PrimaryColumn("Id");
        Create.ForeignKey().FromTable("Progress").ForeignColumn("TaskId").ToTable("Tasks").PrimaryColumn("Id");
    }
    public override void Down()
    {
        Delete.Table("Progress");
    }
}
