using UnityEngine;
using System.Collections;

public class Trigger_Anim : MonoBehaviour {

    public Animator anim;
	public bool punchabelObject;

    private bool startedcoroutine;

    void Start()
    {
        startedcoroutine = false;
    }

    void OnTriggerStay(Collider other)
    {
        if (other.tag.Equals("Player") && Input.GetButton("Action"))
        {
            if (punchabelObject)
            {
                if(!startedcoroutine)
                other.GetComponent<Biggy>().StopCoroutine("HitStart");
                other.GetComponent<Biggy>().StartCoroutine("HitStart", this);
                startedcoroutine = true;
            }
            else 
                anim.SetBool("Trigger", true);
        }
    }
	public void StartAnim()
	{
		anim.SetBool("Trigger", true);
	}
}
