using System;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace LockscreenTest
{
    public static class Program
    {
        private const string FAKE_TITLE = "Announcing Windows 12!";
        private const string FAKE_URI = "microsoft-edge://?url=https://www.youtube.com/watch?v=dQw4w9WgXcQ";
        private const string FAKE_ACTION_TEXT = "Get access now!";
        private const string FAKE_DESCRIPTION = "It's like Windows 11, but if people actually wanted it";

        public static void Main()
        {
            string localAppDataPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            // The proper subfolder under v3 appears to always be 338387, but check all folders just in case
            string contentParent = $"{localAppDataPath}\\Packages\\Microsoft.Windows.ContentDeliveryManager_cw5n1h2txyewy\\LocalState\\TargetedContentCache\\v3";

            // Loop through all the files to find the lockscreen overlay content file
            string[] files = Directory.GetFiles(contentParent, "*", SearchOption.AllDirectories);
            foreach (var file in files)
            {
                var lockscreenInfo = JsonSerializer.Deserialize<LockscreenInfo>(File.ReadAllText(file));
                // Check if the content type is the lockscreen overlay type
                if (lockscreenInfo.Name != "LockScreenOverlay")
                {
                    continue;
                }
                Console.WriteLine("CONTENT SOURCE FOUND: " + file);

                // Edit the content
                for (int i = 0; i < lockscreenInfo.Items.Length; i++)
                {
                    lockscreenInfo.Items[i].Properties.Title = CreateTextItem(FAKE_TITLE);
                    lockscreenInfo.Items[i].Properties.Description = CreateTextItem(FAKE_DESCRIPTION);
                    lockscreenInfo.Items[i].Properties.ActionText = CreateTextItem(FAKE_ACTION_TEXT);
                    lockscreenInfo.Items[i].Properties.OnClick.Parameters.Uri = FAKE_URI;
                    lockscreenInfo.Items[i].Properties.OnClick.Parameters.ActionText = FAKE_ACTION_TEXT;
                    lockscreenInfo.Items[i].Properties.OnClick.Parameters.Description = FAKE_DESCRIPTION;
                    lockscreenInfo.Items[i].Properties.OnClick.Parameters.Title = FAKE_TITLE;
                }
                File.WriteAllText(file, JsonSerializer.Serialize(lockscreenInfo));
            }
        }

        private static TextItem CreateTextItem(string text) => new() { Text = text };
    }

    #region Entities

    public class LockscreenInfo
    {
        [JsonPropertyName("class")] public object Class { get; set; }
        [JsonPropertyName("collections")] public object Collections { get; set; }
        [JsonPropertyName("name")] public string Name { get; set; }
        [JsonPropertyName("propertyManifest")] public object PropertyManifest { get; set; }
        [JsonPropertyName("properties")] public object Properties { get; set; }
        [JsonPropertyName("tracking")] public object Tracking { get; set; }
        [JsonPropertyName("triggers")] public object Triggers { get; set; }
        [JsonPropertyName("itemPropertyManifest")] public object ItemPropertyManifest { get; set; }
        [JsonPropertyName("items")] public Item[] Items { get; set; }
        [JsonPropertyName("subscriptionId")] public string SubscriptionId { get; set; }
        [JsonPropertyName("contentId")] public string ContentId { get; set; }
        [JsonPropertyName("encodedCreativeId")] public string EncodedCreativeId { get; set; }
        [JsonPropertyName("availability")] public int Availability { get; set; }
        [JsonPropertyName("timeStamp")] public string Timestamp { get; set; }
    }

    public class Item
    {
        [JsonPropertyName("properties")] public Properties Properties { get; set; }
        [JsonPropertyName("tracking")] public Tracking Tracking { get; set; }
        [JsonPropertyName("triggers")] public object[] Triggers { get; set; }
        [JsonPropertyName("state")] public State State { get; set; }
    }

    public class Properties
    {
        [JsonPropertyName("template")] public TextItem Template { get; set; }
        [JsonPropertyName("visualMode")] public TextItem VisualMode { get; set; }
        [JsonPropertyName("title")] public TextItem Title { get; set; }
        [JsonPropertyName("onClick")] public OnClick OnClick { get; set; }
        [JsonPropertyName("location")] public TextItem Location { get; set; }
        [JsonPropertyName("description")] public TextItem Description { get; set; }
        [JsonPropertyName("actionText")] public TextItem ActionText { get; set; }
        [JsonPropertyName("glyph")] public TextItem Glyph { get; set; }
    }


    public class State
    {
        [JsonPropertyName("shouldDisplay")] public bool ShouldDisplay { get; set; }
        [JsonPropertyName("installationState")] public long InstallationState { get; set; }
    }

    public class Tracking
    {
        [JsonPropertyName("events")] public Event[] Events { get; set; }
        [JsonPropertyName("parameterized")] public Parameterized[] Parameterized { get; set; }
    }

    public class Event
    {
        [JsonPropertyName("id")] public string Id { get; set; }
        [JsonPropertyName("name")] public string Name { get; set; }
    }

    public class Parameterized
    {
        [JsonPropertyName("uri")] public string Uri { get; set; }
    }

    public class TextItem
    {
        [JsonPropertyName("text")] public string Text { get; set; }
    }

    public class OnClick
    {
        [JsonPropertyName("event")] public string Event { get; set; }
        [JsonPropertyName("parameters")] public Parameters Parameters { get; set; }
        [JsonPropertyName("action")] public string Action { get; set; }
    }

    public class Parameters
    {
        [JsonPropertyName("uri")] public string Uri { get; set; }
        [JsonPropertyName("actionText")] public string ActionText { get; set; }
        [JsonPropertyName("ctx.action")] public string CtxAction { get; set; }
        [JsonPropertyName("ctx.containerPath")] public string CtxContainerPath { get; set; }
        [JsonPropertyName("ctx.contentId")] public string CtxContentId { get; set; }
        [JsonPropertyName("ctx.creativeId")] public string CtxCreativeId { get; set; }
        [JsonPropertyName("ctx.cv")] public string CtxCv { get; set; }
        [JsonPropertyName("ctx.expiration")] public string CtxExpiration { get; set; }
        [JsonPropertyName("ctx.placementId")] public string CtxPlacementId { get; set; }
        [JsonPropertyName("description")] public string Description { get; set; }
        [JsonPropertyName("glyph")] public string Glyph { get; set; }
        [JsonPropertyName("location")] public string Location { get; set; }
        [JsonPropertyName("onClick")] public string OnClick { get; set; }
        [JsonPropertyName("template")] public string Template { get; set; }
        [JsonPropertyName("title")] public string Title { get; set; }
        [JsonPropertyName("visualMode")] public string VisualMode { get; set; }
    }

    #endregion Entities
}