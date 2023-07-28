namespace PoliNetwork.Core.Utils;

public static class Wait
{
    /// <summary>
    ///     Wait forever
    /// </summary>
    public static void WaitForever()
    {
        Thread.Sleep(Timeout.Infinite);
    }
}