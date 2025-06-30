namespace SpreeCommerce.TestHelpers.Pages;

public class OrderConfirmationPage(IPage page) : IOrderConfirmationPage
{
    public ILocator OrderIsConfirmedText => page.GetByText("Your order is confirmed!");
    private ILocator OrderNumberText => page.Locator("//div[contains(@id, 'order_')]//strong");
    private ILocator StatusText => page.Locator("//span[.='Status']/following-sibling::span");

    public async Task<string> GetOrderNumberAsync()
    {
        return await OrderNumberText.InnerTextAsync();
    }

    public async Task<string> GetStatusAsync()
    {
        return await StatusText.InnerTextAsync();
    }
}

public interface IOrderConfirmationPage
{
    ILocator OrderIsConfirmedText { get; }
    Task<string> GetOrderNumberAsync();
    Task<string> GetStatusAsync();
}