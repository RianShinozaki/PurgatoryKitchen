using Godot;
using System;

public partial class Game : Node3D
{
    [Export]
    public PackedScene GameOverPath;
    GameManager gm;

    public override void _Ready()
    {
        GameOverPath = GD.Load<PackedScene>("res://Scenes/Death.tscn");
        gm = GetNode<GameManager>("/root/GameManager");
        gm.currentState = GameManager.gameState.DEFAULT;
        base._Ready();
    }

    public void OnGameOver(Texture2D tex, int type = 0)
    {
        Node scene = GameOverPath.Instantiate();

        GetParent().AddChild(scene);
        scene.GetNode<Sprite2D>("Sprite2D").Texture = tex;

        if (type == 1)
        {
            scene.GetNode<Label>("TopText").Text = "I'VE WAITED TOO LONG...";
        }
        QueueFree();
    }


}
