using System.Text.Json.Serialization;

namespace LockScreenExploit;

[JsonSerializable(typeof(LockScreenInfo))]
internal partial class LockScreenJsonContext : JsonSerializerContext { }

public class LockScreenInfo
{
    [JsonPropertyName("class")] public string Class { get; set; }
    [JsonPropertyName("collections")] public string[] Collections { get; set; }
    [JsonPropertyName("name")] public string Name { get; set; }
    [JsonPropertyName("propertyManifest")] public PropertyManifest PropertyManifest { get; set; }
    [JsonPropertyName("properties")] public Properties Properties { get; set; }
    [JsonPropertyName("tracking")] public Tracking Tracking { get; set; }
    [JsonPropertyName("triggers")] public TriggerItem[] Triggers { get; set; }
    [JsonPropertyName("itemPropertyManifest")] public ItemPropertyManifest ItemPropertyManifest { get; set; }
    [JsonPropertyName("items")] public Item[] Items { get; set; }
    [JsonPropertyName("subscriptionId")] public string SubscriptionId { get; set; }
    [JsonPropertyName("contentId")] public string ContentId { get; set; }
    [JsonPropertyName("encodedCreativeId")] public string EncodedCreativeId { get; set; }
    [JsonPropertyName("availability")] public int Availability { get; set; }
    [JsonPropertyName("timeStamp")] public string Timestamp { get; set; }
}

public class Item
{
    [JsonPropertyName("entityId")] public string EntityId { get; set; }
    [JsonPropertyName("properties")] public ItemProperties Properties { get; set; }
    [JsonPropertyName("tracking")] public Tracking Tracking { get; set; }
    [JsonPropertyName("triggers")] public TriggerItem[] Triggers { get; set; }
    [JsonPropertyName("state")] public State State { get; set; }
}

public class TriggerItem
{
    [JsonPropertyName("action")] public string Action { get; set; }
    [JsonPropertyName("trigger")] public string Trigger { get; set; }
}

public class ItemPropertyManifest
{
    [JsonPropertyName("template")] public PropertyType Template { get; set; }
    [JsonPropertyName("visualMode")] public PropertyType VisualMode { get; set; }
    [JsonPropertyName("title")] public PropertyType Title { get; set; }
    [JsonPropertyName("glyph")] public PropertyTypeOptional Glyph { get; set; }
    [JsonPropertyName("description")] public PropertyTypeOptional Description { get; set; }
    [JsonPropertyName("actionText")] public PropertyTypeOptional ActionText { get; set; }
    [JsonPropertyName("onClick")] public PropertyTypeOptional OnClick { get; set; }
    [JsonPropertyName("mainImage")] public PropertyTypeOptional MainImage { get; set; }
    [JsonPropertyName("location")] public PropertyType Location { get; set; }
    [JsonPropertyName("showFeedback")] public PropertyTypeOptional ShowFeedback { get; set; }
}

public class PropertyType
{
    [JsonPropertyName("type")] public string Type { get; set; }
}

public class PropertyTypeOptional
{
    [JsonPropertyName("isOptional")] public bool IsOptional { get; set; }
    [JsonPropertyName("type")] public string Type { get; set; }
}

public class ItemProperties
{
    [JsonPropertyName("template")] public TextItem Template { get; set; }
    [JsonPropertyName("title")] public TextItem Title { get; set; }
    [JsonPropertyName("actionText")] public TextItem ActionText { get; set; }
    [JsonPropertyName("onClick")] public OnClick OnClick { get; set; }
    [JsonPropertyName("glyph")] public TextItem Glyph { get; set; }
    [JsonPropertyName("description")] public TextItem Description { get; set; }
    [JsonPropertyName("location")] public TextItem Location { get; set; }
    [JsonPropertyName("visualMode")] public TextItem VisualMode { get; set; }
}

public class Properties
{
    [JsonPropertyName("onRender")] public OnRender2 OnRender { get; set; }
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

public class PropertyManifest
{
    [JsonPropertyName("onRender")] public OnRender OnRender { get; set; }
}

public class OnClick
{
    [JsonPropertyName("event")] public string Event { get; set; }
    [JsonPropertyName("parameters")] public Parameters Parameters { get; set; }
    [JsonPropertyName("action")] public string Action { get; set; }
}

public class OnRender
{
    [JsonPropertyName("type")] public string Type { get; set; }
}

public class OnRender2
{
    [JsonPropertyName("event")] public string Event { get; set; }
    [JsonPropertyName("parameters")] public Parameters2 Parameters { get; set; }
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
    [JsonPropertyName("entityId")] public string EntityId { get; set; }
    [JsonPropertyName("glyph")] public string Glyph { get; set; }
    [JsonPropertyName("location")] public string Location { get; set; }
    [JsonPropertyName("onClick")] public string OnClick { get; set; }
    [JsonPropertyName("template")] public string Template { get; set; }
    [JsonPropertyName("title")] public string Title { get; set; }
    [JsonPropertyName("visualMode")] public string VisualMode { get; set; }
}
public class Parameters2
{
    [JsonPropertyName("ctx.action")] public string CtxAction { get; set; }
    [JsonPropertyName("ctx.containerPath")] public string CtxContainerPath { get; set; }
    [JsonPropertyName("ctx.contentId")] public string CtxContentId { get; set; }
    [JsonPropertyName("ctx.creativeId")] public string CtxCreativeId { get; set; }
    [JsonPropertyName("ctx.cv")] public string CtxCv { get; set; }
    [JsonPropertyName("ctx.expiration")] public string CtxExpiration { get; set; }
    [JsonPropertyName("ctx.placementId")] public string CtxPlacementId { get; set; }
    [JsonPropertyName("onRender")] public string OnRender { get; set; }
}
