#region

using PoliNetwork.Rooms.Enums;

#endregion

namespace PoliNetwork.Rooms.Objects;

[Serializable]
public class RoomOccupancyResultObject
{
    public readonly RoomOccupancyEnum RoomOccupancyEnum;
    internal readonly string? Text;
    public readonly TimeOnly TimeOnly;

    public RoomOccupancyResultObject(TimeOnly timeOnly, RoomOccupancyEnum roomOccupancyEnum, string? text)
    {
        TimeOnly = timeOnly;
        RoomOccupancyEnum = roomOccupancyEnum;
        Text = text;
    }
}