using System;
using Microsoft.Extensions.Configuration;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace PostATAutomationProject.PageObjects;

class UserLoginPage
{
    IWebDriver driver;
    WebDriverWait wait;
    IConfiguration configuration;

    // Locators
    private By buttonLogin_locator =>
        By.XPath(configuration.GetSection("UserLoginPage:Locators:buttonLogin_locator").Value);

    private By inputUser_locator =>
        By.XPath(configuration.GetSection("UserLoginPage:Locators:inputUser_locator").Value);

    private By inputPassword_locator =>
        By.XPath(configuration.GetSection("UserLoginPage:Locators:inputPassword_locator").Value);

    public UserLoginPage(IWebDriver driver, IConfiguration configuration)
    {
        this.driver = driver;
        this.configuration = configuration;
        this.wait = new WebDriverWait(driver, TimeSpan.FromSeconds(60));
    }

    public String GetPageTitle()
    {
        return driver.Title;
    }

    public void clickLoginButton()
    {
        var buttonLogin = driver.FindElement(buttonLogin_locator);
        wait.Until(x => buttonLogin).Click();
        Thread.Sleep(500);
    }

    public void inputLoginCredentials(string email, string password)
    {
        var inputUser = driver.FindElement(inputUser_locator);
        wait.Until(x => inputUser).SendKeys(email);
        Thread.Sleep(500);

        var inputPassword = driver.FindElement(inputPassword_locator);
        wait.Until(x => inputPassword).SendKeys(password);
        Thread.Sleep(500);
    }
}