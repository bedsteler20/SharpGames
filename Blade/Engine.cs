namespace Blade;


public class Engine {

    /// <summary>
    /// Executes the specified action and handles any thrown exceptions.
    /// If a TransitionScreen exception is thrown, the method will be called recursively with the new screen's Run method.
    /// If an ExitSignal exception is thrown, the application will exit with a status code of 0.
    /// </summary>
    /// <param name="action">The action to execute.</param>
    public static void Runtime(Action action) {
        try {
            action();
        } catch (TransitionScreen transition) {
            Runtime(transition.Screen.Run);
        }
    }

    /// <summary>
    /// Starts the game engine with the specified screen.
    /// </summary>
    /// <param name="screen">The screen to start the engine with.</param>
    public static void Start(Screen screen) {

        // Reset the console on exit
        AppDomain.CurrentDomain.ProcessExit += new EventHandler((sender, e) => {
            Console.CursorVisible = true;
            Console.ResetColor();
            Console.Clear();
        });

        Console.CursorVisible = false;

        if (OperatingSystem.IsWindows()) {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
        }

        try {
            Runtime(new Intro() {
                OnDone = () => Signals.Transition(screen)
            }.Run);
        } catch (ExitSignal) {
            Environment.Exit(0);
        }
    }


}
