using Godot;
using System;

public partial class InteractableOpenScene : Area3D
{
	GameManager gm;
	[Export] bool inContact = false;
	Node3D stoveView;

	[Export] public GameManager.gameState myState;
	[Export] public Node3D sceneView;

	PlayerController thePlayer;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		base._Ready();
		gm = GetNode<GameManager>("/root/GameManager");
		//stoveView = GetNode<Node3D>("/root/Game/Cameras/MainCamera/StoveScene");
		
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if (Input.IsActionJustPressed("OpenOrClose"))
		{
			if (gm.currentState == GameManager.gameState.DEFAULT)
			{
				if (inContact)
				{
					gm.currentState = myState;
					sceneView.Visible = true;
					thePlayer.InteractNotif.Visible = false;

				}
			}
		else if (Input.IsActionJustPressed("OpenOrClose"))
			if (gm.currentState == myState)
			{
				
				gm.currentState = GameManager.gameState.DEFAULT;
				sceneView.Visible = false;
					thePlayer.InteractNotif.Visible = true;
				}
		}
	}

	private void _on_body_entered(Node3D node)
	{
		inContact = true;
		if(node is PlayerController player)
        {
			thePlayer = player;
			player.InteractNotif.Visible = true;
        }
	}

	private void _on_body_exited(Node3D node)
	{
		inContact = false;
		if (node is PlayerController player)
		{
			thePlayer = player;
			player.InteractNotif.Visible = false;
		}
	}
}
