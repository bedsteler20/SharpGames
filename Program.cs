

class MainMenu : Blade.Menu {
    public override Dictionary<string, Action> Options => new() {
        ["Number Game"] = () => Blade.Signals.Transition(new NumberGame.Menu()),
        ["Snake"] = () => Blade.Signals.Transition(new Snake.Menu()),
        ["Exit"] = () => Blade.Signals.Exit()
    };
}


class Program {
    public static void Main() {
        // var menu = new Snake.Game();
        // var menu = new Blade.TextBox() {
        //     OnSubmit = (sender, text) => {
        //         if (text == "exit") {
        //             Blade.Signals.Exit();
        //         }
        //     },
        //     OnCancel = () => Blade.Signals.Exit(),
        // };
        Blade.Engine.Start(new MainMenu());
    }
}