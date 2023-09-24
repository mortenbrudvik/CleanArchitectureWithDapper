using Dapper.Contrib.Extensions;
using Features.TaskItems;

namespace Features.IntegrationTests.TaskItems;

public class CreateTaskTests : TestBase
{ 
    [Fact]
    public async Task ShouldRequireMinimumFields()
    {
        var createCommand = new CreateTaskCommand {Title = "Make breakfast"};
        var createHandler = new CreateTaskCommandHandler(UnitOfWork);
        
        await createHandler.Handle(createCommand, new CancellationToken());
        await UnitOfWork.Save(CancellationToken.None);

        var tasks = await UnitOfWork.Tasks.GetAll();

        tasks.ShouldNotBeNull();
        tasks.Count.ShouldBe(1);
        tasks.First().Title.ShouldBe("Make breakfast");
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