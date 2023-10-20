using Godot;
using System;

public partial class GameManager : Node
{
    public enum gameState
    {
        DEFAULT,
        STOVE
    }

    public gameState currentState = gameState.DEFAULT;
}
