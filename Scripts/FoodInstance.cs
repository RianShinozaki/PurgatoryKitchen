using Godot;
using System;

public partial class FoodInstance : Node
{
	[Export] public Texture2D myTex;
	[Export] public float cookLevel;
	[Export] public string myName;
    public override void _Ready()
    {
        base._Ready();
        myName = "";
    }
    public void Define(Texture2D tex, string name, float cookedLevel)
    {
		myTex = tex;
		myName = name;
		cookLevel = cookedLevel;
    }
}
