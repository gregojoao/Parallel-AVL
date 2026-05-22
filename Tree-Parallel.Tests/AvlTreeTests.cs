using Tree_Parallel.Application;
using Tree_Parallel.Models.TreeAVL;

namespace Tree_Parallel.Tests;

public class AvlTreeTests
{
    [Fact]
    public async Task InsertAndSearch_FindsExistingValuesOnly()
    {
        AVL tree = new AVL();
        tree.Initialize();

        foreach (long value in new long[] { 10, 5, 15, 12, 20 })
        {
            await tree.InsertIntoTree(value, string.Empty);
        }

        Assert.True(await tree.DoSearchInTree2(tree.Root, 12));
        Assert.True(await tree.DoSearchInTree2(tree.Root, 20));
        Assert.False(await tree.DoSearchInTree2(tree.Root, 99));
        Assert.False(await tree.DoSearchInTree2(null!, 10));
    }

    [Fact]
    public void TreeDataFileService_CreatesAndReadsOrderedValues()
    {
        string path = Path.Combine(Path.GetTempPath(), $"{Guid.NewGuid():N}.txt");
        TreeDataFileService service = new TreeDataFileService();

        try
        {
            service.CreateOrderedFile(path, 5);

            Assert.Equal(new long[] { 1, 2, 3, 4, 5 }, service.ReadValues(path));
        }
        finally
        {
            if (File.Exists(path))
                File.Delete(path);
        }
    }

    [Fact]
    public void TreeDataFileService_IgnoresInvalidLines()
    {
        string path = Path.Combine(Path.GetTempPath(), $"{Guid.NewGuid():N}.txt");

        try
        {
            File.WriteAllLines(path, new[] { "1", "abc", "3" });

            TreeDataFileService service = new TreeDataFileService();

            Assert.Equal(new long[] { 1, 3 }, service.ReadValues(path));
        }
        finally
        {
            if (File.Exists(path))
                File.Delete(path);
        }
    }

    [Fact]
    public void TreeDataFileService_ReadsVersionedTestData()
    {
        string path = Path.Combine(AppContext.BaseDirectory, "TestData", "20.txt");
        TreeDataFileService service = new TreeDataFileService();

        var values = service.ReadValues(path).ToArray();

        Assert.Equal(20, values.Length);
        Assert.Equal(70299144, values[0]);
        Assert.Equal(49678470, values[^1]);
        Assert.All(values, value => Assert.True(value >= 0));
    }
}
