var target = Argument("Target", "Default");
var configuration = Argument("Configuration", "Release");

var api = Directory("./src/Tracker.Api/Tracker.Api.csproj");
var apiTest = Directory("./src/Tracker.Api.Tests/Tracker.Api.Tests.csproj");
var client = Directory("./src/Tracker.Client/Tracker.Client.csproj");

var outputFolder = Directory("./artifacts");
var apiOutputFolder = Directory("./artifacts/Tracker.Api");
var clientOutputFolder = Directory("./artifacts/Tracker.Client");

Information($"Running target {target} in configuration {configuration}");

Task("Clean").Does(() => { CleanDirectory(outputFolder); });

Task("RestoreApi").Does(() => { DotNetCoreRestore(api); });

Task("RestoreClient").Does(() => { DotNetCoreRestore(client); });

Task("BuildApi")
    .Does(() => {
        DotNetCoreBuild(api, new DotNetCoreBuildSettings {
            NoRestore = true, Configuration = configuration
        });
    });

Task("BuildClient")
    .Does(() => {
        DotNetCoreBuild(client, new DotNetCoreBuildSettings {
            NoRestore = true, Configuration = configuration
        });
    });

Task("Test")
    .Does(() => {
        DotNetCoreTest(apiTest, new DotNetCoreTestSettings {
            NoRestore = true, NoBuild = true, Configuration = configuration
        });
    });

Task("PublishApi")
    .Does(() => {
        DotNetCorePublish(api, new DotNetCorePublishSettings {
            NoRestore = true, NoBuild = true, Configuration = configuration, OutputDirectory = apiOutputFolder
        });

        Zip(apiOutputFolder, $"{outputFolder}/TrackerApi.zip");
    })
    .Finally(() => {
        DeleteDirectory(apiOutputFolder, new DeleteDirectorySettings {
            Recursive = true, Force = true
        });
    });

Task("PublishClient")
    .Does(() => {
        DotNetCorePublish(client, new DotNetCorePublishSettings {
            NoRestore = true, NoBuild = true, Configuration = configuration, OutputDirectory = clientOutputFolder
        });

        Zip(clientOutputFolder, $"{outputFolder}/TrackerClient.zip");
    })
    .Finally(() => {
        DeleteDirectory(clientOutputFolder, new DeleteDirectorySettings {
            Recursive = true, Force = true
        });
    });

Task("BuildAndTest")
    .IsDependentOn("Clean")
    .IsDependentOn("RestoreApi")
    .IsDependentOn("RestoreClient")
    .IsDependentOn("BuildApi")
    .IsDependentOn("BuildClient");
    //.IsDependentOn("Test"); Disabled for CI until Tracker.Api.Tests Hangfire issue is sorted

Task("Default")
    .IsDependentOn("BuildAndTest")
    .IsDependentOn("PublishApi")
    .IsDependentOn("PublishClient");

RunTarget(target);