using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace PostATAutomationProject.PageObjects;

class UserLoginPage
{
    IWebDriver driver;
    WebDriverWait wait;

    // Locators
    private readonly By buttonLogin_locator = By.XPath("//button[contains(text(), 'Jetzt einloggen')]");
    private readonly By inputUser_locator = By.XPath("//input[@id = 'signInName']");
    private readonly By inputPassword_locator = By.XPath("//input[@id = 'password']");

    public UserLoginPage(IWebDriver driver)
    {
        this.driver = driver;
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