using Godot;
using System;

public partial class FoodGenClickable : Clickable
{
	[Export] string myFoodName;
	[Export] Texture2D myFoodTexture;
	GameManager gm;
	public override void _Ready()
	{
		gm = GetNode<GameManager>("/root/GameManager");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{

	}

    public override void OnClicked()
    {
        base.OnClicked();
		gm.TrashFood();
		gm.AddNewFood(myFoodTexture, myFoodName);
    }
}
