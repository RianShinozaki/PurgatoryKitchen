using Godot;
using System;

public partial class Customer : Clickable
{
	GameManager gm;
	public int QueueNum;
	public float WaitingTime;

	[Export] public Order myOrder;

	public bool GoingAway;
	[Export] public Node3D OrderParent;
	[Export] PackedScene orderObject;

	public override void _Ready()
	{
		gm = GetNode<GameManager>("/root/GameManager");
		OrderParent = GetNode<Node3D>("/root/Game/Areas/PickupCounter/OrderTickets");

	}
    public override void _Process(double delta)
    {
        base._Process(delta);
		if(GoingAway)
        {
			Transparency += 0.02f;
			if(Transparency >= 1)
            {
				Node anOrder = orderObject.Instantiate();
				OrderTicket tick = (OrderTicket)anOrder;
				tick.Define(myOrder);
				tick.waitTime = WaitingTime;

				OrderParent.AddChild(anOrder);
				QueueFree();
            }
        }

		Position = new Vector3(GetIndex()*0.5f, 0, -GetIndex() * 0.2f);
		Modulate = new Color(1, 1, 1, 1) - new Color(1, 1, 1, 1) * GetIndex()*0.2f;
		GetNode<Area3D>("Area3D").CollisionLayer = (uint)(GetIndex() == 0 ? 16 : 0);
    }

    public override void OnClicked()
	{
		base.OnClicked();
		if(GetIndex() == 0)
        {
			OrderCounter orderC = (OrderCounter)GetParent().GetParent();
			orderC.ShowOrder(myOrder, this);
        }
	}

	public void DoneGivingOrder()
    {
		GoingAway = true;
    }
}