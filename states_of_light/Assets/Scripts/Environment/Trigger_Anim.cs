using UnityEngine;
using System.Collections;

public class Trigger_Anim : MonoBehaviour {

    public Animator anim;

    void OnTriggerStay(Collider other)
    {
        if (other.tag.Equals("Player") && Input.GetButtonDown("Action"))
        {
            anim.SetBool("Trigger", true);
        }
    }
}
