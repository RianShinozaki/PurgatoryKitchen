using Godot;
using System;

public partial class OvenFood : FoodPlace
{
	[Export] float fullCookTime = 30; //In Seconds
	float cookSpeed;

	public override void _Ready()
	{
		gm = GetNode<GameManager>("/root/GameManager");
		myFoodName = "";
		cookSpeed = 1 / (60 * fullCookTime / 5);

	}

	public override void _Process(double delta)
	{
		if (myFoodName != "")
		{
			Frame = Mathf.Clamp(Mathf.FloorToInt(cookLevel), 0, 6);
			cookLevel += cookSpeed;
		}
		else
		{
			cookLevel = 0;
			Frame = 0;
		}
	}
}
