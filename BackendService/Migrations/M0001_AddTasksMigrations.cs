using FluentMigrator;

namespace EducationService.Migrations;

[Migration(2)]
public class M0001_AddTasksMigrations : AutoReversingMigration
{
    public override void Up()
    {
        Create.Table("Categories")
            .WithColumn("Id").AsInt64().Unique().PrimaryKey().Identity()
            .WithColumn("Name").AsString();

        Create.Table("Levels")
            .WithColumn("Id").AsInt64().Unique().PrimaryKey().Identity()
            .WithColumn("Name").AsString()
            .WithColumn("Award").AsInt64();

        Create.Table("Tasks")
            .WithColumn("Id").AsInt64().Unique().PrimaryKey().Identity()
            .WithColumn("LevelId").AsInt64()
            .WithColumn("CategoryId").AsInt64()
            .WithColumn("RightAnswer").AsString()
            .WithColumn("Content").AsString()
            .WithColumn("PathFile").AsString().Nullable();

        Create.ForeignKey()
            .FromTable("Tasks").ForeignColumn("LevelId")
            .ToTable("Levels").PrimaryColumn("Id");

        Create.ForeignKey()
            .FromTable("Tasks").ForeignColumn("CategoryId")
            .ToTable("Categories").PrimaryColumn("Id");
    }
}
