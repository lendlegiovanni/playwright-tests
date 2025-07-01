namespace SpreeCommerce.TestHelpers.Pages;

public class HomePage(IPage page) : IHomePage
{
    private ILocator LoginButton => page.GetByRole(AriaRole.Button, new() { Name = "Login" });
    private ILocator LogoutButton => page.GetByRole(AriaRole.Button, new() { Name = "Log out" });
    private ILocator ShopAllLink => page.GetByRole(AriaRole.Link, new() { Name = "Shop All" });
    private ILocator CartButton => page.Locator("a[href='/cart']");
    private ILocator SearchButton => page.Locator("#open-search");
    private ILocator SearchTextbox => page.Locator("#q");
    private ILocator SignedInUserIcon => page.Locator("//a[@href='/account' and not(@class)]");
    private ILocator SignedOutUserIcon => page.Locator("//form[@action='/account/wishlist']/preceding-sibling::div[1]//button[@data-action='click->slideover-account#toggle click@window->slideover-account#hide click->toggle-menu#hide touch->toggle-menu#hide']");
    
    public async Task ClickLoginButtonAsync()
    {
        await LoginButton.ClickAsync();
    }

    public async Task ClickCartButtonAsync()
    {
        await CartButton.ClickAsync();
    }

    public async Task ClickUserIconAsync(bool isUserSignedIn = false)
    {
        await page.WaitForLoadStateAsync(LoadState.NetworkIdle);
        if (!isUserSignedIn)
        {
            await SignedOutUserIcon.ClickAsync();
        }
        else
        {
            await SignedInUserIcon.ClickAsync();
        }
    }

    public async Task LogoutAsync()
    {
        await SignedInUserIcon.ClickAsync();
        await LogoutButton.ClickAsync();
    }

    public async Task ClickShopAllLinkAsync()
    {
        await ShopAllLink.ClickAsync();
    }

    public async Task SearchProductAsync(string product)
    {
        await SearchButton.ClickAsync();
        await SearchTextbox.FillAsync(product);
    }

    public async Task SelectProductAsync(string product)
    {
        var productLocator = page.Locator($"//a[contains(@id,'product-')]").Filter(new LocatorFilterOptions { HasText = product }).Nth(0);
        await productLocator.ClickAsync();
    }
}

public interface IHomePage
{
    Task ClickLoginButtonAsync();
    Task ClickShopAllLinkAsync();
    Task ClickCartButtonAsync();
    Task ClickUserIconAsync(bool isUserSignedIn = false);
    Task LogoutAsync();
    Task SearchProductAsync(string product);
    Task SelectProductAsync(string product);
}