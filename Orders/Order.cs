using Godot;
using System;

[GlobalClass]
public partial class Order : Resource
{
    [Export] public Godot.Collections.Array<String> ingredients;
    [Export] public Godot.Collections.Array<Texture2D> textures;
}