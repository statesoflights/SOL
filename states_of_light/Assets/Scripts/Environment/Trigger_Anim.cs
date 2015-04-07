using UnityEngine;
using System.Collections;

public class Trigger_Anim : MonoBehaviour {

    public Animator anim;

    void OnTriggerStay(Collider other)
    {
        if (other.tag.Equals("Player") && Input.GetButton("Action"))
        {
            anim.SetBool("Trigger", true);
        }
    }
}
