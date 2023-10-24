using Godot;
using System;

public partial class OvenFood : FoodPlace
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
			cookLevel += cookSpeed;
		}
		else
		{
			cookLevel = 0;
			Frame = 0;
		}

		if(cookLevel > 7 && type == 0)
        {
			myFoodName = "";
			cookLevel = 0;
			Texture = emptyTex;
			oven.SetJumpscare();

		}
	}

	public override bool FoodAllowed(String food)
    {
		if(type == 0)
        {
			return (food == "Lamb" || food == "Beef");
        }
		else if(type == 1)
        {
			return (food == "Fry" || food == "Octopus");

		}
		return true;
	}
}
