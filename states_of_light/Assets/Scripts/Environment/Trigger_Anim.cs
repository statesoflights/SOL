using UnityEngine;
using System.Collections;

public class Trigger_Anim : MonoBehaviour {

    public Animator anim;
	public bool punchabelObject;

    void OnTriggerStay(Collider other)
    {
        if (other.tag.Equals("Player") && Input.GetButton("Action"))
        {
			if(punchabelObject)
			other.GetComponent<Biggy>().StartCoroutine("HitStart",transform);
			else
				anim.SetBool("Trigger", true);
        }
    }
	public void StartAnim()
	{
		anim.SetBool("Trigger", true);
	}
}
