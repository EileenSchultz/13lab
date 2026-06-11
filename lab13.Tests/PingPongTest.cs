namespace lab13.Tests;

public class PingPongTests : IClassFixture<ApiFactory>
{
    private readonly ApiFactory _factory;
    
    public PingPongTests(ApiFactory factory)
    {
        _factory = factory;
    }
    
    [Fact]
    public async Task GetPing_ReceivePong()
    { 
        var client = _factory.CreateClient(); 
        var response = await client.GetAsync("/ping");
        Assert.True(response.IsSuccessStatusCode);
        var content = await response.Content.ReadAsStringAsync();
        Assert.Equal("pong", content);
    }
}