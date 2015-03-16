using UnityEngine;
using System.Collections;

/*public abstract class Activable : MonoBehaviour
{
	public abstract void OnActivate();
}*/

public interface IActivable
{
	void OnActivate();
}

public class Trigger1 : MonoBehaviour {

	public GameObject machine; //La machine à broyer qui va faire tomber une pierre
	private Animator animator;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	}

	void OnTriggerStay(Collider other)
	{
		animator = machine.GetComponent<Animator>();
		int triggerState = animator.GetInteger("state");

		if (other.tag.Equals("PlayerSmall") && Input.GetButtonDown("Action") && triggerState != 1)
		{
			foreach (MonoBehaviour script in GetComponents<MonoBehaviour>())
			{
				if (script is IActivable)
				{
					((IActivable)script).OnActivate();
				}
            }
            Destroy(gameObject);
		}
	}
}
