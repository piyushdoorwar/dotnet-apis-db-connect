namespace Couchbase.Service;

public class CouchbaseSettings
{
    public string ConnectionString { get; set; } = string.Empty;
    public string Username { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string Bucket { get; set; } = string.Empty;
}
