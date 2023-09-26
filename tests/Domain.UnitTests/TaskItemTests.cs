using Xunit.Abstractions;

namespace Domain.UnitTests;

public class TaskItemTests : XunitContextBase
{
    [Fact]
    public void TaskWithEmptyTitleShouldThrowException()
    {
        Assert.Throws<ArgumentException>(() => new TaskItem(""));
    }
    
    [Fact]
    public void TaskWithLongTitleShouldThrowException()
    {
        Assert.Throws<ArgumentException>(() => 
            new TaskItem("Lorem ipsum dolor sit amet, consectetur adipiscing elit. Donec a diam lectus. Sed sit amet ipsum mauris. Maecenas congue ligula ac quam viverra nec consectetur ante hendrerit. Donec et mollis dolor."));
    }

    public TaskItemTests(ITestOutputHelper output) : base(output)
    {
    }
}