using SpreeCommerce.TestHelpers.Models;

namespace SpreeCommerce.TestHelpers.Pages;

public class CheckoutPage(IPage page) : ICheckoutPage
{
    private ILocator CountryDropdown => page.Locator("#order_ship_address_attributes_country_id");
    private ILocator FirstNameTextbox => page.GetByRole(AriaRole.Textbox, new PageGetByRoleOptions { Name = "First name" });
    private ILocator LastNameTextbox => page.GetByRole(AriaRole.Textbox, new PageGetByRoleOptions { Name = "Last name" });
    private ILocator StreetAndHouseNumberTextbox => page.GetByRole(AriaRole.Textbox, new PageGetByRoleOptions { Name = "Street and house number" });
    private ILocator ApartmentSuiteTextbox => page.GetByRole(AriaRole.Textbox, new PageGetByRoleOptions { Name = "Apartment, suite, etc. (optional)" });
    private ILocator CityTextbox => page.GetByRole(AriaRole.Textbox, new PageGetByRoleOptions { Name = "City" });
    private ILocator StateTextbox => page.Locator("#order_ship_address_attributes_state_id");
    private ILocator PostalTextbox => page.GetByRole(AriaRole.Textbox, new PageGetByRoleOptions { Name = "Postal" });
    private ILocator PhoneNumberTextbox => page.GetByRole(AriaRole.Textbox, new PageGetByRoleOptions { Name = "Phone (optional)" });
    private ILocator SaveAndContinueButton => page.GetByRole(AriaRole.Button, new PageGetByRoleOptions { Name = "Save and Continue" });
    private IFrameLocator CardDetailsFrame => page.FrameLocator("//iframe[contains(@name,'__privateStripeFrame') and @title='Secure payment input frame']");
    private ILocator CardNumberTextbox => CardDetailsFrame.Locator("#Field-numberInput");
    private ILocator ExpirationDateTextbox => CardDetailsFrame.Locator("#Field-expiryInput");
    private ILocator SecurityCodeTextbox => CardDetailsFrame.Locator("#Field-cvcInput");
    private ILocator PayNowButton => page.GetByRole(AriaRole.Button, new PageGetByRoleOptions { Name = "Pay now" });

    public async Task EnterCardDetailsAsync(CardDetails cardDetails)
    {
        await CardNumberTextbox.ClickAsync(new LocatorClickOptions { Trial = true });
        await CardNumberTextbox.FillAsync(cardDetails.CardNumber);
        await ExpirationDateTextbox.FillAsync(cardDetails.ExpirationDate);
        await SecurityCodeTextbox.FillAsync(cardDetails.SecurityCode);    }

    public async Task EnterShippingAddressAsync(UserAddress address)
    {
        await FirstNameTextbox.ClickAsync(new LocatorClickOptions { Trial = true });
        await CountryDropdown.SelectOptionAsync(address.Country);
        await FirstNameTextbox.FillAsync(address.FirstName);
        await LastNameTextbox.FillAsync(address.LastName);
        await StreetAndHouseNumberTextbox.FillAsync(address.StreetAndHouseNumber);
        await ApartmentSuiteTextbox.FillAsync(address.ApartmentSuite);
        await CityTextbox.FillAsync(address.City);
        if (!string.IsNullOrEmpty(address.State))
        {
            await StateTextbox.SelectOptionAsync(address.State);
        }
        await PostalTextbox.FillAsync(address.PostalCode);
        await PhoneNumberTextbox.FillAsync(address.PhoneNumber);
    }

    public async Task PayNowAsync()
    {
        await PayNowButton.ClickAsync();
        await page.WaitForLoadStateAsync(LoadState.NetworkIdle);
    }

    public async Task SaveAndContinueAsync()
    {
        await SaveAndContinueButton.ClickAsync();
    }

    public async Task SelectDeliveryMethodAsync(string deliveryMethod)
    {
        await page.GetByRole(AriaRole.Radio, new PageGetByRoleOptions { Name = deliveryMethod }).ClickAsync();
    }

    public async Task SelectPaymentMethodAsync(string paymentMethod)
    {
        await page.GetByRole(AriaRole.Link, new PageGetByRoleOptions { Name = $" {paymentMethod} " }).ClickAsync();
    }
}

public interface ICheckoutPage
{
    Task EnterShippingAddressAsync(UserAddress address);
    Task SaveAndContinueAsync();
    Task SelectDeliveryMethodAsync(string deliveryMethod);
    Task SelectPaymentMethodAsync(string paymentMethod);
    Task EnterCardDetailsAsync(CardDetails cardDetails);
    Task PayNowAsync();
}