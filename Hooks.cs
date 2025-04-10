global using Reqnroll;
using NUnit.Framework;
using OpenQA.Selenium;
using ReqnrollFirstTestProject.Utils;

[assembly: Parallelizable(ParallelScope.Fixtures)] //Turn this off if you want to run test one after another.
namespace ReqnrollFirstTestProject.Drivers
{
    [Binding]
    public class Hooks
    {
        private readonly ScenarioContext _scenarioContext;
        private readonly FeatureContext _featureContext;

        public Hooks(ScenarioContext scenarioContext, FeatureContext featureContext)
        {
            _scenarioContext = scenarioContext;
            _featureContext = featureContext;
        }


        [BeforeTestRun]
        public static void BeforeTestRun() => HtmlReportHelper.InitializeReport();

        [BeforeScenario(Order = 0)]
        public void setup()
        {
            Console.WriteLine("🚀 Creating WebDriverManager...");
            _scenarioContext.Set(new WebDriverManager(), nameof(WebDriverManager));
        }

        [AfterScenario]
        public void teardown()
        {
            var driverManager = _scenarioContext.Get<WebDriverManager>(nameof(WebDriverManager));
            var driver = driverManager.Driver;

            bool isPassed = _scenarioContext.TestError == null;

            string? screenshotPath = null;

            if (!isPassed && driver is ITakesScreenshot screenshotDriver)
            {

                var screenshot = ((ITakesScreenshot)driver).GetScreenshot();
                var fileName = $"{_scenarioContext.ScenarioInfo.Title}_{DateTime.Now:yyyyMMdd_HHmmss}.png";
                var filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory.Split(new[] { "bin" }, StringSplitOptions.None)[0], "Reports", "Screenshots", fileName);
                Directory.CreateDirectory(Path.GetDirectoryName(filePath)!);
                File.WriteAllBytes(filePath, screenshot.AsByteArray);
                screenshotPath = Path.Combine("Screenshots", fileName); // relative path for HTML
            }

            HtmlReportHelper.AddTestResult(
                _featureContext.FeatureInfo.Title,
                _scenarioContext.ScenarioInfo.Title,
                isPassed,
                screenshotPath,
                _scenarioContext.TestError
            );

            driverManager.Dispose();
        }

        [AfterTestRun]
        [AfterTestRun]
        public static void AfterTestRun() => HtmlReportHelper.FinalizeReport();

    }


}
