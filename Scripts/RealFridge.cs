using Godot;
using System;

public partial class RealFridge : MeshInstance3D
{
	GameManager gm;
	AnimationTree anim;

	bool open = false;

	public Sprite3D mouse;
	public RayCast3D ray;
	Vector3 mouseOrigin;
	Vector3 mouseRelativePos;

	public override void _Ready()
	{
		anim = GetNode<AnimationTree>("AnimationTree");
		gm = GetNode<GameManager>("/root/GameManager");
		mouse = GetNode<Sprite3D>("Mouse");
		ray = mouse.GetNode<RayCast3D>("RayCast3D");
		mouseOrigin = mouse.Position;
	}


	public override void _Process(double delta)
	{
		if (gm.currentState == GameManager.gameState.FRIDGE)
		{
			/*Vector3 velocity = Vector3.Zero;
			Vector2 inputDir = Input.GetVector("left", "right", "down", "up");
			Vector3 direction = (Transform.Basis * new Vector3(inputDir.X, 0, inputDir.Y)).Normalized();
			float Speed = 0.2f;
			if (direction != Vector3.Zero)
			{
				velocity.X = direction.X * Speed;
				velocity.Y = direction.Z * Speed;
			}
			mouse.Position += velocity;
			float bounds = 1.2f;
			mouse.Position = new Vector3(Mathf.Clamp(mouse.Position.X, mouseOrigin.X - bounds, mouseOrigin.X + bounds), Mathf.Clamp(mouse.Position.Y, mouseOrigin.Y - bounds, mouseOrigin.Y + bounds), mouseOrigin.Z);
			*/
			float bounds = 1.2f;
			mouse.Position = new Vector3(Mathf.Clamp(mouseOrigin.X + mouseRelativePos.X, mouseOrigin.X - bounds, mouseOrigin.X + bounds), Mathf.Clamp(mouseOrigin.Y + mouseRelativePos.Y, mouseOrigin.Y - bounds, mouseOrigin.Y + bounds), mouseOrigin.Z);

			if (Input.IsActionJustPressed("Interact"))
			{
				open = !open;
			}

			if (ray.IsColliding() && open)
			{
				mouse.Frame = 1;
			}
			else
            {
				mouse.Frame = 0;
            }
			if (Input.IsActionJustPressed("LeftClick") && open)
			{
				if(ray.IsColliding())
                {
					GodotObject obj = ray.GetCollider();
					Area3D area = (Area3D)obj;
					Clickable clickable = (Clickable)area.GetParent();
					clickable.OnClicked();
				}
			}
		}

		if (gm.currentState != GameManager.gameState.FRIDGE)
        {
			open = false;
        }

		UpdateAnims();
	}

	void UpdateAnims()
    {
		anim.Set("parameters/conditions/open", open);
		anim.Set("parameters/conditions/close", !open);
    }

    public override void _Input(InputEvent @event)
    {
        base._Input(@event);
		if(@event is InputEventMouseMotion input)
        {
			mouseRelativePos += new Vector3(input.Relative.X, -input.Relative.Y, 0) * 0.005f;
        }
    }
}
