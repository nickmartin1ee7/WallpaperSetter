using System;
using System.Collections.Generic;
using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace WallpaperSetter.Library.Instagram
{
    public class InstagramResponse
    {
        [JsonProperty("config")] public Config Config { get; set; }

        [JsonProperty("country_code")] public string CountryCode { get; set; }

        [JsonProperty("language_code")] public string LanguageCode { get; set; }

        [JsonProperty("locale")] public string Locale { get; set; }

        [JsonProperty("entry_data")] public EntryData EntryData { get; set; }

        [JsonProperty("hostname")] public string Hostname { get; set; }

        [JsonProperty("is_whitelisted_crawl_bot")]
        public bool IsWhitelistedCrawlBot { get; set; }

        [JsonProperty("connection_quality_rating")]
        public string ConnectionQualityRating { get; set; }

        [JsonProperty("deployment_stage")] public string DeploymentStage { get; set; }

        [JsonProperty("platform")] public string Platform { get; set; }

        [JsonProperty("nonce")] public string Nonce { get; set; }

        [JsonProperty("mid_pct")] public double MidPct { get; set; }

        [JsonProperty("zero_data")] public ServerChecks ZeroData { get; set; }

        [JsonProperty("cache_schema_version")] public long CacheSchemaVersion { get; set; }

        [JsonProperty("server_checks")] public ServerChecks ServerChecks { get; set; }

        [JsonProperty("knobx")] public Dictionary<string, Knobx> Knobx { get; set; }

        [JsonProperty("to_cache")] public ToCache ToCache { get; set; }

        [JsonProperty("device_id")] public string DeviceId { get; set; }

        [JsonProperty("browser_push_pub_key")] public string BrowserPushPubKey { get; set; }

        [JsonProperty("encryption")] public Encryption Encryption { get; set; }

        [JsonProperty("is_dev")] public bool IsDev { get; set; }

        [JsonProperty("signal_collection_config")]
        public object SignalCollectionConfig { get; set; }

        [JsonProperty("rollout_hash")] public string RolloutHash { get; set; }

        [JsonProperty("bundle_variant")] public string BundleVariant { get; set; }

        [JsonProperty("frontend_env")] public string FrontendEnv { get; set; }
    }

    public class Config
    {
        [JsonProperty("csrf_token")] public string CsrfToken { get; set; }

        [JsonProperty("viewer")] public object Viewer { get; set; }

        [JsonProperty("viewerId")] public object ViewerId { get; set; }
    }

    public class Encryption
    {
        [JsonProperty("key_id")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long KeyId { get; set; }

        [JsonProperty("public_key")] public string PublicKey { get; set; }

        [JsonProperty("version")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long Version { get; set; }
    }

    public class EntryData
    {
        [JsonProperty("TagPage")] public List<TagPage> TagPage { get; set; }
    }

    public class TagPage
    {
        [JsonProperty("graphql")] public Graphql Graphql { get; set; }
    }

    public class Graphql
    {
        [JsonProperty("hashtag")] public Hashtag Hashtag { get; set; }
    }

    public class Hashtag
    {
        [JsonProperty("id")] public string Id { get; set; }

        [JsonProperty("name")] public string Name { get; set; }

        [JsonProperty("allow_following")] public bool AllowFollowing { get; set; }

        [JsonProperty("is_following")] public bool IsFollowing { get; set; }

        [JsonProperty("is_top_media_only")] public bool IsTopMediaOnly { get; set; }

        [JsonProperty("profile_pic_url")] public Uri ProfilePicUrl { get; set; }

        [JsonProperty("edge_hashtag_to_media")]
        public EdgeHashtagToMedia EdgeHashtagToMedia { get; set; }

        [JsonProperty("edge_hashtag_to_top_posts")]
        public EdgeHashtagToTopPosts EdgeHashtagToTopPosts { get; set; }

        [JsonProperty("edge_hashtag_to_content_advisory")]
        public EdgeHashtagToContentAdvisory EdgeHashtagToContentAdvisory { get; set; }

        [JsonProperty("edge_hashtag_to_related_tags")]
        public EdgeHashtagToNullStateClass EdgeHashtagToRelatedTags { get; set; }

        [JsonProperty("edge_hashtag_to_null_state")]
        public EdgeHashtagToNullStateClass EdgeHashtagToNullState { get; set; }
    }

    public class EdgeHashtagToContentAdvisory
    {
        [JsonProperty("count")] public long Count { get; set; }

        [JsonProperty("edges")] public List<object> Edges { get; set; }
    }

    public class EdgeHashtagToMedia
    {
        [JsonProperty("count")] public long Count { get; set; }

        [JsonProperty("page_info")] public PageInfo PageInfo { get; set; }

        [JsonProperty("edges")] public List<EdgeHashtagToMediaEdge> Edges { get; set; }
    }

    public class EdgeHashtagToMediaEdge
    {
        [JsonProperty("node")] public PurpleNode Node { get; set; }
    }

    public class PurpleNode
    {
        [JsonProperty("comments_disabled", NullValueHandling = NullValueHandling.Ignore)]
        public bool? CommentsDisabled { get; set; }

        [JsonProperty("__typename")] public Typename Typename { get; set; }

        [JsonProperty("id")] public string Id { get; set; }

        [JsonProperty("edge_media_to_caption")]
        public EdgeHashtagToNullStateClass EdgeMediaToCaption { get; set; }

        [JsonProperty("shortcode")] public string Shortcode { get; set; }

        [JsonProperty("edge_media_to_comment")]
        public EdgeLikedByClass EdgeMediaToComment { get; set; }

        [JsonProperty("taken_at_timestamp")] public long TakenAtTimestamp { get; set; }

        [JsonProperty("dimensions")] public Dimensions Dimensions { get; set; }

        [JsonProperty("display_url")] public Uri DisplayUrl { get; set; }

        [JsonProperty("edge_liked_by")] public EdgeLikedByClass EdgeLikedBy { get; set; }

        [JsonProperty("edge_media_preview_like")]
        public EdgeLikedByClass EdgeMediaPreviewLike { get; set; }

        [JsonProperty("owner")] public Owner Owner { get; set; }

        [JsonProperty("thumbnail_src")] public Uri ThumbnailSrc { get; set; }

        [JsonProperty("thumbnail_resources")] public List<ThumbnailResource> ThumbnailResources { get; set; }

        [JsonProperty("is_video")] public bool IsVideo { get; set; }

        [JsonProperty("accessibility_caption")]
        public string AccessibilityCaption { get; set; }

        [JsonProperty("product_type", NullValueHandling = NullValueHandling.Ignore)]
        public string ProductType { get; set; }

        [JsonProperty("video_view_count", NullValueHandling = NullValueHandling.Ignore)]
        public long? VideoViewCount { get; set; }
    }

    public class Dimensions
    {
        [JsonProperty("height")] public long Height { get; set; }

        [JsonProperty("width")] public long Width { get; set; }
    }

    public class EdgeLikedByClass
    {
        [JsonProperty("count")] public long Count { get; set; }
    }

    public class EdgeHashtagToNullStateClass
    {
        [JsonProperty("edges")] public List<EdgeHashtagToNullStateEdge> Edges { get; set; }
    }

    public class EdgeHashtagToNullStateEdge
    {
        [JsonProperty("node")] public FluffyNode Node { get; set; }
    }

    public class FluffyNode
    {
        [JsonProperty("text")] public string Text { get; set; }
    }

    public class Owner
    {
        [JsonProperty("id")] public string Id { get; set; }
    }

    public class ThumbnailResource
    {
        [JsonProperty("src")] public Uri Src { get; set; }

        [JsonProperty("config_width")] public long ConfigWidth { get; set; }

        [JsonProperty("config_height")] public long ConfigHeight { get; set; }
    }

    public class PageInfo
    {
        [JsonProperty("has_next_page")] public bool HasNextPage { get; set; }

        [JsonProperty("end_cursor")] public string EndCursor { get; set; }
    }

    public class EdgeHashtagToTopPosts
    {
        [JsonProperty("edges")] public List<EdgeHashtagToMediaEdge> Edges { get; set; }
    }

    public class ServerChecks
    {
    }

    public class ToCache
    {
        [JsonProperty("gatekeepers")] public Dictionary<string, bool> Gatekeepers { get; set; }

        [JsonProperty("qe")] public Qe Qe { get; set; }

        [JsonProperty("probably_has_app")] public bool ProbablyHasApp { get; set; }

        [JsonProperty("cb")] public bool Cb { get; set; }
    }

    public class Qe
    {
        [JsonProperty("0")] public The0 The0 { get; set; }

        [JsonProperty("12")] public The12 The12 { get; set; }

        [JsonProperty("13")] public The100 The13 { get; set; }

        [JsonProperty("16")] public The100 The16 { get; set; }

        [JsonProperty("21")] public The21 The21 { get; set; }

        [JsonProperty("22")] public The22 The22 { get; set; }

        [JsonProperty("23")] public The101 The23 { get; set; }

        [JsonProperty("25")] public The109 The25 { get; set; }

        [JsonProperty("26")] public The26 The26 { get; set; }

        [JsonProperty("28")] public The100 The28 { get; set; }

        [JsonProperty("29")] public The109 The29 { get; set; }

        [JsonProperty("31")] public The109 The31 { get; set; }

        [JsonProperty("33")] public The109 The33 { get; set; }

        [JsonProperty("34")] public The100 The34 { get; set; }

        [JsonProperty("36")] public The101 The36 { get; set; }

        [JsonProperty("37")] public The100 The37 { get; set; }

        [JsonProperty("39")] public The101 The39 { get; set; }

        [JsonProperty("41")] public The41 The41 { get; set; }

        [JsonProperty("42")] public The100 The42 { get; set; }

        [JsonProperty("43")] public The101 The43 { get; set; }

        [JsonProperty("44")] public The44 The44 { get; set; }

        [JsonProperty("45")] public The45 The45 { get; set; }

        [JsonProperty("46")] public The100 The46 { get; set; }

        [JsonProperty("47")] public The101 The47 { get; set; }

        [JsonProperty("49")] public The100 The49 { get; set; }

        [JsonProperty("50")] public The100 The50 { get; set; }

        [JsonProperty("54")] public The100 The54 { get; set; }

        [JsonProperty("58")] public The22 The58 { get; set; }

        [JsonProperty("59")] public The100 The59 { get; set; }

        [JsonProperty("62")] public The100 The62 { get; set; }

        [JsonProperty("67")] public The101 The67 { get; set; }

        [JsonProperty("69")] public The100 The69 { get; set; }

        [JsonProperty("71")] public The71 The71 { get; set; }

        [JsonProperty("72")] public The108 The72 { get; set; }

        [JsonProperty("73")] public The100 The73 { get; set; }

        [JsonProperty("74")] public The101 The74 { get; set; }

        [JsonProperty("75")] public The100 The75 { get; set; }

        [JsonProperty("77")] public The159 The77 { get; set; }

        [JsonProperty("80")] public The101 The80 { get; set; }

        [JsonProperty("84")] public The101 The84 { get; set; }

        [JsonProperty("85")] public The124 The85 { get; set; }

        [JsonProperty("87")] public The100 The87 { get; set; }

        [JsonProperty("93")] public The100 The93 { get; set; }

        [JsonProperty("95")] public The109 The95 { get; set; }

        [JsonProperty("98")] public The159 The98 { get; set; }

        [JsonProperty("100")] public The100 The100 { get; set; }

        [JsonProperty("101")] public The101 The101 { get; set; }

        [JsonProperty("108")] public The108 The108 { get; set; }

        [JsonProperty("109")] public The109 The109 { get; set; }

        [JsonProperty("111")] public The101 The111 { get; set; }

        [JsonProperty("113")] public The101 The113 { get; set; }

        [JsonProperty("117")] public The100 The117 { get; set; }

        [JsonProperty("118")] public The101 The118 { get; set; }

        [JsonProperty("119")] public The100 The119 { get; set; }

        [JsonProperty("121")] public The100 The121 { get; set; }

        [JsonProperty("122")] public The100 The122 { get; set; }

        [JsonProperty("123")] public The101 The123 { get; set; }

        [JsonProperty("124")] public The124 The124 { get; set; }

        [JsonProperty("125")] public The100 The125 { get; set; }

        [JsonProperty("127")] public The101 The127 { get; set; }

        [JsonProperty("128")] public The101 The128 { get; set; }

        [JsonProperty("129")] public The101 The129 { get; set; }

        [JsonProperty("131")] public The101 The131 { get; set; }

        [JsonProperty("132")] public The100 The132 { get; set; }

        [JsonProperty("135")] public The101 The135 { get; set; }

        [JsonProperty("137")] public The109 The137 { get; set; }

        [JsonProperty("141")] public The101 The141 { get; set; }

        [JsonProperty("142")] public The100 The142 { get; set; }

        [JsonProperty("143")] public The101 The143 { get; set; }

        [JsonProperty("146")] public The101 The146 { get; set; }

        [JsonProperty("148")] public The101 The148 { get; set; }

        [JsonProperty("149")] public The100 The149 { get; set; }

        [JsonProperty("150")] public The150 The150 { get; set; }

        [JsonProperty("151")] public The101 The151 { get; set; }

        [JsonProperty("152")] public The109 The152 { get; set; }

        [JsonProperty("154")] public The100 The154 { get; set; }

        [JsonProperty("155")] public The109 The155 { get; set; }

        [JsonProperty("156")] public The156 The156 { get; set; }

        [JsonProperty("158")] public The109 The158 { get; set; }

        [JsonProperty("159")] public The159 The159 { get; set; }

        [JsonProperty("160")] public The109 The160 { get; set; }

        [JsonProperty("161")] public The100 The161 { get; set; }

        [JsonProperty("app_upsell")] public AppUpsell AppUpsell { get; set; }

        [JsonProperty("igl_app_upsell")] public AppUpsell IglAppUpsell { get; set; }

        [JsonProperty("notif")] public AppUpsell Notif { get; set; }

        [JsonProperty("onetaplogin")] public AppUpsell Onetaplogin { get; set; }

        [JsonProperty("felix_clear_fb_cookie")]
        public AppUpsell FelixClearFbCookie { get; set; }

        [JsonProperty("felix_creation_duration_limits")]
        public AppUpsell FelixCreationDurationLimits { get; set; }

        [JsonProperty("felix_creation_fb_crossposting")]
        public AppUpsell FelixCreationFbCrossposting { get; set; }

        [JsonProperty("felix_creation_fb_crossposting_v2")]
        public AppUpsell FelixCreationFbCrosspostingV2 { get; set; }

        [JsonProperty("felix_creation_validation")]
        public AppUpsell FelixCreationValidation { get; set; }

        [JsonProperty("post_options")] public AppUpsell PostOptions { get; set; }

        [JsonProperty("sticker_tray")] public AppUpsell StickerTray { get; set; }

        [JsonProperty("web_sentry")] public AppUpsell WebSentry { get; set; }
    }

    public class AppUpsell
    {
        [JsonProperty("g")] public string G { get; set; }

        [JsonProperty("p")] public ServerChecks P { get; set; }
    }

    public class The0
    {
        [JsonProperty("p")] public The0_P P { get; set; }

        [JsonProperty("l")] public ServerChecks L { get; set; }

        [JsonProperty("qex")] public bool Qex { get; set; }
    }

    public class The0_P
    {
        [JsonProperty("9")] public bool The9 { get; set; }
    }

    public class The100
    {
        [JsonProperty("p")] public LClass P { get; set; }

        [JsonProperty("l")] public ServerChecks L { get; set; }

        [JsonProperty("qex")] public bool Qex { get; set; }
    }

    public class LClass
    {
        [JsonProperty("0")] public bool The0 { get; set; }
    }

    public class The101
    {
        [JsonProperty("p")] public Dictionary<string, bool> P { get; set; }

        [JsonProperty("l")] public ServerChecks L { get; set; }

        [JsonProperty("qex")] public bool Qex { get; set; }
    }

    public class The108
    {
        [JsonProperty("p")] public Dictionary<string, bool> P { get; set; }

        [JsonProperty("l")] public Dictionary<string, bool> L { get; set; }

        [JsonProperty("qex")] public bool Qex { get; set; }
    }

    public class The109
    {
        [JsonProperty("p")] public ServerChecks P { get; set; }

        [JsonProperty("l")] public ServerChecks L { get; set; }

        [JsonProperty("qex")] public bool Qex { get; set; }
    }

    public class The12
    {
        [JsonProperty("p")] public The12_P P { get; set; }

        [JsonProperty("l")] public ServerChecks L { get; set; }

        [JsonProperty("qex")] public bool Qex { get; set; }
    }

    public class The12_P
    {
        [JsonProperty("0")] public long The0 { get; set; }
    }

    public class The124
    {
        [JsonProperty("p")] public Dictionary<string, The124_P> P { get; set; }

        [JsonProperty("l")] public ServerChecks L { get; set; }

        [JsonProperty("qex")] public bool Qex { get; set; }
    }

    public class The150
    {
        [JsonProperty("p")] public Dictionary<string, Knobx> P { get; set; }

        [JsonProperty("l")] public ServerChecks L { get; set; }

        [JsonProperty("qex")] public bool Qex { get; set; }
    }

    public class The156
    {
        [JsonProperty("p")] public LClass P { get; set; }

        [JsonProperty("l")] public LClass L { get; set; }

        [JsonProperty("qex")] public bool Qex { get; set; }
    }

    public class The159
    {
        [JsonProperty("p")] public The159_P P { get; set; }

        [JsonProperty("l")] public ServerChecks L { get; set; }

        [JsonProperty("qex")] public bool Qex { get; set; }
    }

    public class The159_P
    {
        [JsonProperty("1")] public bool The1 { get; set; }
    }

    public class The21
    {
        [JsonProperty("p")] public The21_P P { get; set; }

        [JsonProperty("l")] public ServerChecks L { get; set; }

        [JsonProperty("qex")] public bool Qex { get; set; }
    }

    public class The21_P
    {
        [JsonProperty("2")] public bool The2 { get; set; }
    }

    public class The22
    {
        [JsonProperty("p")] public Dictionary<string, The22_P> P { get; set; }

        [JsonProperty("l")] public ServerChecks L { get; set; }

        [JsonProperty("qex")] public bool Qex { get; set; }
    }

    public class The26
    {
        [JsonProperty("p")] public The26_P P { get; set; }

        [JsonProperty("l")] public ServerChecks L { get; set; }

        [JsonProperty("qex")] public bool Qex { get; set; }
    }

    public class The26_P
    {
        [JsonProperty("0")] public string The0 { get; set; }
    }

    public class The41
    {
        [JsonProperty("p")] public The41_P P { get; set; }

        [JsonProperty("l")] public ServerChecks L { get; set; }

        [JsonProperty("qex")] public bool Qex { get; set; }
    }

    public class The41_P
    {
        [JsonProperty("3")] public bool The3 { get; set; }
    }

    public class The44
    {
        [JsonProperty("p")] public Dictionary<string, The44_P> P { get; set; }

        [JsonProperty("l")] public ServerChecks L { get; set; }

        [JsonProperty("qex")] public bool Qex { get; set; }
    }

    public class The45
    {
        [JsonProperty("p")] public Dictionary<string, The45_P> P { get; set; }

        [JsonProperty("l")] public L L { get; set; }

        [JsonProperty("qex")] public bool Qex { get; set; }
    }

    public class L
    {
        [JsonProperty("60")] public bool The60 { get; set; }
    }

    public class The71
    {
        [JsonProperty("p")] public The71_P P { get; set; }

        [JsonProperty("l")] public ServerChecks L { get; set; }

        [JsonProperty("qex")] public bool Qex { get; set; }
    }

    public class The71_P
    {
        [JsonProperty("1")] public string The1 { get; set; }
    }

    public enum Typename
    {
        GraphImage,
        GraphSidecar,
        GraphVideo
    }

    public struct Knobx
    {
        public bool? Bool;
        public long? Integer;

        public static implicit operator Knobx(bool Bool)
        {
            return new Knobx {Bool = Bool};
        }

        public static implicit operator Knobx(long Integer)
        {
            return new Knobx {Integer = Integer};
        }
    }

    public struct The124_P
    {
        public bool? Bool;
        public string String;

        public static implicit operator The124_P(bool Bool)
        {
            return new The124_P {Bool = Bool};
        }

        public static implicit operator The124_P(string String)
        {
            return new The124_P {String = String};
        }
    }

    public struct The22_P
    {
        public bool? Bool;
        public double? Double;

        public static implicit operator The22_P(bool Bool)
        {
            return new The22_P {Bool = Bool};
        }

        public static implicit operator The22_P(double Double)
        {
            return new The22_P {Double = Double};
        }
    }

    public struct The44_P
    {
        public double? Double;
        public string String;

        public static implicit operator The44_P(double Double)
        {
            return new The44_P {Double = Double};
        }

        public static implicit operator The44_P(string String)
        {
            return new The44_P {String = String};
        }
    }

    public struct The45_P
    {
        public bool? Bool;
        public long? Integer;
        public string String;

        public static implicit operator The45_P(bool Bool)
        {
            return new The45_P {Bool = Bool};
        }

        public static implicit operator The45_P(long Integer)
        {
            return new The45_P {Integer = Integer};
        }

        public static implicit operator The45_P(string String)
        {
            return new The45_P {String = String};
        }
    }

    internal static class Converter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters =
            {
                TypenameConverter.Singleton,
                KnobxConverter.Singleton,
                The124PConverter.Singleton,
                The22PConverter.Singleton,
                The44PConverter.Singleton,
                The45PConverter.Singleton,
                new IsoDateTimeConverter {DateTimeStyles = DateTimeStyles.AssumeUniversal}
            }
        };
    }

    internal class ParseStringConverter : JsonConverter
    {
        public static readonly ParseStringConverter Singleton = new ParseStringConverter();

        public override bool CanConvert(Type t)
        {
            return t == typeof(long) || t == typeof(long?);
        }

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            long l;
            if (long.TryParse(value, out l)) return l;
            throw new Exception("Cannot unmarshal type long");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }

            var value = (long) untypedValue;
            serializer.Serialize(writer, value.ToString());
        }
    }

    internal class TypenameConverter : JsonConverter
    {
        public static readonly TypenameConverter Singleton = new TypenameConverter();

        public override bool CanConvert(Type t)
        {
            return t == typeof(Typename) || t == typeof(Typename?);
        }

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            switch (value)
            {
                case "GraphImage":
                    return Typename.GraphImage;
                case "GraphSidecar":
                    return Typename.GraphSidecar;
                case "GraphVideo":
                    return Typename.GraphVideo;
            }

            throw new Exception("Cannot unmarshal type Typename");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }

            var value = (Typename) untypedValue;
            switch (value)
            {
                case Typename.GraphImage:
                    serializer.Serialize(writer, "GraphImage");
                    return;
                case Typename.GraphSidecar:
                    serializer.Serialize(writer, "GraphSidecar");
                    return;
                case Typename.GraphVideo:
                    serializer.Serialize(writer, "GraphVideo");
                    return;
            }

            throw new Exception("Cannot marshal type Typename");
        }
    }

    internal class KnobxConverter : JsonConverter
    {
        public static readonly KnobxConverter Singleton = new KnobxConverter();

        public override bool CanConvert(Type t)
        {
            return t == typeof(Knobx) || t == typeof(Knobx?);
        }

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            switch (reader.TokenType)
            {
                case JsonToken.Integer:
                    var integerValue = serializer.Deserialize<long>(reader);
                    return new Knobx {Integer = integerValue};
                case JsonToken.Boolean:
                    var boolValue = serializer.Deserialize<bool>(reader);
                    return new Knobx {Bool = boolValue};
            }

            throw new Exception("Cannot unmarshal type Knobx");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            var value = (Knobx) untypedValue;
            if (value.Integer != null)
            {
                serializer.Serialize(writer, value.Integer.Value);
                return;
            }

            if (value.Bool != null)
            {
                serializer.Serialize(writer, value.Bool.Value);
                return;
            }

            throw new Exception("Cannot marshal type Knobx");
        }
    }

    internal class The124PConverter : JsonConverter
    {
        public static readonly The124PConverter Singleton = new The124PConverter();

        public override bool CanConvert(Type t)
        {
            return t == typeof(The124_P) || t == typeof(The124_P?);
        }

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            switch (reader.TokenType)
            {
                case JsonToken.Boolean:
                    var boolValue = serializer.Deserialize<bool>(reader);
                    return new The124_P {Bool = boolValue};
                case JsonToken.String:
                case JsonToken.Date:
                    var stringValue = serializer.Deserialize<string>(reader);
                    return new The124_P {String = stringValue};
            }

            throw new Exception("Cannot unmarshal type The124_P");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            var value = (The124_P) untypedValue;
            if (value.Bool != null)
            {
                serializer.Serialize(writer, value.Bool.Value);
                return;
            }

            if (value.String != null)
            {
                serializer.Serialize(writer, value.String);
                return;
            }

            throw new Exception("Cannot marshal type The124_P");
        }
    }

    internal class The22PConverter : JsonConverter
    {
        public static readonly The22PConverter Singleton = new The22PConverter();

        public override bool CanConvert(Type t)
        {
            return t == typeof(The22_P) || t == typeof(The22_P?);
        }

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            switch (reader.TokenType)
            {
                case JsonToken.Integer:
                case JsonToken.Float:
                    var doubleValue = serializer.Deserialize<double>(reader);
                    return new The22_P {Double = doubleValue};
                case JsonToken.Boolean:
                    var boolValue = serializer.Deserialize<bool>(reader);
                    return new The22_P {Bool = boolValue};
            }

            throw new Exception("Cannot unmarshal type The22_P");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            var value = (The22_P) untypedValue;
            if (value.Double != null)
            {
                serializer.Serialize(writer, value.Double.Value);
                return;
            }

            if (value.Bool != null)
            {
                serializer.Serialize(writer, value.Bool.Value);
                return;
            }

            throw new Exception("Cannot marshal type The22_P");
        }
    }

    internal class The44PConverter : JsonConverter
    {
        public static readonly The44PConverter Singleton = new The44PConverter();

        public override bool CanConvert(Type t)
        {
            return t == typeof(The44_P) || t == typeof(The44_P?);
        }

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            switch (reader.TokenType)
            {
                case JsonToken.Integer:
                case JsonToken.Float:
                    var doubleValue = serializer.Deserialize<double>(reader);
                    return new The44_P {Double = doubleValue};
                case JsonToken.String:
                case JsonToken.Date:
                    var stringValue = serializer.Deserialize<string>(reader);
                    return new The44_P {String = stringValue};
            }

            throw new Exception("Cannot unmarshal type The44_P");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            var value = (The44_P) untypedValue;
            if (value.Double != null)
            {
                serializer.Serialize(writer, value.Double.Value);
                return;
            }

            if (value.String != null)
            {
                serializer.Serialize(writer, value.String);
                return;
            }

            throw new Exception("Cannot marshal type The44_P");
        }
    }

    internal class The45PConverter : JsonConverter
    {
        public static readonly The45PConverter Singleton = new The45PConverter();

        public override bool CanConvert(Type t)
        {
            return t == typeof(The45_P) || t == typeof(The45_P?);
        }

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            switch (reader.TokenType)
            {
                case JsonToken.Integer:
                    var integerValue = serializer.Deserialize<long>(reader);
                    return new The45_P {Integer = integerValue};
                case JsonToken.Boolean:
                    var boolValue = serializer.Deserialize<bool>(reader);
                    return new The45_P {Bool = boolValue};
                case JsonToken.String:
                case JsonToken.Date:
                    var stringValue = serializer.Deserialize<string>(reader);
                    return new The45_P {String = stringValue};
            }

            throw new Exception("Cannot unmarshal type The45_P");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            var value = (The45_P) untypedValue;
            if (value.Integer != null)
            {
                serializer.Serialize(writer, value.Integer.Value);
                return;
            }

            if (value.Bool != null)
            {
                serializer.Serialize(writer, value.Bool.Value);
                return;
            }

            if (value.String != null)
            {
                serializer.Serialize(writer, value.String);
                return;
            }

            throw new Exception("Cannot marshal type The45_P");
        }
    }
}