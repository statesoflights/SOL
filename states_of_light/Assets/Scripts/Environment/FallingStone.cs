using UnityEngine;
using System.Collections;

public class FallingStone : MonoBehaviour {

	public Animator anim;

	void OnTriggerStay(Collider other)
	{
		if (other.tag.Equals("PlayerSmall") && Input.GetButtonDown("Fire1"))
		{
			anim.SetBool("Fall", true);
		}
	}
}
