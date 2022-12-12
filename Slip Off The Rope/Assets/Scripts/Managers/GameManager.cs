using System;
using System.Collections;
using System.Collections.Generic;
using Helpers;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    public event Action GameStateChanged;

    private GameStates gameState;

    public GameStates GameState=>gameState;

    private void Start()
    {
        UpdateGameState(GameStates.Start);
    }

    public void UpdateGameState(GameStates newState)
    {
        gameState=newState;
        GameStateChanged?.Invoke();
    }
}
