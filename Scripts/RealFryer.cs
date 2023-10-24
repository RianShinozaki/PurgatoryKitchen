using Godot;
using System;

public partial class RealFryer : MeshInstance3D
{
	GameManager gm;

	bool open = false;

	public Sprite3D mouse;
	public RayCast3D ray;
	Vector3 mouseOrigin;
	Vector3 mouseRelativePos;

	public override void _Ready()
	{
		gm = GetNode<GameManager>("/root/GameManager");
		mouse = GetNode<Sprite3D>("Mouse");
		ray = mouse.GetNode<RayCast3D>("RayCast3D");
		mouseOrigin = mouse.Position;
	}


	public override void _Process(double delta)
	{
		if (gm.currentState == GameManager.gameState.FRYER)
		{
			
			float bounds = 1.2f;
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

	}


	public override void _Input(InputEvent @event)
	{
		base._Input(@event);
		if (@event is InputEventMouseMotion input)
		{
			mouseRelativePos += new Vector3(input.Relative.X, -input.Relative.Y, 0) * 0.005f;
		}
	}
}
