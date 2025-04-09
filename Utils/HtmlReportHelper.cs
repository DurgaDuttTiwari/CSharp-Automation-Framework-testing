using System.Text;
using System.Web;

namespace ReqnrollFirstTestProject.Utils
{
    public static class HtmlReportHelper
    {
        private static readonly string ReportFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory.Split(new[] { "bin" }, StringSplitOptions.None)[0], "Reports");
        private static readonly string ReportFile = Path.Combine(ReportFolder, "CustomReport.html");

        private static StringBuilder htmlBuilder = new StringBuilder();
        private static bool isInitialized = false;

        public static void InitializeReport()
        {
            if (isInitialized) return;

            htmlBuilder.AppendLine(@"<!DOCTYPE html>
<html>
<head>
    <meta charset='UTF-8'>
    <title>Test Automation Report</title>
    <style>
        body {
            font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
            background-color: #f7f7f7;
            color: #333;
            padding: 20px;
        }
        h1 {
            text-align: center;
        }
        table {
            width: 100%;
            border-collapse: collapse;
            background-color: #fff;
            margin-top: 20px;
            box-shadow: 0 0 5px rgba(0,0,0,0.1);
        }
        th, td {
            border: 1px solid #ccc;
            padding: 10px;
            text-align: left;
            vertical-align: top;
        }
        th {
            background-color: #eee;
        }
        .passed {
            color: green;
            font-weight: bold;
        }
        .failed {
            color: red;
            font-weight: bold;
        }
        .toggle-btn {
            cursor: pointer;
            color: #007BFF;
            text-decoration: underline;
        }
        .details {
            display: none;
            margin-top: 10px;
            background-color: #fff3f3;
            padding: 10px;
            border-left: 5px solid #e74c3c;
            font-family: Consolas, monospace;
            font-size: 13px;
            white-space: pre-wrap;
        }
        .screenshot {
            max-width: 200px;
            border: 1px solid #ccc;
        }
        .suggestion {
            display: block;
            margin-top: 10px;
            color: #e67e22;
        }
    </style>
    <script>
        function toggleErrorDetails(id) {
            var details = document.getElementById(id);
            if (details.style.display === 'block') {
                details.style.display = 'none';
            } else {
                details.style.display = 'block';
            }
        }
    </script>
</head>
<body>
    <h1>Automation Test Report</h1>
    <table>
        <tr>
            <th>Feature</th>
            <th>Scenario</th>
            <th>Status</th>
            <th>Error Line</th>
            <th>Error Details</th>
            <th>Screenshot</th>
        </tr>");

            isInitialized = true;
        }


        public static void AddTestResult(string feature, string scenario, bool isPassed, string? screenshotPath, Exception? error)
        {
            if (!isInitialized)
                InitializeReport();

            string status = isPassed ? "<span class='passed'>Passed</span>" : "<span class='failed'>Failed</span>";
            string errorId = $"err_{Guid.NewGuid():N}";
            //string errorLine = GetErrorLine(error);
            //string errorDetails = BeautifyError(error);

            string errorLine = "No error";
            string errorDetails = "—";

            if (!isPassed && error != null)
            {
                errorLine = GetErrorLine(error);
                errorDetails = $@"<span class='toggle-btn' onclick=""toggleErrorDetails('{errorId}')"">View Details</span>
        <div class='details' id='{errorId}'>{BeautifyError(error)}</div>";
            }

            string screenshotHtml = string.IsNullOrEmpty(screenshotPath)
                ? "—"
                : $"<a href='{screenshotPath}' target='_blank'><img src='{screenshotPath}' class='screenshot'></a>";

            htmlBuilder.AppendLine($@"
            <tr>
                <td>{HttpUtility.HtmlEncode(feature)}</td>
                <td>{HttpUtility.HtmlEncode(scenario)}</td>
                <td>{status}</td>
                <td>{HttpUtility.HtmlEncode(errorLine)}</td>
                <td>
                    {(isPassed ? "—" : $@"<span class='toggle-btn' onclick=""toggleErrorDetails('{errorId}')"">View Details</span>
                    <div class='details' id='{errorId}'>{errorDetails}</div>")}
                </td>
                <td>{screenshotHtml}</td>
            </tr>");


            //            htmlBuilder.AppendLine($@"
            //<tr>
            //    <td>{HttpUtility.HtmlEncode(feature)}</td>
            //    <td>{HttpUtility.HtmlEncode(scenario)}</td>
            //    <td>{status}</td>
            //    <td>{HttpUtility.HtmlEncode(errorLine)}</td>
            //    <td>{errorDetails}</td>
            //    <td>{screenshotHtml}</td>
            //</tr>");

        }


        public static void FinalizeReport()
        {

            htmlBuilder.AppendLine("</table></body></html>");

            File.WriteAllText(ReportFile, htmlBuilder.ToString());
        }

        private static string GetErrorLine(Exception? error)
        {
            if (error?.StackTrace == null) return "Unknown";

            var lines = error.StackTrace.Split('\n');
            var match = lines.FirstOrDefault(l => l.Contains(".feature:line"));
            if (match != null)
            {
                var parts = match.Split(':');
                var linePart = parts.LastOrDefault(p => p.Contains("line"));
                return linePart?.Replace("line", "Line") ?? "Unknown";
            }

            return "Unknown";
        }

        private static string BeautifyError(Exception? error)
        {
            if (error == null) return "No error.";

            var message = HttpUtility.HtmlEncode(error.Message);
            var suggestion = GetErrorSuggestion(message);

            var lines = error.StackTrace?.Split('\n') ?? Array.Empty<string>();
            var userLines = lines
                .Where(line => line.Contains("ReqnrollFirstTestProject") || line.Contains(".cs:line"))
                .Select(line => HttpUtility.HtmlEncode(line.Trim()))
                .ToList();

            string formattedStack = string.Join("<br>", userLines);

            return $@"
<b>Message:</b> {message}<br>
<b>Stack Trace:</b><br><code style='font-size: 0.9em; color: #222'>{formattedStack}</code><br>
<span class='suggestion'>{suggestion}</span>";
        }


        private static string GetErrorSuggestion(string message)
        {
            if (message.Contains("NoSuchElementException"))
                return "🔍 Check if the element exists and selector is correct.";
            if (message.Contains("ElementNotInteractableException"))
                return "🚫 Element exists but may be hidden or disabled.";
            if (message.Contains("TimeoutException"))
                return "⏳ Increase wait time or ensure element appears in time.";
            if (message.Contains("StaleElementReferenceException"))
                return "♻️ Re-fetch element after DOM reload.";
            if (message.Contains("InvalidSelectorException"))
                return "❗ Check syntax of the locator.";
            return "ℹ️ Review the full error trace for debugging.";
        }
    }
}
