using UnityEngine;
using System.Collections;

public class Trigger_CanWalkUp : MonoBehaviour {

    public bool canWalkUp;
    public bool conditionsAchieved;

	void Start () {
        canWalkUp = false;
        conditionsAchieved = false;
	}


    void OnTriggerStay(Collider other)
    {
        if (conditionsAchieved && other.tag.Equals("Player"))
        {
            canWalkUp = true;
            if (Input.GetAxis("Vertical") > 0 && 
            other.GetComponent<PlayerController>().isCurrentPlayer &&
                !other.GetComponent<PlayerController>().isMovingVertically)
            {
                other.GetComponent<Biggy>().moveToEndLevel();
            }
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.tag.Equals("Player"))
        {
            canWalkUp = false;
        }
    }
}
