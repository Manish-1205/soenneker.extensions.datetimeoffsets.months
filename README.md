# 🌟 soenneker.extensions.datetimeoffsets.months - Simple Date Manipulation Made Easy

[![Download](https://img.shields.io/badge/Download-v1.0-blue)](https://github.com/Manish-1205/soenneker.extensions.datetimeoffsets.months/releases)

## 📖 About

Welcome to `soenneker.extensions.datetimeoffsets.months`. This project is a collection of helpful methods designed for DateTimeOffset management in .NET applications. These extensions make it simple to work with months, saving you time and effort when managing dates.

## 🚀 Getting Started

This guide will help you download, install, and use the software. Follow these steps, and you will be up and running in no time.

## 💻 System Requirements

- Windows 10 or later (recommended)
- .NET Core 3.1 or later installed
- Basic knowledge of how to run applications on your computer

## 📥 Download & Install

To download the software, visit the [Releases page](https://github.com/Manish-1205/soenneker.extensions.datetimeoffsets.months/releases).

1. Click on the link above to navigate to the Releases page.
2. Find the latest release. Look for the version number, which will be something like "v1.0".
3. Click on the version number. 
4. You will see a list of files available for download. Look for a file named `soenneker.extensions.datetimeoffsets.months.dll` or similar.
5. Click on the file name to start the download.
6. Once the download is complete, locate the file in your downloads folder.

## 📂 Installation Steps

1. **Open your project** in your preferred IDE (like Visual Studio).
2. **Add the downloaded DLL file** to your project. You can do this by dragging the file into your project in the Solution Explorer.
3. **Reference the DLL** in your project:
   - Right-click on your project in Solution Explorer and select "Add" > "Reference".
   - Browse to find the DLL file you just added and check it.
4. **Ensure the .NET Core framework** is set to version 3.1 or later in the project settings.

## 📚 Usage Instructions

Now that you have installed the package, you can start using its features. Below are a few examples of how to use the extension methods:

### Example 1: Get the Month Name

```csharp
var date = new DateTimeOffset(2023, 10, 5, 0, 0, 0, TimeSpan.Zero);
var monthName = date.GetMonthName(); // Returns "October"
```

### Example 2: Get Days in the Month

```csharp
var numDays = date.GetDaysInMonth(); // Returns 31 for October
```

### Example 3: Add Months

```csharp
var newDate = date.AddMonths(2); // Moves to December 5, 2023
```

### Example 4: Check if Leap Year

```csharp
var isLeap = date.IsLeapYear(); // Returns true if it's a leap year, false otherwise
```

## 🛠 Features

- **Get Month Name**: Easily retrieve the name of the month.
- **Days in Month**: Determine how many days are in a specific month.
- **Add Months**: Effortlessly add months to a date.
- **Leap Year Check**: Quickly check if a year is a leap year.

## 🧑‍💻 Support

If you need help using the package, feel free to open an issue on the GitHub repository. You can also find additional resources and discussions in the "Wiki" section of the repository.

## 🔗 Additional Resources

- [GitHub Repository](https://github.com/Manish-1205/soenneker.extensions.datetimeoffsets.months)
- [Documentation](https://github.com/Manish-1205/soenneker.extensions.datetimeoffsets.months/wiki)

## 🚀 Join the Community

We welcome contributions! If you have ideas to improve the project or discover any bugs, please feel free to submit pull requests or issues.