namespace SpreeCommerce.Tests.Tests;

[Collection(E2eTestCollection.Name)]
public class BrowseProductTests : BaseTest
{

    public BrowseProductTests(ITestOutputHelperAccessor testOutputHelperAccessor, TestFixture fixture) : base(testOutputHelperAccessor, fixture)
    {

    }

    [Fact]
    public async Task SignedInUser_SelectProductAndAddToCart_ShouldAddTheProductToCartSuccessfully()
    {
        //Arrange
        var product = GetProduct();
        var email = $"test{DateTime.UtcNow.Ticks}@test.com";
        var password = Guid.NewGuid().ToString("N");
        InitializePages(Page);
        await GoToPage(TestSettings.BaseUrl);
        await HomePage.ClickUserIconAsync();
        await LoginSidePanel.ClickSignUpAsync();
        await SignUpSidePanel.SignUpAsync(email, password);

        //Act
        await HomePage.SearchProductAsync(product.Name);
        await HomePage.SelectProductAsync(product.Name);
        await ProductPage.AddToCartAsync(product);
        var items = await CartSidePanel.GetProducts();
        await ProductPage.ClickCheckoutAsync();

        //Assert
        items.Count.ShouldBe(1);
        items[0].Name.ShouldBe(product.Name);
        items[0].Color.ShouldBe(product.Color.ToLower());
        items[0].Size.ToLower().ShouldBe(product.Size.ToLower());
        items[0].Quantity.ShouldBe(product.Quantity);
        items[0].Price.ShouldBe(product.Price);
    }

}