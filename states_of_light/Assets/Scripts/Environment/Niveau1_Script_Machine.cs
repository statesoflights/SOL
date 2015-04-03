using UnityEngine;

public class Niveau1_Script_Machine : MonoBehaviour, IActivable
{

    public Transform machine; //La machine à broyer qui va faire tomber une pierre
    public Transform colliderPierre; //La pierre qui tombe de sur la machine
    public Transform playerBiggy;
    public float playerXLimit;
    private Animator animator;

	void Awake()
	{
		//GameController gameController = GameController.Instance;
        if (playerXLimit == 0)
            playerXLimit = 98.5F;
		animator = machine.GetComponent<Animator>();
	}

	public void OnActivate ()
	{
        if (playerBiggy.position.x > playerXLimit)
        {
            Vector3 temp = playerBiggy.position;
            temp.x = playerXLimit;
            playerBiggy.transform.position = temp;
        }

		//Animation machine vibre et pierre tombe
		animator.SetInteger("state", 1);

		//Activation collider pierre tombee
		colliderPierre.transform.Translate (new Vector3 (-1, -3, 0));
	}
}
