using NUnit.Framework;
using OpenQA.Selenium;
using PostATAutomationProject.PageObjects;
using log4net;
using log4net.Config;
using OpenQA.Selenium.Support.UI;

namespace PostATAutomationProject.Tests;

public class LoginTest : BaseTest
{
    private static readonly ILog log = LogManager.GetLogger(typeof(LoginTest));

    public LoginTest(string browser)
        : base(browser)
    {
    }
    
    [Test, Order(1)]
    [Category("Selenium")]
    public void TestPositive_LoginWithActiveCredentials()
    {
        using (var driver = InitializeDriver())
        {
            try
            {
                // navigate to "post.at" Page and check title
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(60));
                StartPage startPage = new StartPage(driver);
                Assert.That("Post AG - PostAG", Is.EqualTo(startPage.GetPageTitle()));
                log.Info("Post AG start Page opened.");
                
                // click accept cookies button
                bool elementCookiesExists = driver.FindElements(By.XPath("//button[@id = 'onetrust-accept-btn-handler']")).Count > 0;
                if (elementCookiesExists)
                {
                    startPage.AcceptCookie();
                    log.Info("Cookies accepted.");
                }

                // click login button on top-menu
                startPage.selectLoginInHeaderbar();
                log.Info("'Einloggen / Registrieren' button clicked.");
                
                // navigate to "login.post.at" Page and check title
                UserLoginPage userLoginPage = new UserLoginPage(driver);
                Assert.That("Userlogin - Default", Is.EqualTo(userLoginPage.GetPageTitle()));
                log.Info("User Login Post AG Page opened.");
                
                // click 'Jetzt einloggen' button and check if user successfully logged in
                userLoginPage.inputLoginCredentials("max.musterman.postat.test@gmail.com", "postATtest2023!");
                userLoginPage.clickLoginButton();
                startPage.waitTillElementIsClickable(By.XPath("//button[@id = 'contextmenu-lg']"));
                Assert.That("Post AG - PostAG", Is.EqualTo(startPage.GetPageTitle()));
                startPage.CheckIfElementExistByLocator(By.XPath("//nav[@aria-hidden = 'true']/a[contains(text(), 'Ausloggen')]"));
                log.Info("User successfully logged in.");
                
                // click logout button
                startPage.clickLogoutButton();
                Thread.Sleep(2000);
                Assert.True(driver.FindElement(By.XPath("(//span[contains(text(), 'Einloggen / Registrieren')]//ancestor::button)[1]")).Displayed);
                log.Info("User successfully logged out.");

            }
            finally
            {
                commonUtils.PrintLogs("browser", driver);
            }
        }
    }

    [Test, Order(2)]
    [Category("Selenium")]
    public void TestNegative_LoginWithEmptyCredentials()
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
                bool elementCookiesExists = driver.FindElements(By.XPath("//button[@id = 'onetrust-accept-btn-handler']")).Count > 0;
                if (elementCookiesExists)
                {
                    startPage.AcceptCookie();
                    log.Info("Cookies accepted.");
                }
                
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
    
    [Test, Order(3)]
    [Category("Selenium")]
    public void TestNegative_LoginInvalidEmail()
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
                bool elementCookiesExists = driver.FindElements(By.XPath("//button[@id = 'onetrust-accept-btn-handler']")).Count > 0;
                if (elementCookiesExists)
                {
                    startPage.AcceptCookie();
                    log.Info("Cookies accepted.");
                }
                
                // click login button on top-menu
                startPage.selectLoginInHeaderbar();
                log.Info("'Einloggen / Registrieren' button clicked.");
                
                // navigate to "login.post.at" Page and check title
                UserLoginPage userLoginPage = new UserLoginPage(driver);
                Assert.That("Userlogin - Default", Is.EqualTo(userLoginPage.GetPageTitle()));
                log.Info("User Login Post AG Page opened.");

                // click 'Jetzt einloggen' button and check if Login failed message is displayed 
                userLoginPage.inputLoginCredentials("test", "postATtest2023!");
                userLoginPage.clickLoginButton();
                userLoginPage.clickLoginButton();
                Thread.Sleep(1000);
                Assert.True(driver.FindElement(By.XPath("//p[contains(@role, 'alert') and contains(text(), 'Ungültige E-Mail Adresse.')]")).Displayed);
                log.Info("Login failed message is displayed.");
            }
            finally
            {
                commonUtils.PrintLogs("browser", driver);
            }
        }
    }
    
    [Test, Order(4)]
    [Category("Selenium")]
    public void TestNegative_LoginWrongEmail()
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
                bool elementCookiesExists = driver.FindElements(By.XPath("//button[@id = 'onetrust-accept-btn-handler']")).Count > 0;
                if (elementCookiesExists)
                {
                    startPage.AcceptCookie();
                    log.Info("Cookies accepted.");
                }
                
                // click login button on top-menu
                startPage.selectLoginInHeaderbar();
                log.Info("'Einloggen / Registrieren' button clicked.");
                
                // navigate to "login.post.at" Page and check title
                UserLoginPage userLoginPage = new UserLoginPage(driver);
                Assert.That("Userlogin - Default", Is.EqualTo(userLoginPage.GetPageTitle()));
                log.Info("User Login Post AG Page opened.");

                // click 'Jetzt einloggen' button and check if Login failed message is displayed 
                userLoginPage.inputLoginCredentials("test@gmail.com", "postATtest2023!");
                userLoginPage.clickLoginButton();
                Thread.Sleep(1000);
                Assert.True(driver.FindElement(By.XPath("//h2[contains(text(), 'Login fehlgeschlagen')]")).Displayed);
                Assert.True(driver.FindElement(By.XPath("//h3[contains(text(), 'Bitte versuchen Sie es nochmals!')]")).Displayed);
                log.Info("Login failed message is displayed.");
            }
            finally
            {
                commonUtils.PrintLogs("browser", driver);
            }
        }
    }
    
    [Test, Order(5)]
    [Category("Selenium")]
    public void TestNegative_LoginWrongPassword()
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
                bool elementCookiesExists = driver.FindElements(By.XPath("//button[@id = 'onetrust-accept-btn-handler']")).Count > 0;
                if (elementCookiesExists)
                {
                    startPage.AcceptCookie();
                    log.Info("Cookies accepted.");
                }
                
                // click login button on top-menu
                startPage.selectLoginInHeaderbar();
                log.Info("'Einloggen / Registrieren' button clicked.");
                
                // navigate to "login.post.at" Page and check title
                UserLoginPage userLoginPage = new UserLoginPage(driver);
                Assert.That("Userlogin - Default", Is.EqualTo(userLoginPage.GetPageTitle()));
                log.Info("User Login Post AG Page opened.");

                // click 'Jetzt einloggen' button and check if Login failed message is displayed 
                userLoginPage.inputLoginCredentials("max.musterman.postat.test@gmail.com", "test");
                userLoginPage.clickLoginButton();
                Thread.Sleep(1000);
                Assert.True(driver.FindElement(By.XPath("//h2[contains(text(), 'Login fehlgeschlagen')]")).Displayed);
                Assert.True(driver.FindElement(By.XPath("//h3[contains(text(), 'Bitte versuchen Sie es nochmals!')]")).Displayed);
                log.Info("Login failed message is displayed.");
            }
            finally
            {
                commonUtils.PrintLogs("browser", driver);
            }
        }
    }
    
    [Test, Order(6)]
    [Category("Selenium")]
    public void TestNegative_LoginWrongUsernameAndPassword()
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
                bool elementCookiesExists = driver.FindElements(By.XPath("//button[@id = 'onetrust-accept-btn-handler']")).Count > 0;
                if (elementCookiesExists)
                {
                    startPage.AcceptCookie();
                    log.Info("Cookies accepted.");
                }
                
                // click login button on top-menu
                startPage.selectLoginInHeaderbar();
                log.Info("'Einloggen / Registrieren' button clicked.");
                
                // navigate to "login.post.at" Page and check title
                UserLoginPage userLoginPage = new UserLoginPage(driver);
                Assert.That("Userlogin - Default", Is.EqualTo(userLoginPage.GetPageTitle()));
                log.Info("User Login Post AG Page opened.");

                // click 'Jetzt einloggen' button and check if Login failed message is displayed
                userLoginPage.inputLoginCredentials("test@gmail.com", "test");
                userLoginPage.clickLoginButton();
                Thread.Sleep(1000);
                Assert.True(driver.FindElement(By.XPath("//h2[contains(text(), 'Login fehlgeschlagen')]")).Displayed);
                Assert.True(driver.FindElement(By.XPath("//h3[contains(text(), 'Bitte versuchen Sie es nochmals!')]")).Displayed);
                log.Info("Login failed message is displayed.");
            }
            finally
            {
                commonUtils.PrintLogs("browser", driver);
            }
        }
    }
}