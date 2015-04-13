using UnityEngine;
using System.Collections;

public class Trigger_Laser : MonoBehaviour {

	public Animator anim;
	public LaserScript ls;
    public Animator animdoor1;
    public Animator animdoor2;
    public Trigger_CanWalkUp triggerCanWalkUp;

	public void StartAnim(){
		StartCoroutine("Anim");
	}
	IEnumerator Anim()
    {
        if (triggerCanWalkUp)
        {
            animdoor1.SetBool("Trigger", true);
            animdoor2.SetBool("Trigger", true);
        }
        else
        {
            yield return new WaitForSeconds(2.0f);
            anim.SetBool("Trigger", true);
            ls.canFire = false;
            Destroy(gameObject);
        }
	}
}
