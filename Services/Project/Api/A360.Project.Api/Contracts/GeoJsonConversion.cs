using System.Text.Json;
using MongoDB.Bson;

namespace A360.Project.Api.Contracts;

internal static class GeoJsonConversion
{
    public static List<BsonDocument> ToBsonDocuments(List<JsonElement>? geoJsonData)
    {
        return geoJsonData?.Select(element => BsonDocument.Parse(element.GetRawText())).ToList() ?? [];
    }

    public static List<JsonElement> ToJsonElements(List<BsonDocument> geoJsonData)
    {
        return geoJsonData.Select(document => JsonDocument.Parse(document.ToJson()).RootElement).ToList();
    }
}
