namespace SpreeCommerce.TestHelpers.PageComponents;

public class SignUpSidePanel(IPage page) : ISignUpSidePanel
{
    private ILocator EmailTextbox => page.Locator("#user_email");
    private ILocator PasswordTextbox => page.Locator("#user_password");
    private ILocator PasswordConfirmationTextbox => page.Locator("#user_password_confirmation");
    private ILocator SignUpButton => page.GetByRole(AriaRole.Button, new() { Name = "Sign Up" });


    public async Task SignUpAsync(string email, string password)
    {
        await PasswordConfirmationTextbox.ClickAsync(new LocatorClickOptions { Trial = true}); // Ensure the panel is open before filling in the credentials
        await EmailTextbox.FillAsync(email);
        await PasswordTextbox.FillAsync(password);
        await PasswordConfirmationTextbox.FillAsync(password);
        await SignUpButton.ClickAsync();
    }

}

public interface ISignUpSidePanel
{
    Task SignUpAsync(string email, string password);
}