
# Reqnroll Automation Demo Framework

This is a personal demo project built to explore and implement Behavior-Driven Development (BDD) using **Reqnroll** (SpecFlow alternative) with **C#**, **NUnit**, and **Selenium**. The project demonstrates a unified automation test framework structure with integrated feature-level HTML reporting and GitHub Actions CI/CD pipeline.

---

## 🚀 Key Features

- ✅ BDD Testing with Reqnroll (Gherkin + Step Definitions)  
- 🧪 Unified Web UI test structure  
- 📊 Custom HTML Reporting (feature-level grouping, screenshots on failure)  
- 🔁 GitHub Actions CI/CD Integration  
- 🧩 Modular and scalable folder structure  

---

## 🛠️ Tech Stack

- C# (.NET 8)  
- NUnit  
- Reqnroll  
- Selenium WebDriver  
- RestSharp  
- GitHub Actions  
- Custom HTML Reporter  

---

## 📁 Folder Structure Overview

```
├── .github/workflows/   # GitHub Actions CI pipeline
├── Drivers/             # Browser Driver setup
├── Features/            # Gherkin feature files
├── Reports/             # Custom HTML Reports
├── Selectors/           # Class containing page wise UI selectors
├── StepDefinitions/     # Step Definitions for feature files
├── TestResults/         # Test results containing trx files and screenshot if failure occurs
├── Utils/               # Utility classes and helpers for HTML report and others
├── Hooks.cs             # Reqnroll Hooks for setup/teardown
```




---

## 🧪 How to Run Tests Locally

1. **Clone the repository:**

```bash

   git clone https://github.com/DurgaDuttTiwari/Reqnroll-Automation-DemoFramework.git

```

2. **Open the project in Visual Studio 2022+**

3. **Restore NuGet packages and build the project.**

4. **Add a json file named as "UserDetails.json" which will contain your email and password from test website https://magento.softwaretestingboard.com/  or you can add your own test website by changing the url in the LoginStepDefinitions.cs file**

5. **Your UserDetails.json look like this**
```
{
  "email": "YourMailFromAboveTestWebsite",
  "password": "YourPasswordFromAboveTestWebsite"
}
```
6. **Run tests via Test Explorer or with the CLI:**
```bash
	dotnet test
```

dotnet test
🔄 CI/CD Pipeline (GitHub Actions)

 	Triggers on push and pull request events
	Restores dependencies and builds the project
	Executes test suite
	Generates a clean HTML test report with result summary and screenshots


📁 Pipeline config: .github/workflows/ci.yml



👤 Author
Durga Dutt Tiwari



⚠️ This project is for learning and demonstration purposes only.
