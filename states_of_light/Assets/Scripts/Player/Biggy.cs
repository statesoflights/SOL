using UnityEngine;
using System.Collections;

public class Biggy : MonoBehaviour {

    public GameObject playerSmall;

    void RecallSmall()
    {
        playerSmall.GetComponent<Small>().StartFollowPlayerAnim();
    }
}
