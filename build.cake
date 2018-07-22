#tool nuget:?package=xunit.runner.console

var target = Argument<string>("target");
var configuration = Argument<string>("configuration");

Task("Restore")
    .Does(
        () =>
            NuGetRestore("MyPlaces.sln"));

Task("Build")
    .IsDependentOn("Restore")
    .Does(
        () =>
            MSBuild(
                "MyPlaces.sln",
                new MSBuildSettings
                {
                    ArgumentCustomization = args => args.Append("/nologo")
                }
                    .SetConfiguration(configuration)
                    .SetMaxCpuCount(0)
                }));

Task("Test")
    .IsDependentOn("Build")
    .Does(
        () =>
            XUnit2(
                @"MyPlaces.Test\bin\" + configuration + @"\MyPlaces.Test.dll",
                new XUnit2Settings
                {
                    NoAppDomain = true,
                    ArgumentCustomization = args => args.Append("-nologo")
                }));

RunTarget(target);
