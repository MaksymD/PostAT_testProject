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
                Assert.That(configuration.GetSection("StartPage:TestData:startPageTitle").Value, Is.EqualTo(startPage.GetPageTitle()));
                log.Info("Post AG start Page opened.");
                
                // click accept cookies button
                bool elementCookiesExists = driver.FindElements(By.XPath(configuration.GetSection("StartPage:Locators:buttonAcceptCookies_locator").Value)).Count > 0;
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
                Assert.That(configuration.GetSection("UserLoginPage:TestData:userLoginPageTitle").Value, Is.EqualTo(userLoginPage.GetPageTitle()));
                log.Info("User Login Post AG Page opened.");
                
                // click 'Jetzt einloggen' button and check if user successfully logged in
                userLoginPage.inputLoginCredentials(configuration.GetSection("UserLoginPage:TestData:email").Value, configuration.GetSection("UserLoginPage:TestData:password").Value);
                userLoginPage.clickLoginButton();
                startPage.waitTillElementIsClickable(By.XPath(configuration.GetSection("StartPage:Locators:buttonUserContextmenu_locator").Value));
                Assert.That(configuration.GetSection("StartPage:TestData:startPageTitle").Value, Is.EqualTo(startPage.GetPageTitle()));
                startPage.CheckIfElementExistByLocator(By.XPath(configuration.GetSection("StartPage:Locators:buttonLogout_locator").Value));
                log.Info("User successfully logged in.");
                
                // click logout button
                startPage.clickLogoutButton();
                Thread.Sleep(2000);
                Assert.True(driver.FindElement(By.XPath(configuration.GetSection("StartPage:Locators:buttonLogin_locator").Value)).Displayed);
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
                Assert.That(configuration.GetSection("StartPage:TestData:startPageTitle").Value, Is.EqualTo(startPage.GetPageTitle()));
                log.Info("Post AG start Page opened.");
                
                // click accept cookies button
                bool elementCookiesExists = driver.FindElements(By.XPath(configuration.GetSection("StartPage:Locators:buttonAcceptCookies_locator").Value)).Count > 0;
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
                Assert.That(configuration.GetSection("UserLoginPage:TestData:UserLoginPageTitle").Value, Is.EqualTo(userLoginPage.GetPageTitle()));
                log.Info("User Login Post AG Page opened.");
                
                // click 'Jetzt einloggen' button and check if Email/Password errors are displayed 
                userLoginPage.clickLoginButton();
                Thread.Sleep(1000);
                Assert.True(driver.FindElement(By.XPath(configuration.GetSection("StartPage:Locators:textEmailPassword_locator").Value)).Displayed);
                Assert.True(driver.FindElement(By.XPath(configuration.GetSection("StartPage:Locators:textErrorPassword_locator").Value)).Displayed);
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
                Assert.That(configuration.GetSection("StartPage:TestData:startPageTitle").Value, Is.EqualTo(startPage.GetPageTitle()));
                log.Info("Post AG start Page opened.");
                
                // click accept cookies button
                bool elementCookiesExists = driver.FindElements(By.XPath(configuration.GetSection("StartPage:Locators:buttonAcceptCookies_locator").Value)).Count > 0;
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
                Assert.That(configuration.GetSection("UserLoginPage:TestData:UserLoginPageTitle").Value, Is.EqualTo(userLoginPage.GetPageTitle()));
                log.Info("User Login Post AG Page opened.");

                // click 'Jetzt einloggen' button and check if Login failed message is displayed 
                userLoginPage.inputLoginCredentials(configuration.GetSection("UserLoginPage:TestData:email_invalid").Value, configuration.GetSection("UserLoginPage:TestData:password").Value);
                userLoginPage.clickLoginButton();
                userLoginPage.clickLoginButton();
                Thread.Sleep(1000);
                Assert.True(driver.FindElement(By.XPath(configuration.GetSection("StartPage:Locators:textErrorEmailInvalid_locator").Value)).Displayed);
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
                Assert.That(configuration.GetSection("StartPage:TestData:startPageTitle").Value, Is.EqualTo(startPage.GetPageTitle()));
                log.Info("Post AG start Page opened.");
                
                // click accept cookies button
                bool elementCookiesExists = driver.FindElements(By.XPath(configuration.GetSection("StartPage:Locators:buttonAcceptCookies_locator").Value)).Count > 0;
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
                Assert.That(configuration.GetSection("UserLoginPage:TestData:UserLoginPageTitle").Value, Is.EqualTo(userLoginPage.GetPageTitle()));
                log.Info("User Login Post AG Page opened.");

                // click 'Jetzt einloggen' button and check if Login failed message is displayed 
                userLoginPage.inputLoginCredentials(configuration.GetSection("UserLoginPage:TestData:email_wrong").Value, configuration.GetSection("UserLoginPage:TestData:password").Value);
                userLoginPage.clickLoginButton();
                Thread.Sleep(1000);
                Assert.True(driver.FindElement(By.XPath(configuration.GetSection("StartPage:Locators:textErrorLoginFailedH2_locator").Value)).Displayed);
                Assert.True(driver.FindElement(By.XPath(configuration.GetSection("StartPage:Locators:textErrorLoginFailedH3_locator").Value)).Displayed);
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
                Assert.That(configuration.GetSection("StartPage:TestData:startPageTitle").Value, Is.EqualTo(startPage.GetPageTitle()));
                log.Info("Post AG start Page opened.");
                
                // click accept cookies button
                bool elementCookiesExists = driver.FindElements(By.XPath(configuration.GetSection("StartPage:Locators:buttonAcceptCookies_locator").Value)).Count > 0;
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
                Assert.That(configuration.GetSection("UserLoginPage:TestData:UserLoginPageTitle").Value, Is.EqualTo(userLoginPage.GetPageTitle()));
                log.Info("User Login Post AG Page opened.");

                // click 'Jetzt einloggen' button and check if Login failed message is displayed 
                userLoginPage.inputLoginCredentials(configuration.GetSection("UserLoginPage:TestData:email").Value, configuration.GetSection("UserLoginPage:TestData:password_wrong").Value);
                userLoginPage.clickLoginButton();
                Thread.Sleep(1000);
                Assert.True(driver.FindElement(By.XPath(configuration.GetSection("StartPage:Locators:textErrorLoginFailedH2_locator").Value)).Displayed);
                Assert.True(driver.FindElement(By.XPath(configuration.GetSection("StartPage:Locators:textErrorLoginFailedH3_locator").Value)).Displayed);
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
                Assert.That(configuration.GetSection("StartPage:TestData:startPageTitle").Value, Is.EqualTo(startPage.GetPageTitle()));
                log.Info("Post AG start Page opened.");
                
                // click accept cookies button
                bool elementCookiesExists = driver.FindElements(By.XPath(configuration.GetSection("StartPage:Locators:buttonAcceptCookies_locator").Value)).Count > 0;
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
                Assert.That(configuration.GetSection("UserLoginPage:TestData:UserLoginPageTitle").Value, Is.EqualTo(userLoginPage.GetPageTitle()));
                log.Info("User Login Post AG Page opened.");

                // click 'Jetzt einloggen' button and check if Login failed message is displayed
                userLoginPage.inputLoginCredentials(configuration.GetSection("UserLoginPage:TestData:email_wrong").Value, configuration.GetSection("UserLoginPage:TestData:password_wrong").Value);
                userLoginPage.clickLoginButton();
                Thread.Sleep(1000);
                Assert.True(driver.FindElement(By.XPath(configuration.GetSection("StartPage:Locators:textErrorLoginFailedH2_locator").Value)).Displayed);
                Assert.True(driver.FindElement(By.XPath(configuration.GetSection("StartPage:Locators:textErrorLoginFailedH3_locator").Value)).Displayed);
                log.Info("Login failed message is displayed.");
            }
            finally
            {
                commonUtils.PrintLogs("browser", driver);
            }
        }
    }
}