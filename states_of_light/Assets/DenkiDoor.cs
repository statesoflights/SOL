using UnityEngine;
using System.Collections;

public class DenkiDoor : MonoBehaviour {
	
	public Animator animG;
	public Animator animD;
	public LaserScript ls;
	
	public void StartAnim(){
		Debug.Log ("StartAnim");
		StartCoroutine("Anim");
	}
	IEnumerator Anim()
	{	
		yield return new WaitForSeconds(2.0f);
		animG.SetBool("open_door", true);
		animD.SetBool("open_door", true);
		ls.canFire = false;
		Destroy (gameObject);
	}
}