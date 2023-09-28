namespace Blade;

internal class ExitSignal : Exception {
    public ExitSignal() : base("Exit Game") { }
}

internal class TransitionScreen : Exception {
    public Screen Screen;
    public TransitionScreen(Screen game) : base("Transition Screen") {
        Screen = game;
    }
}

public static class Signals {
    public static void Exit() {
        throw new ExitSignal();
    }

    public static void Transition(Screen? game) {
        if (game != null) {
            throw new TransitionScreen(game);
        }
    }



}

