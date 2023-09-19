using Application.Features.TaskItems;
using Shouldly;
using Xunit.Abstractions;

namespace Application.IntegrationTests;

public class CreateTaskTests : TestBase
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

    public CreateTaskTests(ITestOutputHelper output) : base(output)
    {
    }
}