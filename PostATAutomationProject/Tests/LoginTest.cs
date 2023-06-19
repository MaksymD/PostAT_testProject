using NUnit.Framework;
using OpenQA.Selenium;
using PostATAutomationProject.PageObjects;
using log4net;
using log4net.Config;

namespace PostATAutomationProject.Tests;

public class LoginTest : BaseTest
{
    private static readonly ILog log = LogManager.GetLogger(typeof(LoginTest));

    public LoginTest(string browser)
        : base(browser)
    {
    }

    [Test]
    [Category("Selenium")]
    public void Test_LoginWithoutCredentials()
    {
        using (var driver = InitializeDriver())
        {
            try
            {
                // navigate to "post.at" Page and check title
                StartPage startPage = new StartPage(driver);
                Assert.That("Post AG - PostAG", Is.EqualTo(startPage.GetPageTitle()));
                log.Info("Post AG start Page opened.");
                
                // click accept cookies button
                startPage.AcceptCookie();
                log.Info("Cookies accepted.");
                
                // click login button on top-menu
                startPage.selectLoginInHeaderbar();
                log.Info("'Einloggen / Registrieren' button clicked.");
                
                // navigate to "login.post.at" Page and check title
                UserLoginPage userLoginPage = new UserLoginPage(driver);
                Assert.That("Userlogin - Default", Is.EqualTo(userLoginPage.GetPageTitle()));
                log.Info("User Login Post AG Page opened.");
                
                // click 'Jetzt einloggen' button and check if Email/Password errors are displayed 
                userLoginPage.clickLoginButton();
                Thread.Sleep(1000);
                Assert.True(driver.FindElement(By.XPath("//p[contains(@role, 'alert') and contains(text(), 'Bitte E-Mail Adresse eingeben.')]")).Displayed);
                Assert.True(driver.FindElement(By.XPath("//p[contains(@role, 'alert') and contains(text(), 'Bitte Passwort eingeben.')]")).Displayed);
                log.Info("Email/Password errors are displayed.");
            }
            finally
            {
                commonUtils.PrintLogs("browser", driver);
            }
        }
    }
}