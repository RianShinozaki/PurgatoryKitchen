using Godot;
using System;

public partial class CustomerGen : Node3D
{
	// Called when the node enters the scene tree for the first time.
	PackedScene customer;

	float timerMax = 0;
	float timer = 5;
	GameManager gm;

	[Export] public Godot.Collections.Array<Order> easyOrders;
	[Export] public Godot.Collections.Array<Order> medOrders;
	[Export] public Godot.Collections.Array<Order> hardOrders;

	[Export] public Godot.Collections.Array<Texture2D> textures;
	[Export] public Godot.Collections.Array<String> foodNames;

	[Export] public Godot.Collections.Array<Order> totalOrderPool;

	int phase = 0;

	RandomNumberGenerator randGen = new RandomNumberGenerator();


	public override void _Ready()
	{
		customer = GD.Load<PackedScene>("res://Scenes/customer.tscn");
		gm = GetNode<GameManager>("/root/GameManager");

		for(int i = 0; i < easyOrders.Count; i++)
        {
			totalOrderPool.Add(easyOrders[i]);
        }
		
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		timer -= (float)delta;
		if(timer <= 0)
        {
			Customer newCustomer = (Customer)customer.Instantiate();
			AddChild(newCustomer);
			
			if(gm.ordersCompleted < 2)
            {
				timerMax = 50;
            }
			else if (gm.ordersCompleted >= 2 && gm.ordersCompleted < 5)
			{
				timerMax = 37;

				if(phase <= 0)
                {
					phase = 1;
					/*for (int i = 0; i < easyOrders.Count; i++)
					{
						totalOrderPool.Add(medOrders[i]);
					}*/
				}
			}
			else if (gm.ordersCompleted >= 5 && gm.ordersCompleted < 9)
			{
				timerMax = 26;
				if (phase <= 1)
				{
					phase = 2;
					/*for (int i = 0; i < easyOrders.Count; i++)
					{
						totalOrderPool.Add(hardOrders[i]);
					}*/
				}

			}
			else if (gm.ordersCompleted > 9)
			{
				timerMax = 12;
				if (phase <= 2)
				{
					phase = 3;
					/*totalOrderPool.Clear();
					for (int i = 0; i < easyOrders.Count; i++)
					{
						totalOrderPool.Add(medOrders[i]);
					}
					for (int i = 0; i < easyOrders.Count; i++)
					{
						totalOrderPool.Add(hardOrders[i]);
					}*/
				}
			}

			//timerMax = 3;
			timer = timerMax;

			//int ind = randGen.RandiRange(0, totalOrderPool.Count - 1);

			int ingredients = 0;
			if(phase == 0)
            {
				ingredients = 1;
            }
			if (phase == 1)
			{
				ingredients = randGen.RandiRange(1, 2);
			}
			if (phase == 2)
			{
				ingredients = randGen.RandiRange(1, 3);
			}
			if (phase == 3)
			{
				ingredients = randGen.RandiRange(2, 3);
			}

			newCustomer.myOrder = new Order();

			for (int i = 0; i < ingredients; i++) {
				int type = randGen.RandiRange(0, 5);
				newCustomer.myOrder.ingredients.Add(foodNames[i]);
				newCustomer.myOrder.textures.Add(textures[i]);
			}
        }
	}
}
