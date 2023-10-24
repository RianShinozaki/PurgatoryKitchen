using Godot;
using System;

public partial class MainMenu : Control {

    public PackedScene GamePath;

    public override void _Ready() {
        base._Ready();

        Input.MouseMode = Input.MouseModeEnum.Visible;
        GamePath = GD.Load<PackedScene>("res://Game.tscn");
    }

    public void OnPlayClicked() {
        Node scene = GamePath.Instantiate();

        GetParent().AddChild(scene);
        QueueFree();
        Input.MouseMode = Input.MouseModeEnum.Captured;
    }

    public void OnQuitClicked() {
        GetTree().Quit();
    }
}
