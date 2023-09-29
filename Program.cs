

class MainMenu : Blade.Menu {
    public override Dictionary<string, Action> Options => new() {
        ["Number Game"] = () => Blade.ScreenManager.AddScreen(new NumberGame.Menu()),
        ["Snake"] = () => Blade.ScreenManager.AddScreen(new Snake.Menu()),
        ["Exit"] = () => Environment.Exit(0)
    };
}


class Program {
    public static void Main() {
        Blade.ScreenManager.AddScreen(new MainMenu());
        Blade.ScreenManager.Run();
    }
}