using Godot;
using System;

public partial class OrderTicket : Clickable
{
	public Order myOrder;
	public float waitTime;
	public int OrderID;
	Plate thePlate;
	float shake = 0;
	public RandomNumberGenerator randomGen = new RandomNumberGenerator();
	PickupCounter pickupCounter;

	public void Define(Order order)
    {
		myOrder = order;
		for (int i = 0; i < 3; i++)
		{
			Sprite3D sprite = (Sprite3D)GetChild(i);
			sprite.Texture = null;
		}
		for (int i = 0; i < order.ingredients.Count; i++)
		{
			Sprite3D sprite = (Sprite3D)GetChild(i);
			sprite.Texture = (Texture2D)order.textures[i];
		}

		for (int i = 0; i < order.ingredients.Count; i++)
		{
			string theFood = myOrder.ingredients[i];
			switch (theFood)
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
			}
		}
		thePlate = GetParent().GetParent().GetNode<Plate>("Plate");
		GD.Print(OrderID);
		pickupCounter = (PickupCounter)GetParent().GetParent();
	}
    public override void _Process(double delta)
    {
        base._Process(delta);

		Position = Vector3.Right * GetIndex() * 0.5f;
		if (shake >= 0)
        {
			shake -= 0.01f;
			Position += new Vector3(randomGen.RandfRange(-shake, shake), randomGen.RandfRange(-shake, shake), 0);
		}
		
    }

    public override void OnClicked()
    {
        base.OnClicked();
		if(thePlate.OrderID == OrderID)
        {
			GD.Print("Correct order");
			pickupCounter.SuccessfulOrder();
			QueueFree();

        }
		else
        {
			GD.Print("Incorrect order");
			shake = 0.1f;
        }
    }

}
