using System;
using NUnit.Framework;
using OpenQA.Selenium;
using PostATAutomationProject.SeleniumUtils;
using Microsoft.Extensions.Configuration;

namespace PostATAutomationProject;

[TestFixture(BrowserTarget.Chrome)]
public abstract class BaseTest
{
    protected LocalDriverBuilder builder;
    protected string startingUrl;
    protected string epochStartTime;
    protected string targetBrowser;
    protected CommonUtils commonUtils = new CommonUtils();
    protected IConfiguration configuration;

    protected BaseTest(string browser)
    {
        this.targetBrowser = browser;

        this.configuration = new ConfigurationBuilder()
            .AddJsonFile(Path.Combine(AppDomain.CurrentDomain.BaseDirectory,"Tests/Resources/properties.json"))
            .Build();
        
        this.epochStartTime = DateTimeOffset.Now.ToUnixTimeMilliseconds().ToString();
    }

    public IWebDriver InitializeDriver()
    {
        LocalDriverBuilder builder = new LocalDriverBuilder();
        this.startingUrl = configuration.GetSection("StartPage:TestData:start_URL").Value;
        var driver = builder.Launch(targetBrowser, this.startingUrl);
        return driver;
    }
}