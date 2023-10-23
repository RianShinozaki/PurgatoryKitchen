using Godot;
using System;

public partial class PickupCounter : Node
{
	GameManager gm;


	public Sprite3D mouse;
	public RayCast3D ray;
	Vector3 mouseOrigin;
	Vector3 mouseRelativePos;

	bool gettingOrder;
	float orderGettingTime;

	Sprite3D customer;
	Plate plate;

	public override void _Ready()
	{
		gm = GetNode<GameManager>("/root/GameManager");
		mouse = GetNode<Sprite3D>("Mouse");
		ray = mouse.GetNode<RayCast3D>("RayCast3D");
		mouseOrigin = mouse.Position;
		customer = GetNode("FrontCounter").GetNode<Sprite3D>("Customer");
		plate = GetNode<Plate>("Plate");
	}


	public override void _Process(double delta)
	{
		if (gm.currentState == GameManager.gameState.SERVECOUNTER)
		{

			float bounds = 0.6f;
			mouse.Position = new Vector3(Mathf.Clamp(mouseOrigin.X + mouseRelativePos.X, mouseOrigin.X - bounds, mouseOrigin.X + bounds), Mathf.Clamp(mouseOrigin.Y + mouseRelativePos.Y, mouseOrigin.Y - bounds, mouseOrigin.Y + bounds), mouseOrigin.Z);


			if (ray.IsColliding())
			{
				mouse.Frame = 1;
			}
			else
			{
				mouse.Frame = 0;
			}
			if (Input.IsActionJustPressed("LeftClick"))
			{
				if (ray.IsColliding())
				{
					GodotObject obj = ray.GetCollider();
					Area3D area = (Area3D)obj;
					Clickable clickable = (Clickable)area.GetParent();
					clickable.OnClicked();
				}
			}
		}
		if(gettingOrder)
        {
			orderGettingTime -= (float)delta;
			if(orderGettingTime <= 0)
            {
				gm.currentState = GameManager.gameState.SERVECOUNTER;
				customer.Visible = false;
				plate.SweepOrder();
				gettingOrder = false;
			}
        }

	}
	public override void _Input(InputEvent @event)
	{
		base._Input(@event);
		if (@event is InputEventMouseMotion input)
		{
			mouseRelativePos += new Vector3(input.Relative.X, -input.Relative.Y, 0) * 0.002f;
		}
	}

	public void SuccessfulOrder()
    {
		gm.currentState = GameManager.gameState.LOCKED;
		customer.Visible = true;
		orderGettingTime = 3;
		gettingOrder = true;
	}
}
