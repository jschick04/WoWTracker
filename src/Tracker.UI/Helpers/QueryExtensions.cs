using System.Collections.Specialized;
using System.Web;
using Microsoft.AspNetCore.Components;

namespace Tracker.UI.Helpers;

public static class QueryExtensions
{
    public static NameValueCollection QueryString(this NavigationManager navigationManager) =>
        HttpUtility.ParseQueryString(new Uri(navigationManager.Uri).Query);

    public static string? QueryString(this NavigationManager navigationManager, string key) =>
        navigationManager.QueryString()[key];
}
