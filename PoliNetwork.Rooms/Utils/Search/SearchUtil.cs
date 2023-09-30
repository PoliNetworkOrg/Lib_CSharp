using Newtonsoft.Json.Linq;

namespace PoliNetwork.Rooms.Utils.Search;

public static class SearchUtil
{
    public static JObject? FormatRoom(object? room)
    {
        if (room == null) return null;

        var formattedRoom = JObject.FromObject(room);
        var roomLink = formattedRoom.GetValue("link");
        if (roomLink == null)
            return formattedRoom;

        var roomId = uint.Parse(roomLink.ToString().Split("idaula=")[1]);
        formattedRoom.Add(new JProperty("room_id", roomId));

        formattedRoom["occupancy_rate"] = null;

        return formattedRoom;
    }

}