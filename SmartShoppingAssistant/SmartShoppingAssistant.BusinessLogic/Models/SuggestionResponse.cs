using System.ComponentModel;
using System.Text.Json.Serialization;

namespace SmartShoppingAssistant.BusinessLogic.Models;

[Description("Suggested products for the current cart")]
public sealed class SuggestionResponse
{
    [JsonPropertyName("suggestions")]
    public List<ProductSuggestion> Suggestions { get; set; } = [];
}

public sealed class ProductSuggestion
{
    [JsonPropertyName("productId")]
    public int ProductId { get; set; }

    [JsonPropertyName("productName")]
    public string ProductName { get; set; } = "";

    [JsonPropertyName("price")]
    public decimal Price { get; set; }

    [JsonPropertyName("category")]
    public string Category { get; set; } = "";

    [JsonPropertyName("reason")]
    public string Reason { get; set; } = "";
}
