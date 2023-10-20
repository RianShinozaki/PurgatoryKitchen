using Godot;
using System;

public partial class CamWobble : Camera3D
{
	// Called when the node enters the scene tree for the first time.
	float initRot;
	float t = 0;
	[Export] float cycleSpd = 0;
	[Export] float amp = 0;
	public override void _Ready()
	{
		initRot = Rotation.X;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		Rotation = new Vector3(initRot + Mathf.Cos(t) * amp, 0, 0);
		t += cycleSpd * (float)delta;
	}
}
