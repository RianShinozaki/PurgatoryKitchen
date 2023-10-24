using Godot;
using System;

public partial class RealOven : MeshInstance3D
{
	GameManager gm;
	AnimationTree anim;

	bool open = false;

	public Sprite3D mouse;
	public RayCast3D ray;
	Vector3 mouseOrigin;
	Vector3 mouseRelativePos;

	bool jumpscaring = false;
	float jumpscaretime = 0;

	[Export] PackedScene jumpscareMan;
	[Export] Node jumpscareParent;
	[Export] Sprite3D OvenScene;

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
		if (gm.currentState == GameManager.gameState.OVEN)
		{
			
			float bounds = 1.2f;
			mouse.Position = new Vector3(Mathf.Clamp(mouseOrigin.X + mouseRelativePos.X, mouseOrigin.X - bounds, mouseOrigin.X + bounds), Mathf.Clamp(mouseOrigin.Y + mouseRelativePos.Y, mouseOrigin.Y - bounds, mouseOrigin.Y + bounds), mouseOrigin.Z);

			if (Input.IsActionJustPressed("Interact"))
			{
				open = !open;
				GetNode<AudioStreamPlayer>("AudioStreamPlayer").Play();
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
				if (ray.IsColliding())
				{
					GodotObject obj = ray.GetCollider();
					Area3D area = (Area3D)obj;
					Clickable clickable = (Clickable)area.GetParent();
					clickable.OnClicked();
				}
			}
		}

		if (gm.currentState != GameManager.gameState.OVEN && gm.currentState != GameManager.gameState.LOCKED)
		{
			open = false;
		}

		if (jumpscaring && open)
		{
			gm.currentState = GameManager.gameState.LOCKED;
			jumpscaretime += 1;
			if (jumpscaretime >= 60)
			{
				Node jumpy = jumpscareMan.Instantiate();
				jumpscareParent.AddChild(jumpy);
				gm.currentState = GameManager.gameState.DEFAULT;
				GetNode<Sprite3D>("BurnedMan").Visible = false;
				OvenScene.Visible = false;
				jumpscaring = false;
			}
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
		if (@event is InputEventMouseMotion input)
		{
			mouseRelativePos += new Vector3(input.Relative.X, -input.Relative.Y, 0) * 0.005f;
		}
	}

	public void SetJumpscare()
    {
		jumpscaring = true;
		jumpscaretime = 0;
		GetNode<Sprite3D>("BurnedMan").Visible = true;
    }
}
