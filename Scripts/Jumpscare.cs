using Godot;
using System;

public partial class Jumpscare : Sprite3D
{
	GameManager gm;
	AnimationPlayer anim;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		gm = GetNode<GameManager>("/root/GameManager");
		gm.InvokeScare();
		anim = GetNode<AnimationPlayer>("AnimationPlayer");
		anim.Play("Scare");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
