using Dapper.Contrib.Extensions;
using Features.TaskItems;

namespace Features.IntegrationTests.TaskItems;

public class CreateTaskTests : UnitOfWorkFixture
{ 
    [Fact]
    public async Task ShouldRequireMinimumFields()
    {
        var createCommand = new CreateTaskCommand {Title = "Make breakfast"};
        var createHandler = new CreateTaskCommandHandler(UnitOfWork);
        
        var task = await createHandler.Handle(createCommand, new CancellationToken());
        
        var tasks = await UnitOfWork.Tasks.GetAll();

        task.ShouldNotBeNull();
        task.Title.ShouldBe(createCommand.Title);
    }
    
    [Fact]
    public async Task TestDapper()
    {
        
        Insert(    
            """
                CREATE TABLE Cars (
                    Id TEXT PRIMARY KEY,
                    Name TEXT NOT NULL,
                    Year INTEGER NOT NULL
                )
            """);
        
        var car = new Car
        {
            Id = Guid.NewGuid(),
            Name = "Ford",
            Year = 2021
        };  
        
        const string sql = "INSERT INTO Cars (Id, Name, Year) VALUES (@Id, @Name, @Year)";
        Insert(sql);
    }

    public CreateTaskTests(ITestOutputHelper output) : base(output)
    {
    }
}

public class Car
{
    [ExplicitKey]
    public Guid Id { get; set; }
    public string Name { get; set; } = "";
    public int Year { get; set; }
}