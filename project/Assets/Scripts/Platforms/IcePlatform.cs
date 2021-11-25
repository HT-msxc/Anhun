using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IcePlatform : MonoBehaviour
{

    public int Incount = 0;
    public bool PlayerIn;
    public float CrashTime = 3f;
    public float ReloadTime = 3.0f;
    public bool PlayerMove = false;
    public bool startDissolve = false;
    public GameObject CurrentPlayer;
    public Player player;
    public NatureState PlayerState;
    public PlayerMovement CurrentPlayerMovement;
    public GameObject GameobjectCollider;
    public Dissovle dissovle;
    private void Update()
    {
        if(PlayerIn)
        {
            PlayerState = player.GetNatureState();
            if(CurrentPlayerMovement.IsOnGround)
            {
                CurrentPlayerMovement.canMove = false;
                if(PlayerState == NatureState.Fire)
                {

                        Invoke("StartDissolve", CrashTime);

                }
                else if(PlayerState == NatureState.Water)
                {
                    player.GetComponent<PlayerBattle>().IsInIceEffect = true;
                }
                else
                {
                    player.GetComponent<PlayerBattle>().IsInIceEffect = false;
                }
            }
            else
            {
                CurrentPlayerMovement.canMove = true;
            }
        }
        //PlayerMove = CurrentPlayerMovement.canMove;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            CurrentPlayer = other.gameObject;
            CurrentPlayerMovement = CurrentPlayer.GetComponent<PlayerMovement>();
            player = CurrentPlayer.GetComponent<Player>();
            dissovle = this.GetComponent<Dissovle>();
            PlayerState = player.GetNatureState();
            PlayerIn = true;
            if(PlayerState == NatureState.Fire || PlayerState == NatureState.Water)
                ++Incount;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            PlayerIn = false;
            player.GetComponent<PlayerBattle>().IsInIceEffect = false;
            CurrentPlayerMovement.canMove = true;
        }
    }
    void StartDissolve()
    {
        PlayerIn = false;
        GameobjectCollider.GetComponent<Collider2D>().enabled = false;
        this.gameObject.GetComponent<Collider2D>().enabled = false;
        dissovle.StartDissolve();
        Debug.Log("start dissove");
        Invoke("StartReload", ReloadTime);
    }

    void StartReload()
    {
        dissovle.CallBackDissolve();
        Incount = 0;
        Debug.Log("Start call back");
    }
}
