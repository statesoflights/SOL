using UnityEngine;
using System.Collections;

public class DenkiDoor : MonoBehaviour {
	
	public Animator animG;
	public Animator animD;
	public LaserScript ls;
    public Trigger_CanWalkUp trigger_CanWalkUp;
	
	public void StartAnim(){
		StartCoroutine("Anim");
	}
	IEnumerator Anim()
	{	
		animG.SetBool("open_door", true);
        animD.SetBool("open_door", true);
        yield return new WaitForSeconds(2.0f);
        ls.EndFiringLaser();
        trigger_CanWalkUp.conditionsAchieved = true;
        gameObject.SetActive(false);
	}
}