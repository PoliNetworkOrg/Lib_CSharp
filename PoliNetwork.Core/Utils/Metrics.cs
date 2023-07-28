using System;
using System.Diagnostics;

namespace PoliNetwork.Core.Utils;

public class Metrics
{
    private const bool Stdout = true;
    private readonly Stopwatch _sw;

    public Metrics()
    {
        _sw = new Stopwatch();
    }

    private void Start()
    {
        _sw.Start();
    }

    private void Stop(string helper = "")
    {
        _sw.Stop();
        if (Stdout)
        {
            var ms = _sw.ElapsedMilliseconds;
            var helperMsg = helper == "" ? "" : $" {helper}:";
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"[Metrics]{helperMsg} {ms}ms");
            Console.ResetColor();
        }

        _sw.Reset();
    }

    public T Execute<T>(Func<T> func, string helper = "")
    {
        var fullFuncName = func.Method.DeclaringType?.FullName + "." + func.Method.Name;

        Start();

        var result = func.Invoke();

        Stop(helper == "" ? fullFuncName : helper);
        return result;
    }
}