using SpreeCommerce.TestHelpers.Models;

namespace SpreeCommerce.TestHelpers.Pages;

public class ProductPage(IPage page) : IProductPage
{
    private ILocator AddToCartButton => page.GetByRole(AriaRole.Button, new() { Name = "Add To Cart" });
    private ILocator CheckoutLink => page.GetByRole(AriaRole.Link, new() { Name = "Checkout" });
    private ILocator PleaseChooseSizeButton => page.GetByRole(AriaRole.Button, new() { Name = "Please choose size" }).Nth(0);
    private ILocator QuantityTextbox => page.Locator("#quantity");

    public async Task AddToCartAsync(Product product)
    {
        await QuantityTextbox.ClickAsync(new LocatorClickOptions { Trial = true });
        await page.Locator($"//input[@type='radio' and @name='Color' and @value='{product.Color.ToLower()}']/following-sibling::div").ClickAsync(new LocatorClickOptions { Force = true,  });
        await PleaseChooseSizeButton.ClickAsync();
        await page.Locator($"//input[@type='radio' and @name='Size' and @value='{product.Size.ToLower()}']/following-sibling::label").ClickAsync(new LocatorClickOptions { Force = true, });
        await QuantityTextbox.FillAsync(product.Quantity.ToString());
        await AddToCartButton.ClickAsync();
    }

    public async Task ClickCheckoutAsync()
    {
        await CheckoutLink.ClickAsync();
    }

}

public interface IProductPage
{
    Task AddToCartAsync(Product product);
    Task ClickCheckoutAsync();
}