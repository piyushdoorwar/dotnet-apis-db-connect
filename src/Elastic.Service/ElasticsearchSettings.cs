namespace Elastic.Service;

public class ElasticsearchSettings
{
    public string Uri { get; set; } = string.Empty;
    public string EncodedApiKey { get; set; } = string.Empty;
    public string IndexName { get; set; } = "products";
}

