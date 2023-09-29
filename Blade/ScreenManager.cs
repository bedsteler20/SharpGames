namespace Blade;

public static class ScreenManager {
    private static readonly Stack<Screen> screens = new();

    public static Screen CurrentScreen => screens.Peek();

    public static void AddScreen(Screen screen) {
        screens.Push(screen);
    }

    public static void Back() {
        screens.Pop();
    }

    public static void Back<T>() {
        while (CurrentScreen.GetType() != typeof(T)) {
            Back();
        }
    }

    public static void Run() {

        Task consoleKeyTask = Task.Run(() => {
            while (true) {
                ConsoleKeyInfo key = Console.ReadKey(true);
                CurrentScreen.OnKeyPress(key);
            }
        });

        while (true) {
            Console.Clear();
            CurrentScreen.Draw();
            CurrentScreen.Update();
            Thread.Sleep(1000 / 10);
        }


    }
}