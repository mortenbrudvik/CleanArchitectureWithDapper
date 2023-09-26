using Features.TaskItems;
using Infrastructure;

namespace Features.IntegrationTests.TaskItems;

public class GetTaskTests : UnitOfWorkTestBase
{
    [Fact]
    public async Task ShouldReturnAValidTask()
    {
        var createCommand = new CreateTaskCommand {Title = "Make breakfast"};
        var createTaskCommandHandler = new CreateTaskCommandHandler(UnitOfWork);
        var createdTask = await createTaskCommandHandler.Handle(createCommand, new CancellationToken());
        await UnitOfWork.Save(CancellationToken.None);
        
        var task = await new GetTaskQueryHandler(UnitOfWork)
            .Handle(new GetTaskQuery {Id = createdTask.Id}, new CancellationToken());

        task.ShouldNotBeNull();
        task.Title.ShouldBe("Make breakfast");
        task.Id.ShouldNotBe(Guid.Empty);
        task.Done.ShouldBeFalse();
    }

    public GetTaskTests(ITestOutputHelper output) : base(output)
    {
    }
}