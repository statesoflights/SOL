using UnityEngine;
using System.Collections;

public class Trigger_Anim : MonoBehaviour {

    public Animator anim;
	public bool punchabelObject;
	public GameObject mur;

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
			{
                anim.SetBool("BrokenWall", true);
				mur.GetComponent<Collider>().enabled = false;
			}
        }
    }
	public void StartAnim()
	{
		anim.SetBool("BrokenWall", true);
	}
}
