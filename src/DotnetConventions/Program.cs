using System.CommandLine;
using DotnetConventions.Commands;

var embeddedFiles = new Dictionary<string, string>
{
    { ".editorconfig", "Templates.editorconfig" },
    { "Directory.Build.props", "Templates.DirectoryBuildProps" },
    { "Directory.Build.targets", "Templates.DirectoryBuildTargets" }
};

var root = new RootCommand("dotnet-conventions — apply shared .NET conventions to a repository.");
var apply = new Command("apply", "Apply conventions to the repository.");
var verify = new Command("verify", "Verify that repository files match the embedded templates.");

apply.SetHandler(_ => Apply.Handle(embeddedFiles));
verify.SetHandler(_ => Verify.Handle(embeddedFiles));

root.AddCommand(apply);
root.AddCommand(verify);

await root.InvokeAsync(args);
