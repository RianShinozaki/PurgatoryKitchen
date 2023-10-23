using Godot;
using System;

public partial class WorldCam : Camera3D
{
	GameManager gm;
	public Vector3 originPos;
	public float shake;
	public RandomNumberGenerator randomGen = new RandomNumberGenerator();
	public override void _Ready()
	{
		gm = GetNode<GameManager>("/root/GameManager");
		gm.Jumpscared += OnJumpscared;
		originPos = Position;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if (shake > 0)
		{
			Position = originPos + new Vector3(randomGen.RandfRange(-shake, shake), randomGen.RandfRange(-shake, shake), 0);
			GD.Print("shakin");
			shake -= 0.005f;
		}
		else
        {
			Position = originPos;
        }
		
	}

	public void OnJumpscared()
	{
		shake = 0.5f;
	}
}
