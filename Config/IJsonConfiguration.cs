namespace devopsgenie.service.Config
{
    public interface IJsonConfiguration
    {
        string DOGREPONOOK_URI { get; }
        string DOGREPONOOK_PORT { get; }
        string DO_ENCRYPT { get; }
        string ENCRYPTION_KEY { get; }
    }
}