using Godot;
using System;

public partial class StoveInteractable : Area3D
{
	GameManager gm;
	bool inContact = false;
	Node3D stoveView;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		base._Ready();
		gm = GetNode<GameManager>("/root/GameManager");
		stoveView = GetNode<Node3D>("/root/Game/Cameras/MainCamera/StoveScene");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if (Input.IsActionJustPressed("Interact")) {
			if (gm.currentState == GameManager.gameState.DEFAULT)
			{
				if (inContact)
				{
					gm.currentState = GameManager.gameState.STOVE;
					stoveView.Visible = true;
				}
			} else
			{
				gm.currentState = GameManager.gameState.DEFAULT;
				stoveView.Visible = false;
			}
		}
	}

	private void _on_body_entered(Node3D node)
    {
		inContact = true;
    }

	private void _on_body_exited(Node3D node)
    {
		inContact = false;
    }
}
