using PuppeteerSharp;

namespace ScraperDofusRetroAPI.Scrapers;

public abstract class BaseScraper : IScraper
{
    protected IBrowser _browser = null!;
    protected readonly HttpClient _httpClient;
    
    public BaseScraper(bool headless)
    {
        LaunchOptions options = new LaunchOptions
        {
            Headless = headless,
            ExecutablePath = @"C:\Program Files (x86)\Google\Chrome\Application\chrome.exe"
            // ExecutablePath = @"/Applications/Google Chrome.app/Contents/MacOS/Google Chrome"
        };
        
        Puppeteer
            .LaunchAsync(options)
            .ContinueWith(t => _browser = t.Result)
            .GetAwaiter()
            .GetResult();

        _httpClient = new HttpClient();
    }
    
    public abstract Task Scrape();
    
    protected async Task ScrollDownABit(IPage page)
    {
        await page.Mouse.WheelAsync(0, 2000);
        
        await page.WaitForTimeoutAsync(500);
        
        await page.Mouse.WheelAsync(0, 2000);
        
        await page.WaitForTimeoutAsync(500);
        
        await page.Mouse.WheelAsync(0, 2000);
        
        await page.WaitForTimeoutAsync(500);
        
        await page.Mouse.WheelAsync(0, 2000);
        
        await page.WaitForTimeoutAsync(500);
        
        await page.Mouse.WheelAsync(0, 2000);
    }

    protected async Task ScrollToBottom(IPage page)
    {
        int totalScrolls = 150;
        for (int i = 0; i < totalScrolls; i++)
        {
            await page.Mouse.WheelAsync(0, 8000);
            await page.WaitForTimeoutAsync(500);
            Console.WriteLine($"Scroll {i + 1}/{totalScrolls}");
        }
    }
}