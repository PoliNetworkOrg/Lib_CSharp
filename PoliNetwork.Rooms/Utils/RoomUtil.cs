#region

using HtmlAgilityPack;
using PoliNetwork.Core.Enums;
using PoliNetwork.Html.Objects.Web;
using PoliNetwork.Html.Utils;

#endregion

namespace PoliNetwork.Rooms.Utils;

public static class RoomUtil
{
    public const string RoomInfoUrls = "https://www7.ceda.polimi.it/spazi/spazi/controller/";


    public static async Task<Tuple<List<HtmlNode>?, string>> GetDailySituationOnDate(DateTime? date, string sede,
        Action<string, string>? cacheSaveToCache, Func<string, WebReply?>? cacheCheckIfToUse)
    {
        date ??= DateTime.Today;
        var day = date?.Day;
        var month = date?.Month;
        var year = date?.Year;

        if (string.IsNullOrEmpty(sede)) return new Tuple<List<HtmlNode>?, string>(null, "sede empty");

        var url = "https://www7.ceda.polimi.it/spazi/spazi/controller/OccupazioniGiornoEsatto.do?" +
                  "csic=" + sede +
                  "&categoria=tutte" +
                  "&tipologia=tutte" +
                  "&giorno_day=" + day +
                  "&giorno_month=" + month +
                  "&giorno_year=" + year +
                  "&jaf_giorno_date_format=dd%2FMM%2Fyyyy&evn_visualizza=";

        var html = await HtmlUtil.DownloadHtmlAsync(url, false, cacheTypeEnum: CacheTypeEnum.ROOMTABLE,
            cacheSaveToCache: cacheSaveToCache,
            cacheCheckIfToUse: cacheCheckIfToUse);
        if (html.IsValid() == false) return new Tuple<List<HtmlNode>?, string>(null, "html invalid");

        var doc = new HtmlDocument();
        doc.LoadHtml(html.GetData());
        List<HtmlNode> nodes = new();

        var node = new HtmlNode(HtmlNodeType.Element, doc, 0)
        {
            InnerHtml = doc.DocumentNode.InnerHtml
        };
        nodes.Add(node);
        return new Tuple<List<HtmlNode>?, string>(nodes, string.Empty);
    }

    internal static int GetShiftSlotFromTime(DateTime time)
    {
        var shiftSlot = (time.Hour - 8) * 4;
        shiftSlot += time.Minute / 15;
        return shiftSlot;
    }
}