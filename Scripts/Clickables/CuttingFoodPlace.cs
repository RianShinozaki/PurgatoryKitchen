using Godot;
using System;

public partial class CuttingFoodPlace : FoodPlace
{
	[Export] float fullCookTime = 30; //In Seconds
	float cookSpeed;
	[Export] int type;
	RealOven oven;
	public override void _Ready()
	{
		gm = GetNode<GameManager>("/root/GameManager");
		myFoodName = "";
		cookSpeed = 1 / (60 * fullCookTime / 5);
		oven = (RealOven)GetParent();
	}

	public override void _Process(double delta)
	{
		if (myFoodName != "")
		{
			Frame = Mathf.Clamp(Mathf.FloorToInt(cookLevel), 0, 6);
		}
	}

	public override bool FoodAllowed(String food)
    {
		return (food == "Lettuce" || food == "Tomato");

	}
}
