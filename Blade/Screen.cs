namespace Blade;

/// <summary>
/// Represents the update strategy for the game screen.
/// </summary>
public enum UpdateStrategy {
    /// <summary>
    /// Update the game at a fixed rate.
    /// </summary>
    FixedRate,
    /// <summary>
    /// Update the game after user input.
    /// </summary>
    Lazy,
}

/// <summary>
/// Represents a screen that can be drawn on the console and updated at a fixed rate or lazily.
/// </summary>
public abstract class Screen : Drawable {
    abstract public UpdateStrategy updateStrategy { get; }
    public virtual bool ShowCursor { get; } = false;

    public void Run() {
        switch (updateStrategy) {
            case UpdateStrategy.FixedRate:
                RunFixedRate();
                break;
            case UpdateStrategy.Lazy:
                RunLazy();
                break;
        }
    }


    public virtual void Update() {
        if (ShowCursor) {
            Console.CursorVisible = true;
        } else {
            Console.CursorVisible = false;
        }
    }

    /// <summary>
    /// Called when a key is pressed while the screen is active.
    /// </summary>
    /// <param name="key">The key that was pressed.</param>
    public virtual void OnKeyPress(ConsoleKeyInfo key) { }

    /// <summary>
    /// Runs on every frame after the Update() method
    /// </summary>
    public override void Draw() {
        Console.Clear();
        base.Draw();
    }


    private void RunFixedRate() {
        try {
            Task consoleKeyTask = Task.Run(() => {
                while (true) {
                    ConsoleKeyInfo key = Console.ReadKey(true);
                    OnKeyPress(key);
                }
            });

            while (true) {
                Draw();
                Update();
                Thread.Sleep(1000 / 10);
            }
        } catch (ExitSignal) {
            Console.Clear();
        }
    }

    private void RunLazy() {
        try {
            while (true) {
                Draw();
                Update();
                ConsoleKeyInfo key = Console.ReadKey(true);
                OnKeyPress(key);

            }
        } catch (ExitSignal) {
            Console.Clear();
        }
    }
}