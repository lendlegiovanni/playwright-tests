namespace SpreeCommerce.Tests.Tests;

[Collection(E2eTestCollection.Name)]
public class LoginTests : BaseTest
{

    public LoginTests(ITestOutputHelperAccessor testOutputHelperAccessor, TestFixture fixture) : base(testOutputHelperAccessor, fixture)
    {
    }

    [Fact]
    public async Task ExistingUser_Login_ShouldBeSignedInSuccessfully()
    {
        //Arrange
        InitializePages(Page);
        await GoToPage(TestSettings.BaseUrl);
        await HomePage.ClickUserIconAsync();

        //Act
        await LoginSidePanel!.LoginAsync(TestSettings.UserEmail, TestSettings.UserPassword);

        //Assert
        await Expect(Page.GetByText(MessageConstants.SignedInSuccessfully)).ToBeVisibleAsync();
    }

    [Fact]
    public async Task SignedInUser_Logout_ShouldBeSignedOutSuccessfully()
    {
        //Arrange
        InitializePages(Page);
        await GoToPage(TestSettings.BaseUrl);
        await HomePage.ClickUserIconAsync();
        await LoginSidePanel!.LoginAsync(TestSettings.UserEmail, TestSettings.UserPassword);

        //Act
        await HomePage.LogoutAsync();

        //Assert
        await Expect(Page.GetByText(MessageConstants.SignedOutSuccessfully)).ToBeVisibleAsync();
    }

    [Fact]
    public async Task NewUser_SignUp_ShouldBeAbleToSeeTheWelcomeMessage()
    {
        //Arrange
        var email = $"test{DateTime.UtcNow.Ticks}@test.com";
        var password = Guid.NewGuid().ToString("N");
        InitializePages(Page);
        await GoToPage(TestSettings.BaseUrl);
        await HomePage.ClickUserIconAsync();
        await LoginSidePanel.ClickSignUpAsync();

        //Act
        await SignUpSidePanel.SignUpAsync(email, password);

        //Assert
        await Expect(Page.GetByText(MessageConstants.WelcomeToSite)).ToBeVisibleAsync();
    }

    public override BrowserNewContextOptions ContextOptions()
    {
        return base.ContextOptions();
    }
}