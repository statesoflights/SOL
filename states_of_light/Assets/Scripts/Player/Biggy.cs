using UnityEngine;
using System.Collections;

public class Biggy : MonoBehaviour {

    public GameObject playerSmall;

    void Awake()
    {

    }

    void Update()
    {
        if (!playerSmall.GetComponent<Small>().isFollowing && 
            Input.GetButtonDown("SwitchPlayer") && 
            GetComponent<PlayerController>().isCurrentPlayer && 
            playerSmall.GetComponent<PlayerController>().isGrounded)
            RecallSmall();
    }

    void RecallSmall()
    {
        playerSmall.GetComponent<Small>().StartFollowPlayerAnim();
    }
}
