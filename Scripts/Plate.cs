using Godot;
using System;

public partial class Plate : Sprite3D
{
	public int OrderID = 0;
	public float OrderQuality;
	public override void _Ready()
	{
		FoodPlace fp = (FoodPlace)GetChild(0);
		fp.Changed += OnPlateChanged;

		fp = (FoodPlace)GetChild(1);
		fp.Changed += OnPlateChanged;

		 fp = (FoodPlace)GetChild(2);
		fp.Changed += OnPlateChanged;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{

	}

	private void OnPlateChanged()
    {
		OrderID = 0;
		OrderQuality = 0;
		int things = 3;
		for(int i = 0; i < 3; i++)
        {
			
			FoodPlace fp = (FoodPlace)GetChild(i);
			string theFood = fp.myFoodName;
			switch(theFood)
            {
				case "Lamb":
					OrderID += 1;
					break;
				case "Beef":
					OrderID += 10;
					break;
				case "Tomato":
					OrderID += 100;
					break;
				case "Lettuce":
					OrderID += 1000;
					break;
				case "Fry":
					OrderID += 10000;
					break;
				case "Octopus":
					OrderID += 100000;
					break;
				default:
					things--;
					break;
			}

			OrderQuality += Mathf.Clamp((4 - Mathf.Abs(4 - fp.cookLevel)) / 4, 0, 1);

        }

		OrderQuality /= things;

		GD.Print(OrderQuality);
	}
	public void SweepOrder()
    {
		for (int i = 0; i < 3; i++)
		{
			FoodPlace fp = (FoodPlace)GetChild(i);
			fp.myFoodName = "";
			fp.Texture = fp.emptyTex;
			fp.cookLevel = 0;
			
		}
		OrderID = 0;
	}
}
