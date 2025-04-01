namespace Tasks.Tests;

using Tasks;

public class Basic
{
    [Fact]
    public void TestNothing()  // essentially just confirming test runs successfully
    {
        int testNum = 2;
        Assert.Equal(4, testNum + 2);
    }
}