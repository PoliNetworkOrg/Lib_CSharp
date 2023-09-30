#region

using System.Net;
using System.Text;
using HtmlAgilityPack;
using PoliNetwork.Core.Enums;
using PoliNetwork.Html.Objects.Web;

#endregion

namespace PoliNetwork.Html.Utils;

public static class HtmlUtil
{
    public static Task<WebReply> DownloadHtmlAsync(
        string urlAddress,
        bool useCache = true,
        Func<string,WebReply?>? cacheCheckIfToUse = null,
        Action<string,string>? cacheSaveToCache = null,
        CacheTypeEnum cacheTypeEnum = CacheTypeEnum.NONE)
    {
        try
        {
            if (useCache && cacheCheckIfToUse != null)
            {
                var resultFromCache =  cacheCheckIfToUse.Invoke(urlAddress); // CacheUtil.CheckIfTouse(urlAddress)
                if (resultFromCache != null)
                    return Task.FromResult(resultFromCache);
            }

            HttpClient httpClient = new();
            var task = httpClient.GetByteArrayAsync(urlAddress);
            task.Wait();
            var response = task.Result;
            var s = Encoding.UTF8.GetString(response, 0, response.Length);
            //s = FixTableContentFromCache(cacheTypeEnum, s);

            switch (cacheTypeEnum)
            {
                case CacheTypeEnum.ROOMTABLE:
                    s = FixFromTableRoomCache(s);
                    break;
                default:
                    break;
            }

            if (useCache && cacheSaveToCache != null)
                cacheSaveToCache.Invoke(urlAddress, s); //  CacheUtil.SaveToCache(urlAddress, s);
              

            return Task.FromResult(new WebReply(s, HttpStatusCode.OK));
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            return Task.FromResult(new WebReply(null, HttpStatusCode.ExpectationFailed));
        }
    }

    private static string FixFromTableRoomCache(string s)
    {
        var doc = new HtmlDocument();
        doc.LoadHtml(s);
        var t1 = NodeUtil.GetElementsByTagAndClassName(doc.DocumentNode, "", "BoxInfoCard", 1);
        var t3 = NodeUtil.GetElementsByTagAndClassName(t1?[0], "", "scrollContent");
        s = t3?[0].InnerHtml ?? "";
        return s;
    }

}