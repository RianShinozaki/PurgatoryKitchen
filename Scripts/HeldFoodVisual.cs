using Godot;
using System;

public partial class HeldFoodVisual : Sprite3D
{
	GameManager gm;
	public override void _Ready()
	{
		gm = GetNode<GameManager>("/root/GameManager");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		GD.Print(gm.heldFood.cookLevel);
		//if(gm.heldFood.myTex != null)
		Texture = gm.heldFood.myTex;
		Frame = Mathf.Clamp(Mathf.FloorToInt(gm.heldFood.cookLevel), 0, 6);
	}
}
