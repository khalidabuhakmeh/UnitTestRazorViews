# Unit Testing Views

The approach outlined in this repository allows folks to unit test HTML output from their ASP.NET Core applications.

Personally, I think UI changes happen frequently that its best to unit test behavior rather than UI. 

If you really want to though, this approach can also allow you to use query selectors like you would in a browser dev tool.

## Packages

You'll need to add the following packages to your unit test projects.

- AngleSharp
- Microsoft.AspNetCore.Mvc.Testing
- XUnit (or other framework)

## ClassFixture

You'll need to create an XUnit class fixture that will be injected into your unit tests.

```c#
    public class UnitTest1
        : IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly WebApplicationFactory<Startup> factory;

        public UnitTest1(WebApplicationFactory<Startup> factory)
        {
            this.factory = factory;
        }
        ...
    }
```

## HtmlExtensions

```c#
public static class HtmlExtensions
{
    public static async Task<IDocument> GetHtmlAsync(this HttpClient client, string urlPath )
    {
        var response = await client.GetStringAsync(urlPath);
        var context = BrowsingContext.New(Configuration.Default);
        var document = await context.OpenAsync(req => req.Content(response));
        return document;
    }
}
```

## The Test

```c#
[Fact]
public async Task Header_say_Welcome()
{
    var client = factory.CreateClient();
    var html = await client.GetHtmlAsync("/");
    var header = html.QuerySelector("h1");
    Assert.Equal("Welcome", header.Text());
}
```

## Caveats

You will need to make sure that your application and all its dependencies can be resolved. This includes databases and third party services. Luckily the `WebApplicationFactory` has the ability to swap out and register new dependencies.

**Good Luck!**
