using Godot;
using System;

public partial class GameManager : Node
{
    public enum gameState
    {
        DEFAULT,
        OVEN,
        FRIDGE,
        ORDERCOUNTER,
        SERVECOUNTER,
        LOCKED
    }

    public gameState currentState = gameState.DEFAULT;
    public FoodInstance heldFood;

    public override void _Ready()
    {
        base._Ready();
        heldFood = GetNode<FoodInstance>("HeldFood");
    }

    public void TrashFood()
    {
        
    }

    public void AddNewFood(Texture2D tex, string name, float cookedLevel = 0)
    {
        heldFood.Define(tex, name, cookedLevel);
        GD.Print("added new food");
    }

    public override void _Process(double delta)
    {
        base._Process(delta);
        if(Input.IsActionPressed("Quit"))
        {
            GetTree().Quit();
        }
    }
}
