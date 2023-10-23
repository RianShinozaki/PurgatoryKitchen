using Godot;
using System;

public partial class FinalRenderTexture : Sprite3D
{
	GameManager gm;
	AnimationPlayer anim;
	public override void _Ready()
	{
		gm = GetNode<GameManager>("/root/GameManager");
		gm.Jumpscared += OnJumpscared;
		anim = GetNode<AnimationPlayer>("AnimationPlayer");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void OnJumpscared()
    {
		anim.Play("ScareRed");
    }
}
