﻿#region

using System.Web;
using HtmlAgilityPack;
using PoliNetwork.Core.Data;
using PoliNetwork.Rooms.Enums;
using PoliNetwork.Rooms.Objects;

#endregion

namespace PoliNetwork.Rooms.Utils;

public static class FreeRoomsUtil
{
    public static List<object?>? GetFreeRooms(HtmlNode? table, DateTime? start, DateTime? stop)
    {
        if (table?.ChildNodes == null) return null;

        var shiftStart = RoomUtil.GetShiftSlotFromTime(start ?? DateTime.Now);
        var shiftEnd = RoomUtil.GetShiftSlotFromTime(stop ?? DateTime.Now);

        var enumerable = table.ChildNodes
            .Select(child => CheckIfFree(child, shiftStart, shiftEnd)).ToList();

        return enumerable
            .Where(toAdd => toAdd != null).ToList();
    }

    private static object? CheckIfFree(HtmlNode? node, int shiftStart, int shiftEnd)
    {
        try
        {
            if (node == null)
                return null;

            if (!node.GetClasses().Contains("normalRow"))
                return null;

            if (node.ChildNodes == null)
                return null;

            if (shiftEnd < shiftStart)
                shiftEnd = shiftStart;

            if (!node.ChildNodes.Any(x =>
                    x.HasClass("dove")
                    && x.ChildNodes != null
                    && x.ChildNodes.Any(x2 => x2.Name == "a" && !x2.InnerText.ToUpper().Contains("PROVA"))
                ))
                return null;

            var roomFree = IsRoomFree(node, shiftStart, shiftEnd);
            return ExtractHtmlRoomUtil.GetAula(node, roomFree);
        }
        catch (Exception ex)
        {
            GlobalVariables.DefaultLogger.Error(ex.ToString());
            throw;
        }
    }

    private static IEnumerable<RoomOccupancyResultObject> IsRoomFree(HtmlNode? node, int shiftStart, int shiftEnd)
    {
        if (node?.ChildNodes == null)
            return new List<RoomOccupancyResultObject>();

        var colsizetotal = 0;

        var occupied = new List<RoomOccupancyResultObject>
            { new(new TimeOnly(7, 45, 0), RoomOccupancyEnum.FREE, null) };

        // the first two children are not time slots
        for (var i = 2; i < node.ChildNodes.Count; i++)
        {
            var iTime = new TimeOnly(8, 0, 0);
            iTime = iTime.AddMinutes(colsizetotal * 15);

            var nodeChildNode = node.ChildNodes[i];

            var colsize =
                // for each column, take it's span as the colsize
                nodeChildNode.Attributes.Contains("colspan")
                    ? (int)Convert.ToInt64(nodeChildNode.Attributes["colspan"].Value)
                    : 1;

            // the time start in shifts for each column, is the previous total
            var vStart = colsizetotal;
            colsizetotal += colsize;
            var vEnd = colsizetotal; // the end is the new total (prev + colsize)


            // this is the trickery, if any column ends before the shift start or starts before
            // the shift end, then we skip
            var inScopeSearch = vEnd >= shiftStart && vStart <= shiftEnd;


            // if one of the not-skipped column represents an actual lesson, then return false,
            // the room is occupied
            var occupiedBool = !string.IsNullOrEmpty(nodeChildNode.InnerHtml.Trim());
            var roomOccupancyEnum = occupiedBool ? RoomOccupancyEnum.OCCUPIED : RoomOccupancyEnum.FREE;

            //now mark the occupancies of the room
            var htmlNodes = nodeChildNode.ChildNodes.Where(x => x.Name == "a").ToList();
            string? text = null;
            if (htmlNodes.Count > 0)
            {
                var htmlNode = htmlNodes.First();
                text = HttpUtility.HtmlDecode(htmlNode?.InnerHtml.Trim());
            }


            occupied.Add(new RoomOccupancyResultObject(iTime, roomOccupancyEnum, text));
        }

        // if no lesson takes place in the room in the time window, the room is free (duh)
        // remove duplicates
        var nodupes = new List<RoomOccupancyResultObject>();
        foreach (var roomOccupancyResultObject in occupied)
        {
            // Skip conditions (same status, empty list, different course)
            if (nodupes.Any() && nodupes.Last().RoomOccupancyEnum == roomOccupancyResultObject.RoomOccupancyEnum &&
                nodupes.Last().Text == roomOccupancyResultObject.Text)
                continue;

            nodupes.Add(roomOccupancyResultObject);
        }

        return nodupes;
    }
}