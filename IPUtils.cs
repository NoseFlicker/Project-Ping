using System.Net;

internal struct IPUtils
{
    public static string get_ip() => new WebClient().DownloadString("https://icanhazip.com/");
}