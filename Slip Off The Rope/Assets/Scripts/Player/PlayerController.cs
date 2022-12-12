using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float downForce;

    [SerializeField]
    private int maxStamina;

    [SerializeField]
    private GameObject ınGamePos;

    [SerializeField]
    private GameObject finalPos;

    [SerializeField]
    private CoinManager coinManager;

    private int currentStamina;
    private Vector3 startPos;
    private Transform materialBody;
    private bool isFire=false;
    private Animator animator;

    public int CurrentStamina=>currentStamina;

    public int MaxStamina
    {
         get
         {
            return maxStamina;
         }
         set
         {
            maxStamina=value;
            PlayerPrefs.SetInt("MaxStamina",maxStamina);
         } 
    }
    public float DownForce
    {
         get
         {
            return downForce;
         }
         set
         {
            downForce=value;
            PlayerPrefs.SetFloat("Speed",downForce);
         } 
    }


    void Start()
    {
        GameManager.Instance.GameStateChanged+=OnGameStateChanged;
        InputSystem.Instance.Touch+=OnTouch;
        startPos=transform.position;
        PlayerPrefs.SetInt("MaxStamina",maxStamina);
        PlayerPrefs.SetFloat("Speed",downForce);
        animator=transform.GetChild(0).GetComponent<Animator>();

        
    }

    
    private void OnTriggerEnter(Collider other)
    {
        var finalGround=other.GetComponent<FinalGround>();
        if(finalGround)
        {
            GameManager.Instance.UpdateGameState(GameStates.Succes);
        }
    }

    private void OnGameStateChanged()
    {
        var state=GameManager.Instance.GameState;
        switch (state)
        {
            case GameStates.Start:
                transform.position=startPos;
                currentStamina=0;
                break;
            case GameStates.InGame:
                animator.SetTrigger("Grab");
                transform.position=ınGamePos.transform.position;
                transform.localRotation=new Quaternion(0,0,0,0);
                if(isFire)
                {
                    maxStamina=PlayerPrefs.GetInt("MaxStamina");
                    currentStamina=0;
                    transform.gameObject.SetActive(true);
                    animator.SetTrigger("Grab");
                }
                break;

            case GameStates.Succes:
                coinManager.addLevelUpCoin();
                transform.position=finalPos.transform.position;
                animator.SetTrigger("Idle");
                break;
            case GameStates.Fail:
                animator.SetTrigger("Idle");
                
                //anim bomb
                break;
        }
    }

    private void OnTouch(Touch touch)
    {
       if(GameManager.Instance.GameState==GameStates.InGame)
       {
            if(touch.phase==TouchPhase.Ended)
                LeanTween.moveLocalY(gameObject,transform.position.y+-downForce*.5f,.1f);
                
            if(currentStamina>=maxStamina)
            {
                //anim patla
                transform.gameObject.SetActive(false);
                isFire=true;
                GameManager.Instance.UpdateGameState(GameStates.Fail);

            }
            else
            {
                if(touch.phase==TouchPhase.Ended)
                {
                    currentStamina+=1;
                    coinManager.addStrokeCoin();
                }
              
            }
            Debug.Log(currentStamina); 

          if(currentStamina>=maxStamina-2)
          {
            //materialBody.GetComponent<Material>().color=Color.red;
          }
           
        }
    }
}
