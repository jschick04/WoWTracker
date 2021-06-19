var target = Argument("Target", "Default");
var configuration = Argument("Configuration", "Release");

var solutionFolder = Directory("./");
var api = Directory("./Tracker.Api/Tracker.Api.csproj");
var apiTest = Directory("./Tracker.Api.Tests/Tracker.Api.Tests.csproj");

var outputFolder = Directory("./artifacts");

Information($"Running target {target} in configuration {configuration}");

Task("Clean").Does(() => { CleanDirectory(outputFolder); });

Task("Restore").Does(() => { DotNetCoreRestore(); });

Task("BuildApi")
    .Does(() => {
        DotNetCoreBuild(api, new DotNetCoreBuildSettings {
            NoRestore = true, Configuration = configuration
        });
    });

Task("Test")
    .Does(() => {
        DotNetCoreTest(apiTest, new DotNetCoreTestSettings {
            NoRestore = true, NoBuild = true, Configuration = configuration
        });
    });

Task("Publish")
    .Does(() => {
        DotNetCorePublish(api, new DotNetCorePublishSettings {
            NoRestore = true, NoBuild = true, Configuration = configuration, OutputDirectory = outputFolder
        });
    });

Task("BuildAndTest")
    .IsDependentOn("Clean")
    .IsDependentOn("Restore")
    .IsDependentOn("BuildApi")
    .IsDependentOn("Test");

Task("Default")
    .IsDependentOn("BuildAndTest")
    .IsDependentOn("Publish");

RunTarget(target);