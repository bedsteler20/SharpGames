

namespace Snake;

public class Menu : Blade.Menu {
    public readonly Blade.Leaderboard Leaderboard = new("Snake");
    public override ConsoleColor BackgroundColor => ConsoleColor.Green;

    public override Dictionary<string, Action> Options => new() {
        ["Play"] = () => Blade.Signals.Transition(new Game(this)),
        ["Leaderboard"] = () => Blade.Signals.Transition(new Blade.LeaderboardMenu() {
            bgColor = ConsoleColor.Green,
            Leaderboard = Leaderboard,
            Close = () => Blade.Signals.Transition(this)
        }),
        ["Exit"] = () => Blade.Signals.Transition(new MainMenu())
    };
}