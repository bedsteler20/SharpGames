using LibVLCSharp.Shared;

namespace Blade;
public class AudioPlayer {
    private readonly LibVLC lib;
    private readonly MediaPlayer player;

    public bool Repeat { get; set; } = false;

    public AudioPlayer() {
        Core.Initialize();
        lib = new();
        player = new(lib);
        player.EndReached += OnEndReached;
    }

    public void Play(string path) {
        player.Play(new Media(lib, path));
    }

    public void Stop() {
        player.Stop();
    }

    public void Pause() {
        player.Pause();
    }

    private void OnEndReached(object? sender, EventArgs e) {
        if (Repeat) {
            player.Play();
        }
    }

    public void Dispose() {
        lib.Dispose();
    }
}