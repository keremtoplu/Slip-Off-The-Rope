using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinManager : MonoBehaviour
{
    [SerializeField]
    private int currentCoin;

    [SerializeField]
    private int strokeCoin;

    [SerializeField]
    private int levelUpCoin;


    public event Action coinAdded;

    public int CurrentCoin=>currentCoin;


    private void Start()
    {
    }
    public void addStrokeCoin()
    {
        currentCoin+=strokeCoin;
        PlayerPrefs.SetInt("Coin",currentCoin);
        coinAdded?.Invoke();
    }

    public void addLevelUpCoin()
    {
        currentCoin+=levelUpCoin;        
        PlayerPrefs.SetInt("Coin",currentCoin);
        coinAdded?.Invoke();
    }
    public void DecreaseCoin(int value)
    {
        currentCoin-=value;
        PlayerPrefs.SetInt("Coin",currentCoin);
        coinAdded?.Invoke();
    }
}
