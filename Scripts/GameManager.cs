using Godot;
using System;

public partial class GameManager : Node
{
    public enum gameState
    {
        DEFAULT,
        STOVE,
        FRIDGE
    }

    public gameState currentState = gameState.DEFAULT;
    public FoodInstance heldFood;

    public override void _Ready()
    {
        base._Ready();
        Input.MouseMode = Input.MouseModeEnum.Captured;
        heldFood = GetNode<FoodInstance>("HeldFood");
    }
    public void TrashFood()
    {
        
    }
    public void AddNewFood(Texture2D tex, string name)
    {
        heldFood.Define(tex, name);
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
