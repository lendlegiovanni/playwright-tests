namespace SpreeCommerce.TestHelpers.PageComponents;

public class LoginSidePanel(IPage page) : ILoginSidePanel
{
    private ILocator EmailTextbox => page.Locator("#user_email");
    private ILocator PasswordTextbox => page.Locator("#user_password");
    private ILocator LoginButton => page.GetByRole(AriaRole.Button, new() { Name = "Login" });
    private ILocator SignUpLink => page.Locator("a[href='/user/sign_up']");


    public async Task LoginAsync(string email, string password)
    {
        await LoginButton.ClickAsync(new LocatorClickOptions { Trial = true }); // Ensure the panel is open before filling in the credentials
        await EmailTextbox.FillAsync(email);
        await PasswordTextbox.FillAsync(password);
        await LoginButton.ClickAsync();
    }

    public async Task ClickSignUpAsync()
    {
        //await LoginButton.ClickAsync(new LocatorClickOptions { Trial = true }); // Ensure the panel is open before filling in the credentials
        await SignUpLink.ClickAsync();
    }

}

public interface ILoginSidePanel
{
    Task LoginAsync(string email, string password);
    Task ClickSignUpAsync();
}