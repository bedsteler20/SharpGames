namespace NumberGame;
public class Menu : Blade.Menu {
    public override string Title => "Number Game";
    public override ConsoleColor BackgroundColor => ConsoleColor.Red;
    public Blade.Leaderboard leaderboard = new("NumberGame");

    public override Dictionary<string, Action> Options => new() {
        ["Play"] = () => Blade.Signals.Transition(new NumberGame.Game(4, leaderboard)),
        ["Leaderboard"] = () => Blade.Signals.Transition(new Blade.LeaderboardMenu() {
            Leaderboard = leaderboard,
            Close = () => Blade.Signals.Transition(this)
        }),
        ["Exit"] = () => Blade.Signals.Transition(new MainMenu()),
    };
}