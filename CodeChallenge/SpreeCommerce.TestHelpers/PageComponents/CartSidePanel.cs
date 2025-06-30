using SpreeCommerce.TestHelpers.Models;

namespace SpreeCommerce.TestHelpers.PageComponents;

public class CartSidePanel(IPage page) : ICartSidePanel
{
    private ILocator ProductItems => page.Locator("//li[contains(@class, 'cart-line-item')]");

    public async Task<List<Product>> GetProducts()
    {
        var products = new List<Product>();
        await ProductItems.First.ClickAsync(new LocatorClickOptions { Trial = true }); // Ensure the panel is open before fetching products
        var items = await ProductItems.AllAsync();
        foreach (var item in items)
        {
            var productName = await item.Locator("//form[1]/preceding-sibling::a").InnerTextAsync();
            var productPrice = await item.Locator("//form[1]/../following-sibling::div[1]/span[last()]").InnerTextAsync();
            var productColor = await item.Locator("//form[1]/../following-sibling::div[2]//input").InputValueAsync();
            var productSize = await item.Locator("//form[1]/../following-sibling::div[2]/div[2]").InnerTextAsync();
            var productQuantity = await item.Locator("//form[1]/../following-sibling::div[3]//input[@id='line_item_quantity']").InputValueAsync();

            products.Add(new Product
            {
                Name = productName,
                Price = productPrice,
                Color = productColor,
                Size = productSize,
                Quantity = int.Parse(productQuantity)
            });
        }

        return products;
    }

}

public interface ICartSidePanel
{
    Task<List<Product>> GetProducts();
}