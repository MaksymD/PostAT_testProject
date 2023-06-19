using NUnit.Framework;
using PostATAutomationProject.PageObjects;

namespace PostATAutomationProject.Tests;

public class FirstTest : BaseTest
{
    public FirstTest(string browser)
        : base(browser)
    {
    }

    [Test]
    [Category("Selenium")]
    public void Test_StartPage()
    {
        using (var driver = InitializeDriver())
        {
            try
            {
                StartPage startPage = new StartPage(driver);
                startPage.EnterSerchText("QA test");
            }
            finally
            {
                commonUtils.PrintLogs("browser", driver);
            }
        }
    }
}