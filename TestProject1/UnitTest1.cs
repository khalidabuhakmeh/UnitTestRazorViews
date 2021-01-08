using System;
using System.Threading.Tasks;
using AngleSharp.Dom;
using Microsoft.AspNetCore.Mvc.Testing;
using WebApplication6;
using Xunit;

namespace TestProject1
{
    public class UnitTest1
        : IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly WebApplicationFactory<Startup> factory;

        public UnitTest1(WebApplicationFactory<Startup> factory)
        {
            this.factory = factory;
        }
        
        [Fact]
        public async Task Header_say_Welcome()
        {
            var client = factory.CreateClient();
            var html = await client.GetHtmlAsync("/");
            var header = html.QuerySelector("h1");
            Assert.Equal("Welcome", header.Text());
        }
    }
}