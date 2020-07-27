using OrchardCore.Modules.Manifest;

[assembly: Module(
    Name = "Rework: Content Approval",
    Author = "Endless Mountain Solutions",
    Website = "https://www.endlessmountainsolutions.com",
    Version = "0.0.1",
    Description = "Adds ability to have some users without publish permissions to request content approval (via workflow if workflow enabled).",
    Category = "Content Management",
    Dependencies = new[] { "OrchardCore.Contents" }
)]