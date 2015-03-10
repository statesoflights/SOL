using UnityEngine;

public class Niveau1_Script_Machine : MonoBehaviour, IActivable
{

	public GameObject machine; //La machine à broyer qui va faire tomber une pierre
	public GameObject colliderPierre; //La pierre qui tombe de sur la machine
	private Animator animator;

	void Awake()
	{
		GameController gameController = GameController.Instance;

		animator = machine.GetComponent<Animator>();
	}

	public void OnActivate ()
	{
		Debug.Log ("script1 active");



		//Animation Bouton appui

		//Animation machine vibre et pierre tombe
		animator.SetInteger("state", 1);

		//Activation collider pierre tombee
		colliderPierre.transform.Translate (new Vector3 (-1, -3, 0));
	}
}
