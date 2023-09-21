using FluentMigrator;

namespace Infrastructure.Migrations;

[Migration(2023092106)]
public class AddTaskTable : Migration
{
    public override void Up()
    {
        Create.Table("Tasks")
            .WithColumn("Id").AsGuid().PrimaryKey().Identity()
            .WithColumn("Title").AsString(255).NotNullable()
            .WithColumn("Done").AsBoolean().NotNullable().WithDefaultValue(false);
    }

    public override void Down()
    {
        Delete.Table("Tasks");
    }
}