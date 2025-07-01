namespace SpreeCommerce.Tests.Tests;

[Collection(E2eTestCollection.Name)]
public class CheckoutProductTests : BaseTest
{

    public CheckoutProductTests(ITestOutputHelperAccessor testOutputHelperAccessor, TestFixture fixture) : base(testOutputHelperAccessor, fixture)
    {

    }

    [Fact]
    public async Task UserCheckedOutTheProducts_EnterShippingAddressAndPaymentDetails_ShouldDisplayTheOrderNumberAndStatusSetToPaid()
    {
        //Arrange
        var product = GetProduct();
        var email = $"test{DateTime.UtcNow.Ticks}@test.com";
        var password = Guid.NewGuid().ToString("N");
        var address = GetUserAddress();
        var cardDetails = GetCardDetails();
        InitializePages(Page);
        await GoToPage(TestSettings.BaseUrl);
        await HomePage.ClickUserIconAsync();
        await LoginSidePanel.ClickSignUpAsync();
        await SignUpSidePanel.SignUpAsync(email, password);
        await HomePage.SearchProductAsync(product.Name);
        await HomePage.SelectProductAsync(product.Name);
        await ProductPage.AddToCartAsync(product);
        await ProductPage.ClickCheckoutAsync();

        //Act
        await CheckoutPage.EnterShippingAddressAsync(address);
        await CheckoutPage.SaveAndContinueAsync();
        await CheckoutPage.SelectDeliveryMethodAsync("Premium");
        await CheckoutPage.SaveAndContinueAsync();
        await CheckoutPage.SelectPaymentMethodAsync("Stripe");
        await CheckoutPage.EnterCardDetailsAsync(cardDetails);
        await CheckoutPage.PayNowAsync();

        //Assert
        await Expect(OrderConfirmationPage.OrderIsConfirmedText).ToBeVisibleAsync();
        var orderNumber = await OrderConfirmationPage.GetOrderNumberAsync();
        var status = await OrderConfirmationPage.GetStatusAsync();
        orderNumber.ShouldNotBeNullOrEmpty();
        status.ShouldBe("Paid");
    }

    public override BrowserNewContextOptions ContextOptions()
    {
        return base.ContextOptions();
    }
}