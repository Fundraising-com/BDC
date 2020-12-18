using System;
using System.Configuration;
using System.Collections.Generic;

/// <summary>
/// Provides a typed access to the Application Settings defined in Web.Config.
/// </summary>
public sealed class Configuration {

    #region Helper Methods

    /// <summary>
    /// Returns a list of comma-delimited integers configured in the web.config.
    /// </summary>
    /// <param name="settingsKey">The key to read from web.config</param>
    /// <returns>A list of integers.</returns>
    private static List<int> GetIntlistFromConfig(string settingsKey) {
        List<int> Result = new List<int>();
        string FormGroupsString = "";
        if (ConfigurationManager.AppSettings[settingsKey] != null)
            FormGroupsString = ConfigurationManager.AppSettings[settingsKey].ToString();
        else
            throw new ConfigurationException("Key not defined in Web.config : " + settingsKey);

        try {
            foreach (string formGroup in FormGroupsString.Split(',')) {
                Result.Add(Convert.ToInt32(formGroup));
            }
        }
        catch {
            throw new ConfigurationException("Key defined in Web.config is not a valid comma-delimited list of integers :" + settingsKey);
        }

        return Result;
    }

    /// <summary>
    /// Returns the default warehouse to use for Otis forms.
    /// </summary>
    private static int GetIntFromConfig(string settingsKey) {
        string Result = "";
        if (ConfigurationManager.AppSettings[settingsKey] != null)
            Result = ConfigurationManager.AppSettings[settingsKey].ToString();

        try {
            return Int32.Parse(Result);
        }
        catch {
            throw new ConfigurationException("Key defined in Web.config is not a valid integer : " + settingsKey);
        }
    }

    #endregion

    // Server-side paging with .Skip() isn't efficient in SQL Server 2000.
    // We will instead return all records up to the page we want to display and skip first pages in code.
    // When we switch to SQL Server 2005, we can turn this flag on or remove it and fix wherever code doesn't compile.
    //public static bool UseDatabasePaging = false;
}