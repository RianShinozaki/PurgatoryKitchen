using Godot;
using System;

public partial class FoodInstance : Node
{
	[Export] public Texture2D myTex;
	[Export] public float cookLevel;
	[Export] public string myName;

	public void Define(Texture2D tex, string name)
    {
		myTex = tex;
		myName = name;
    }
}
