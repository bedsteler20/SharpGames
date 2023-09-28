namespace Blade;

public delegate void TextBoxSubmitHandler(TextBox sender, string text);

public class TextBox : Screen {
    public override UpdateStrategy updateStrategy => UpdateStrategy.Lazy;
    public override bool ShowCursor => true;
    public override (int x, int y) Offset => GetCenter(Width, Height);

    public ConsoleColor BackgroundColor { get; set; } = ConsoleColor.Yellow;
    public ConsoleColor ForegroundColor { get; set; } = ConsoleColor.Black;
    public ConsoleColor TextColor { get; set; } = ConsoleColor.White;

    public required TextBoxSubmitHandler OnSubmit { get; set; }
    public required Action OnCancel { get; set; }

    const int PADDING = 4;
    const int NAME_WIDTH = 25;
    public string Text { get; set; } = "";
    public int Width => NAME_WIDTH + PADDING * 2;
    public int Height => 1 + PADDING * 2;



    public override void Draw() {
        base.Draw();
        Paint(0, 0, '▗' + new string('▄', Width - 2) + '▖', BackgroundColor);
        Paint(0, 1, "▐", BackgroundColor);
        Paint(1, 1, Utils.CenterText("Hello", Width - 2), ConsoleColor.Black, BackgroundColor);
        Paint(Width - 1, 1, "▌", BackgroundColor);

        Paint(0, 2, "▐", BackgroundColor);
        Paint(1, 2, "██▛" + new string('▀', Width - PADDING * 2) + "▜██", BackgroundColor, ForegroundColor);
        Paint(Width - 1, 2, "▌", BackgroundColor);

        Paint(0, 3, "▐", BackgroundColor);
        Paint(1, 3, "██▌" + new string(' ', Width - PADDING * 2) + "▐██", BackgroundColor, ForegroundColor);
        Paint(Width - 1, 3, "▌", BackgroundColor);


    }
}