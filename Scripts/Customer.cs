using Godot;
using System;

public partial class Customer : Clickable
{
	GameManager gm;
	public int QueueNum;
	public float WaitingTime;
	public float totalWaitTime = 60*2.5f;

	[Export] public Order myOrder;

	public bool GoingAway;
	 public Node3D OrderParent;
	[Export] PackedScene orderObject;
	[Export] public Godot.Collections.Array<Texture2D> facePossibilities;
	RandomNumberGenerator randGen = new RandomNumberGenerator();

	bool givingOrder;
	float t;

	Texture2D myRealTex;

	public override void _Ready()
	{
		gm = GetNode<GameManager>("/root/GameManager");
		OrderParent = GetNode<Node3D>("/root/Game/Areas/PickupCounter/OrderTickets");

		int rand = randGen.RandiRange(0, 2);
		myRealTex = facePossibilities[rand];

	}
    public override void _Process(double delta)
    {
		WaitingTime += (float)delta;

		base._Process(delta);
		if(GoingAway)
        {
			Transparency += 0.02f;
			if(Transparency >= 1)
            {
				Node anOrder = orderObject.Instantiate();
				OrderTicket tick = (OrderTicket)anOrder;
				OrderParent.AddChild(anOrder);

				tick.Define(myOrder, myRealTex);
				tick.waitTime = WaitingTime;
				gm.currentState = GameManager.gameState.ORDERCOUNTER;

				QueueFree();
            }
        }

		Position = new Vector3(GetIndex()*0.5f, 0, -GetIndex() * 0.2f);
		Modulate = new Color(1, 1, 1, 1) - new Color(1, 1, 1, 1) * GetIndex()*0.2f;
		GetNode<Area3D>("Area3D").CollisionLayer = (uint)(GetIndex() == 0 ? 16 : 0);

		if(givingOrder)
        {
			t += (float)delta * 5;
			Frame = Mathf.FloorToInt(t) % 2;
        }

		if(WaitingTime > totalWaitTime && !givingOrder && !GoingAway)
        {
			Game game = GetNode<Game>("/root/Game");
			game.OnGameOver(myRealTex, 1);
		}

		GetNode<Warning>("WarningSign").warningLevel = WaitingTime / totalWaitTime;
	}

    public override void OnClicked()
	{
		base.OnClicked();
		if(GetIndex() == 0)
        {
			OrderCounter orderC = (OrderCounter)GetParent().GetParent();
			orderC.ShowOrder(myOrder, this);
			Texture = myRealTex;
			givingOrder = true;
			GetNode<AudioStreamPlayer>("AudioStreamPlayer").Play();
			WaitingTime = 0;

		}
	}

	public void DoneGivingOrder()
    {
		GoingAway = true;
		givingOrder = false;
		Frame = 0;
    }
}