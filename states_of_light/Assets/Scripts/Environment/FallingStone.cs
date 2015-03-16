using UnityEngine;
using System.Collections;

public class FallingStone : MonoBehaviour {

	public Animator anim;

	void OnTriggerStay(Collider other)
	{
		if (other.tag.Equals("Player") && Input.GetButtonDown("Action"))
		{
			anim.SetBool("Fall", true);
		}
	}
}
