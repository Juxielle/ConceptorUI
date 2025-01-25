using System.Timers;

namespace ConceptorUI.Utils;

public static class TimerClick
{
    private static Timer? _timer;
    private static int _count;

    private static void BeginTimer()
    {
        _timer = new Timer(80);
        _timer.Elapsed += OnTimedEvent!;
        _timer.AutoReset = true;
        _timer.Enabled = true;
        _count = 0;
    }

    public static bool IsEnable()
    {
        if (_count == 0)
        {
            BeginTimer();
            return false;
        }

        if (_count < 2) return false;

        _timer?.Dispose();
        return true;
    }

    private static void OnTimedEvent(object source, ElapsedEventArgs e)
    {
        _count++;
        if (_count < 3) return;
        
        _count = 0;
        _timer?.Dispose();
    }
}