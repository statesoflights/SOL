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
		Debug.Log ("start anim");
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
			Debug.Log ("start anim 2");
            yield return new WaitForSeconds(2.0f);
            anim.SetBool("Trigger", true);
            ls.EndFiringLaser();
            gameObject.SetActive(false);
        }
	}
}
