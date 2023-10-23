using Godot;
using System;

public partial class Customer : Clickable
{
	GameManager gm;
	public int QueueNum;
	public float WaitingTime;

	[Export] public Order myOrder;

	public bool GoingAway;

	public override void _Ready()
	{
		gm = GetNode<GameManager>("/root/GameManager");

	}
    public override void _Process(double delta)
    {
        base._Process(delta);
		if(GoingAway)
        {
			GetNode<Sprite3D>("").Transparency -= 0.02f;
        }
    }

    public override void OnClicked()
	{
		base.OnClicked();
		if(QueueNum == 0)
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