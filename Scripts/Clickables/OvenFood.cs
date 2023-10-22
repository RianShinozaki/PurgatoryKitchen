using Godot;
using System;

public partial class OvenFood : Clickable
{
	[Export] string myFoodName;
	[Export] float cookLevel = 0;
	[Export] Texture2D emptyTex;

	GameManager gm;
	[Export] float fullCookTime = 30; //In Seconds
	float cookSpeed;
	public override void _Ready()
	{
		gm = GetNode<GameManager>("/root/GameManager");
		myFoodName = "";
		cookSpeed = 1 / (60 * fullCookTime / 5);

	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if(myFoodName != "")
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

	public override void OnClicked()
	{
		base.OnClicked();

		if(myFoodName == "" && gm.heldFood.myName != "")
        {
			myFoodName = gm.heldFood.myName;
			Texture = gm.heldFood.myTex;
			cookLevel = gm.heldFood.cookLevel;

			gm.AddNewFood(null, "", 0);
		}
		else if (myFoodName != "" && gm.heldFood.myName == "")
		{
			gm.AddNewFood(Texture, myFoodName, cookLevel);
			myFoodName = "";
			Texture = emptyTex;
		}
		else if (myFoodName != "" && gm.heldFood.myName != "")
		{
			string tempFoodName = gm.heldFood.myName;
			Texture2D tempTex = gm.heldFood.myTex;
			float tempCookLevel = gm.heldFood.cookLevel;

			gm.AddNewFood(Texture, myFoodName, cookLevel);

			myFoodName = tempFoodName;
			Texture = tempTex;
			cookLevel = tempCookLevel;
		}
		

	}
}
