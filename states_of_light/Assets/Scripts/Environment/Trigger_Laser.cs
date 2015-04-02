using UnityEngine;
using System.Collections;

public class Trigger_Laser : MonoBehaviour {

	public Animator anim;
	public LaserScript ls;

	public void StartAnim(){
		StartCoroutine("Anim");
	}
	IEnumerator Anim()
	{	
		yield return new WaitForSeconds(2.0f);
		anim.SetBool("Trigger", true);
		ls.canFire = false;
		Destroy (gameObject);
	}
}
