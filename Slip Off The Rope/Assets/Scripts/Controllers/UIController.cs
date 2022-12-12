using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIController : MonoBehaviour
{
    [SerializeField]
    private PlayerController player;

    [SerializeField]
    private GameObject startPanel;

    [SerializeField]
    private GameObject ınGamePanel;

    [SerializeField]
    private GameObject failPanel;

    [SerializeField]
    private GameObject succesPanel;

    [SerializeField]
    private TextMeshProUGUI coinText;

    [SerializeField]
    private CoinManager coinManager;

    void Start()
    {  
        GameManager.Instance.GameStateChanged+=OnGameStateChanged;
        coinManager.coinAdded+=OnCoinAdded;
    }



    // Update is called once per frame
    void Update()
    {
        
    }


    private void AllPanelClose()
    {
        startPanel.SetActive(false);
        succesPanel.SetActive(false);
        failPanel.SetActive(false);
    }

    public void GameIsStart()
    {
        GameManager.Instance.UpdateGameState(GameStates.InGame);
    }
    public void GameIsRestart()
    {
        GameManager.Instance.UpdateGameState(GameStates.InGame);
    }

    public void UpgradeStamina()
    {
        player.MaxStamina+=2;
        coinManager.DecreaseCoin(10);
    }
    public void UpgradeSpeed()
    {
        player.DownForce+=.3f;
        coinManager.DecreaseCoin(10);
    }

    private void OnCoinAdded()
    {
        coinText.text=coinManager.CurrentCoin.ToString();
    }
    private void OnGameStateChanged()
    {
        var state=GameManager.Instance.GameState;
        switch (state)
        {
            case GameStates.Start:
                AllPanelClose();
                startPanel.SetActive(true);
                coinText.text=PlayerPrefs.GetInt("Coin").ToString();
                break;
            case GameStates.InGame:
                AllPanelClose();
                ınGamePanel.SetActive(true);
                break;

            case GameStates.Fail:  
                AllPanelClose();
                failPanel.SetActive(true);
                break;
            case GameStates.Succes:
                AllPanelClose();
                succesPanel.SetActive(true);
                break;
        }
    }
}
