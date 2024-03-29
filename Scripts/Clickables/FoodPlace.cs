using Godot;
using System;

public partial class FoodPlace : Clickable
{
	[Export] public string myFoodName;
	[Export] public float cookLevel = 0;
	[Export] public Texture2D emptyTex;

	[Signal]
	public delegate void ChangedEventHandler();

	public GameManager gm;
	float cookSpeed;
	public override void _Ready()
	{
		gm = GetNode<GameManager>("/root/GameManager");
		myFoodName = "";
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if (myFoodName != "")
		{
			Frame = Mathf.Clamp(Mathf.FloorToInt(cookLevel), 0, 6);
		}
	}
	public override void OnClicked()
	{
		base.OnClicked();

		if (myFoodName == "" && gm.heldFood.myName != "")
		{
			if(!FoodAllowed(gm.heldFood.myName))
            {
				return;
            }
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
			cookLevel = 0;
		}
		else if (myFoodName != "" && gm.heldFood.myName != "")
		{
			if (!FoodAllowed(gm.heldFood.myName))
			{
				return;
			}

			string tempFoodName = gm.heldFood.myName;
			Texture2D tempTex = gm.heldFood.myTex;
			float tempCookLevel = gm.heldFood.cookLevel;

			gm.AddNewFood(Texture, myFoodName, cookLevel);

			myFoodName = tempFoodName;
			Texture = tempTex;
			cookLevel = tempCookLevel;
		}
		EmitSignal(SignalName.Changed);

	}

	public virtual bool FoodAllowed(string food)
    {
		return true;
    }
}
