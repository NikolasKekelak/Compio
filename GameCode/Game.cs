using System.Diagnostics;

namespace ConsoleApp1.GameCode;

public class Game
{
    private bool _isRunning;
    private Thread? _gameThread;
    private readonly Stopwatch _stopwatch = new();

    public void Start()
    {
        if (_isRunning) return;

        _isRunning = true;
        _gameThread = new Thread(RunGameLoop);
        _gameThread.Start();
    }

    public void Stop()
    {
        _isRunning = false;
        _gameThread?.Join();
    }

    private void RunGameLoop()
    {
        _stopwatch.Start();
        float lastTime = 0;

        while (_isRunning)
        {
            float currentTime = (float)_stopwatch.Elapsed.TotalSeconds;
            float deltaTime = currentTime - lastTime;
            lastTime = currentTime;

            // Handle game processes here
            UpdateGameProcesses(deltaTime);

            // Simple throttle to avoid 100% CPU usage if no work is needed
            // This can be replaced with more sophisticated timing later.
            Thread.Sleep(1); 
        }
        _stopwatch.Stop();
    }

    private void UpdateGameProcesses(float deltaTime)
    {
        // For now, this is where other game logic (non-player) will live.
        // For example: updating other entities, AI, world state, etc.
    }
}
