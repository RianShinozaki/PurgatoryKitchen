using Godot;
using System;

public partial class OrderTicket : Sprite3D
{
	public Order myOrder;
	public float waitTime;

	public void Define(Order order)
    {
		myOrder = order;
		for (int i = 0; i < 3; i++)
		{
			Sprite3D sprite = (Sprite3D)GetChild(i);
			sprite.Texture = null;
		}
		for (int i = 0; i < order.ingredients.Count; i++)
		{
			Sprite3D sprite = (Sprite3D)GetChild(i);
			sprite.Texture = (Texture2D)order.textures[i];
		}
	}
    public override void _Process(double delta)
    {
        base._Process(delta);
		Position = Vector3.Right * GetIndex() * 0.5f;
    }
}
