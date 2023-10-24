using Godot;
using System;

public partial class Death : Control
{
    [Export]
    public PackedScene GameOverPath;
    public override void _Ready()
    {
        base._Ready();
        GameOverPath = GD.Load<PackedScene>("res://Scenes/GameOver.tscn");
        //Input.MouseMode = Input.MouseModeEnum.Visible;
        GetNode<AnimationPlayer>("AnimationPlayer").Play("GameOver");
    }

    public void Continue()
    {
        Node scene = GameOverPath.Instantiate();

        GetParent().AddChild(scene);
        QueueFree();
    }

}
