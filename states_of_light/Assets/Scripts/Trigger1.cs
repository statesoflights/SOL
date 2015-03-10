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

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	}

	void OnTriggerStay(Collider other)
	{
		if (other.tag.Equals("Player") && Input.GetButtonDown("Fire1"))
		{
			//SendMessage("OnTriggerActivated", SendMessageOptions.DontRequireReceiver);
			/*foreach (Activable script in GetComponents<Activable>())
			{
				script.OnActivate();
			}*/
			foreach (MonoBehaviour script in GetComponents<MonoBehaviour>())
			{
				if (script is IActivable)
				{
					((IActivable)script).OnActivate();
				}
			}
		}
	}
}
