using UnityEngine;
using System.Collections;

public class BiggyParent : MonoBehaviour {

    Animator anim;
    SpriteRenderer sr;
    private SpriteRenderer sr_Biggy;
    Biggy biggy;

	void Awake () {
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        sr_Biggy = transform.GetComponentInParent<SpriteRenderer>();
        biggy = transform.GetComponentInParent<Biggy>();
	}
    void Start()
    {
        sr.enabled = false;
    }
	
	public void StartAnim () {
        sr_Biggy.enabled = false;
        sr.enabled = true;
        if (biggy.Fusion)
            anim.Play("WallClimb_Right_Fusion");
        else
            anim.Play("WallClimb_Right");
	}
    public void AnimEnded()
    {
        sr_Biggy.enabled = true;
        sr.enabled = false;
        biggy.ClimbWallEnded();
    }
}
