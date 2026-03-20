using Forum.Web.Framework.Path;

namespace Tests.Forum.Web.Framework;

public class PathExtentionsTests
{
    [Theory]
    [InlineData("http://localhost:5292/", "http://localhost:5292/", "CurrentPage")]
    [InlineData("http://localhost:5292/?CurrentPage=2", "http://localhost:5292/", "CurrentPage")]
    [InlineData("http://localhost:5292/?CurrentPage=2&PageSize=2", "http://localhost:5292/?PageSize=2", "CurrentPage")]
    [InlineData("http://localhost:5292/?CurrentPage=2&PageSize=2", "http://localhost:5292/?CurrentPage=2", "PageSize")]
    [InlineData("http://localhost:5292/?CurrentPage=2&PageSize=2&Something=123",
        "http://localhost:5292/?CurrentPage=2&Something=123", "PageSize")]
    public void RemoveQueryParam(string path, string expected, string param)
    {
        var result = path.RemoveQueryParams(param);

        Assert.Equal(result, expected);
    }

    [Theory]
    [InlineData("http://localhost:5292/?CurrentPage=2&PageSize=2&Something=123",
        "http://localhost:5292/?PageSize=2", "CurrentPage", "Something")]
    [InlineData("http://localhost:5292/?CurrentPage=2&PageSize=2&Something=123",
        "http://localhost:5292/?Something=123", "PageSize", "CurrentPage")]
    [InlineData("http://localhost:5292/?CurrentPage=2&PageSize=2&Something=123",
        "http://localhost:5292/", "Something", "CurrentPage", "PageSize")]
    public void RemoveQueryParams(string path, string expected, params string[] parameters)
    {
        var result = path.RemoveQueryParams(parameters);

        Assert.Equal(result, expected);
    }

    [Theory]
    [InlineData("http://localhost:5292/", "")]
    public void RemoveQueryParam_ThrowsIfEmpty(string path, string param)
    {
        Assert.Throws<ArgumentException>(() => path.RemoveQueryParams(param));
    }

    [Theory]
    [InlineData("http://localhost:5292/", null)]
    public void RemoveQueryParam_ThrowsIfNull(string path, string param)
    {
        Assert.Throws<ArgumentNullException>(() => path.RemoveQueryParams(param));
    }
}