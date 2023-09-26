using Dapper.Contrib.Extensions;
using Features.TaskItems;

namespace Features.IntegrationTests.TaskItems;

public class CreateTaskTests : UnitOfWorkTestBase
{ 
    [Fact]
    public async Task ShouldReturnAValidTask()
    {
        var createCommand = new CreateTaskCommand {Title = "Make breakfast"};
        var sut = new CreateTaskCommandHandler(UnitOfWork);
        
        var task = await sut.Handle(createCommand, new CancellationToken());
        await UnitOfWork.Save(CancellationToken.None);

        task.ShouldNotBeNull();
        task.Title.ShouldBe("Make breakfast");
        task.Id.ShouldNotBe(Guid.Empty);
        task.Done.ShouldBeFalse();
    }
    
    [Fact]
    public async Task ShouldThrowExceptionWhenTitleIsEmpty()
    {
        var createCommand = new CreateTaskCommand {Title = ""};
        var sut = new CreateTaskCommandHandler(UnitOfWork);
        
        await Should.ThrowAsync<ArgumentException>(async () => 
            await sut.Handle(createCommand, new CancellationToken()));
    }
    

    public CreateTaskTests(ITestOutputHelper output) : base(output)
    {
    }
}