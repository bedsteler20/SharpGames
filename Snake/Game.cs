namespace Snake;

enum Direction {
    Up,
    Down,
    Left,
    Right
}


public class Game : Blade.Screen {
    const int CELLS_X = 15;
    const int CELLS_Y = 15;
    const int HEIGHT = CELLS_Y + 3;
    const int WIDTH = (CELLS_X * 2) + 2;

    public override (int x, int y) Offset => GetCenter(WIDTH, HEIGHT);

    private readonly Random Rng = Blade.Utils.CreateRadom();

    public override Blade.UpdateStrategy updateStrategy => Blade.UpdateStrategy.FixedRate;

    private Direction Direction = Direction.Right;
    private int frame = 0;
    private int score = 0;

    private ref (int x, int y) SnakeHead => ref snake[0];
    private ref (int x, int y) SnakeTail => ref snake[^1];

    private (int x, int y) food;
    private readonly Blade.Leaderboard leaderboard;

    private (int x, int y)[] snake = new[] { (9, 9), (9, 8), (9, 7), (9, 6) };
    public Game(Blade.Leaderboard leaderboard) {
        this.leaderboard = leaderboard;
        SpawnFood();
    }

    private void SpawnFood() {
        int x = Rng.Next(0, CELLS_X);
        int y = Rng.Next(0, CELLS_Y);
        if (snake.Contains((x, y))) {
            SpawnFood();
        } else {
            food = (x, y);
        }
    }

    private void GameOver() {
        var gameOverScreen = new Blade.GameOver() {
            OnGameOver = () => Blade.ScreenManager.AddScreen(new Blade.TextBox() {
                BackgroundColor = ConsoleColor.Green,
                OnSubmit = (sender, text) => {
                    leaderboard.AddScore(text, score);
                    leaderboard.Save();
                    Blade.ScreenManager.Back<Menu>();
                },
                OnCancel = () => {
                    Blade.ScreenManager.Back<Menu>();
                }
            })
        };
        Blade.ScreenManager.AddScreen(gameOverScreen);
    }



    public override void OnKeyPress(ConsoleKeyInfo key) {
        base.OnKeyPress(key);
        switch (key.Key) {
            case ConsoleKey.UpArrow:
                Direction = Direction.Up;
                break;
            case ConsoleKey.DownArrow:
                Direction = Direction.Down;
                break;
            case ConsoleKey.LeftArrow:
                Direction = Direction.Left;
                break;
            case ConsoleKey.RightArrow:
                Direction = Direction.Right;
                break;
        }
    }

    public override void Update() {
        base.Update();
        frame++;
        if (frame % 4 == 0) {
            Move();
        }
    }

    private void Move() {

        (int x, int y) next = Direction switch {
            Direction.Up => (SnakeHead.x, SnakeHead.y - 1),
            Direction.Down => (SnakeHead.x, SnakeHead.y + 1),
            Direction.Left => (SnakeHead.x - 1, SnakeHead.y),
            Direction.Right => (SnakeHead.x + 1, SnakeHead.y),
            _ => (SnakeHead.x, SnakeHead.y),
        };
        if (next.x < 0 || next.x >= CELLS_X || next.y < 0 || next.y >= CELLS_Y) {
            GameOver();
            return;
        }
        if (snake.Contains(next)) {
            GameOver();
            return;
        }
        var newSnake = snake.Prepend(next).ToArray();

        if (next == food) {
            snake = newSnake;
            score++;
            SpawnFood();
        } else {
            snake = newSnake[..snake.Length];

        }
    }

    public override void Draw() {
        base.Draw();
        Paint(0, 0, $"Score: {score}", ConsoleColor.Green);
        Paint(food.x * 3 + 1, food.y + 2, " O ", ConsoleColor.Red);

        Paint(0, 1, "┏");
        for (int x = 0; x < CELLS_X; x++) {
            Paint(x * 3 + 1, 1, "━━━");
        }
        Paint(CELLS_X * 3 + 1, 1, "┓");
        for (int y = 0; y < CELLS_Y; y++) {
            Paint(0, y + 2, "┃");
        }
        for (int y = 0; y < CELLS_Y; y++) {
            Paint(CELLS_X * 3 + 1, y + 2, "┃");
        }
        Paint(0, CELLS_Y + 2, "┗");
        for (int x = 0; x < CELLS_X; x++) {
            Paint(x * 3 + 1, CELLS_Y + 2, "━━━");
        }
        Paint(CELLS_X * 3 + 1, CELLS_Y + 2, "┛");

        foreach (var (x, y) in snake) {
            Paint(x * 3 + 1, y + 2, " ■ ", ConsoleColor.Green);
        }
    }
}