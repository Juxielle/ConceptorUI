using System;
using System.Timers;

namespace ConceptorUI.Utils;

public static class TimerClick
{
    private static Timer? _timer;
    private static int _count;

    private static void BeginTimer()
    {
        _timer = new Timer(100);
        _timer.Elapsed += OnTimedEvent!;
        _timer.AutoReset = true;
        _timer.Enabled = true;
        _count = 0;
    }

    public static bool IsEnable()
    {
        Console.WriteLine($@"Click count -->0: {_count}");
        if (_count == 0)
        {
            BeginTimer();
            return false;
        }

        Console.WriteLine($@"Click count -->1: {_count}");
        if (_count < 2) return false;

        Dispose();
        return true;
    }

    private static void OnTimedEvent(object source, ElapsedEventArgs e)
    {
        _count++;
        Console.WriteLine($@"Click count: {_count}");
        if (_count < 10) return;
        Dispose();
    }

    private static void Dispose()
    {
        _count = 0;
        _timer?.Dispose();
        _timer?.Close();
        _timer!.Elapsed -= OnTimedEvent!;
    }
}