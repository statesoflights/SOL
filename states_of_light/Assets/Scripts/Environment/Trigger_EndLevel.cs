using UnityEngine;
using System.Collections;

public class Trigger_EndLevel : MonoBehaviour {

	void OnTriggerStay(Collider other)
	{
		if (other.tag.Equals("Player"))
		{			
			Application.LoadLevel(2);
		}
	}
}
