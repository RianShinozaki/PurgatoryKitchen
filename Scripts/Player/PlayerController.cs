using Godot;
using System;

public partial class PlayerController : CharacterBody3D
{
	public const float Speed = 5.0f;
	public const float JumpVelocity = 4.5f;

	AnimationTree anim;

	GameManager gm;

	// Get the gravity from the project settings to be synced with RigidBody nodes.
	public float gravity = ProjectSettings.GetSetting("physics/3d/default_gravity").AsSingle();

    public override void _Ready()
    {
        base._Ready();
		anim = GetNode<AnimationTree>("AnimationTree");
		gm = GetNode<GameManager>("/root/GameManager");
	}
    public override void _PhysicsProcess(double delta)
	{
		if (gm.currentState == GameManager.gameState.DEFAULT)
		{
			Vector3 velocity = Velocity;

			// Add the gravity.
			if (!IsOnFloor())
				velocity.Y -= gravity * (float)delta;

			// Handle Jump.
			//if (Input.IsActionJustPressed("ui_accept") && IsOnFloor())
				//velocity.Y = JumpVelocity;

			// Get the input direction and handle the movement/deceleration.
			// As good practice, you should replace UI actions with custom gameplay actions.
			Vector2 inputDir = Input.GetVector("left", "right", "up", "down");
			Vector3 direction = (Transform.Basis * new Vector3(inputDir.X, 0, inputDir.Y)).Normalized();
			if (direction != Vector3.Zero)
			{
				velocity.X = direction.X * Speed;
				velocity.Z = direction.Z * Speed;
			}
			else
			{
				velocity.X = Mathf.MoveToward(Velocity.X, 0, Speed);
				velocity.Z = Mathf.MoveToward(Velocity.Z, 0, Speed);
			}

			Velocity = velocity;
			MoveAndSlide();
		}
		else
        {
			Velocity = new Vector3(0, 0, 0);
        }
		UpdateAnims();
	}

	public void UpdateAnims()
    {
		if (Velocity.IsZeroApprox())
		{
			anim.Set("parameters/conditions/running", false);
			anim.Set("parameters/conditions/idle", true);
		}
		if (!Velocity.IsZeroApprox())
		{
			anim.Set("parameters/conditions/running", true);
			anim.Set("parameters/conditions/idle", false);
			anim.Set("parameters/Idle/blend_position", new Vector2(Velocity.X, -Velocity.Z));
			anim.Set("parameters/Run/blend_position", new Vector2(Velocity.X, -Velocity.Z));

		}
	}
}
