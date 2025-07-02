namespace SpreeCommerce.Tests.Tests;

public class BaseTest : PageTest
{

    protected readonly ITestOutputHelper output;
    protected readonly IConfiguration configuration;
    protected readonly ServiceProvider provider;
    protected readonly TestSettings TestSettings;
    protected IHomePage HomePage;
    protected ILoginSidePanel LoginSidePanel;
    protected ISignUpSidePanel SignUpSidePanel;
    protected IProductPage ProductPage;
    protected ICartSidePanel CartSidePanel;
    protected ICheckoutPage CheckoutPage;
    protected IOrderConfirmationPage OrderConfirmationPage;

    public BaseTest(ITestOutputHelperAccessor testOutputHelperAccessor, TestFixture fixture)
    {
        provider = fixture.ServiceProvider;
        output = testOutputHelperAccessor.Output!;
        configuration = fixture.Configuration;
        TestSettings = provider.GetService<IOptions<TestSettings>>().Value;
    }

    protected void InitializePages(IPage page)
    {
        HomePage = new HomePage(Page);
        LoginSidePanel = new LoginSidePanel(Page);
        SignUpSidePanel = new SignUpSidePanel(Page);
        ProductPage = new ProductPage(Page);
        CartSidePanel = new CartSidePanel(Page);
        CheckoutPage = new CheckoutPage(Page);
        OrderConfirmationPage = new OrderConfirmationPage(Page);
    }

    protected async Task GoToPage(string url)
    {
        await Page.GotoAsync(url);
        await Page.WaitForLoadStateAsync(LoadState.NetworkIdle);
        output.WriteLine($"URL: {Page.Url}");
    }

    protected Product GetProduct()
    {
        return new Product
        {
            Name = "Ripped T-Shirt",
            Color = "Teal",
            Price = "$40.00",
            Quantity = 1,
            Size = "L"
        };
    }

    protected UserAddress GetUserAddress()
    {
        return new UserAddress
        {
            FirstName = "Test",
            LastName = "User",
            StreetAndHouseNumber = "123 Test St",
            ApartmentSuite = "Apt 1",
            City = "Denver",
            State = "Colorado",
            PostalCode = "12345",
            Country = "United States",
            PhoneNumber = "123-456-7890"
        };
    }

    protected CardDetails GetCardDetails()
    {
        return new CardDetails
        {
            CardNumber = "4242424242424242",
            ExpirationDate = $"12/{DateTime.UtcNow.AddYears(1):yy}",
            SecurityCode = "123"
        };
    }

    public BrowserNewContextOptions GetContextOptions()
    {
        return new BrowserNewContextOptions
        {
            IgnoreHTTPSErrors = true,
            BypassCSP = true,
            ViewportSize = new ViewportSize
            {
                Width = 1920,
                Height = 1080
            },
            BaseURL = TestSettings.BaseUrl,
        };
    }

}
