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

	RandomNumberGenerator randomGen = new RandomNumberGenerator();

	bool jumpscaring = false;
	float jumpscaretime = 0;

	[Export] PackedScene jumpscareLady;
	[Export] Node jumpscareParent;
	[Export] Sprite3D FridgeScene;

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

			float bounds = 1.2f;
			mouse.Position = new Vector3(Mathf.Clamp(mouseOrigin.X + mouseRelativePos.X, mouseOrigin.X - bounds, mouseOrigin.X + bounds), Mathf.Clamp(mouseOrigin.Y + mouseRelativePos.Y, mouseOrigin.Y - bounds, mouseOrigin.Y + bounds), mouseOrigin.Z);

			if (Input.IsActionJustPressed("Interact"))
			{
				if(!open)
                {
					float rand = randomGen.Randf();
					if(rand <= 0.1f) //Spooky time
                    {
						GetNode<Node3D>("Jumpscare").Visible = true;
						GetNode<Node3D>("FridgeContents").Visible = false;
						jumpscaring = true;
						jumpscaretime = 0;
						

                    } else
                    {
						GetNode<Node3D>("Jumpscare").Visible = false;
						GetNode<Node3D>("FridgeContents").Visible = true;
						jumpscaring = false;
					}
                } else
                {
					jumpscaring = false;

				}
				open = !open;
				GetNode<AudioStreamPlayer>("AudioStreamPlayer").Play();
			}

			if (ray.IsColliding() && open && !jumpscaring)
			{
				mouse.Frame = 1;
			}
			else
            {
				mouse.Frame = 0;
            }
			if (Input.IsActionJustPressed("LeftClick") && open && !jumpscaring)
			{
				if(ray.IsColliding())
                {
					GodotObject obj = ray.GetCollider();
					Area3D area = (Area3D)obj;
					Clickable clickable = (Clickable)area.GetParent();
					clickable.OnClicked();
				}
			}
			if(jumpscaring)
            {
				jumpscaretime += 1;
				if(jumpscaretime >= 120)
                {
					Node jumpy = jumpscareLady.Instantiate();
					jumpscareParent.AddChild(jumpy);
					gm.currentState = GameManager.gameState.DEFAULT;
					FridgeScene.Visible = false;
					jumpscaring = false;
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
