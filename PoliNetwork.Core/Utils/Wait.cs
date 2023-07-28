namespace PoliNetwork.Core.Utils;

public static class Wait
{
    /// <summary>
    ///     Wait forever with "Console.Readline()"
    /// </summary>
    public static void WaitForeverConsoleReadline()
    {
        Thread.Sleep(Timeout.Infinite);
    }
}