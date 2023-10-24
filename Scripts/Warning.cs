using Godot;
using System;

public partial class Warning : Sprite3D
{
	public float warningLevel;
    public AnimationTree anim;

    public override void _Ready()
    {
        base._Ready();
        anim = GetNode<AnimationTree>("AnimationTree");
    }
    public override void _Process(double delta)
    {
        base._Process(delta);
        anim.Set("parameters/Warning/blend_position", warningLevel*2-1);
    }
}
