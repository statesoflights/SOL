using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class Lentille : MonoBehaviour {

    public List<LightObject> lights;
    public GameObject object_spotlight;
    public Small playerSmall;
    public bool isTrigger;
    public GameObject Target;

    void Awake()
    {
        lights = new List<LightObject>();
    }

    public int Activate(Quaternion target_Pos, string id)
    {
        if (!lights.Any(O => O.id == id))
        {
            GameObject object_spotlight_temp = (GameObject)Instantiate(object_spotlight, transform.position, target_Pos);
            LightObject temp_lo = new LightObject(id, object_spotlight_temp);
            lights.Add(temp_lo);
        }
        else
        {
            lights.Find(O => O.id == id).spotlight.SetActive(true);
        }
        return lights.Count - 1;
    }

    public void Desactivate(string id)
    {
        lights.Find(O => O.id == id).spotlight.SetActive(false);
    }

    public void UpdateSpotLight(string index, Quaternion target_Pos)
    {
        GameObject temp_lo = lights.Find(O => O.id == index).spotlight;
        temp_lo.transform.rotation = target_Pos;

		if (isTrigger && Mathf.Abs(transform.position.x-playerSmall.transform.position.x)<=0.5F && playerSmall.transform.position.z < 1 &&
            !playerSmall.isFollowing && playerSmall.pc.isGrounded)
            isTriggered();
    }

    public void isTriggered()
    {
        Target.GetComponent<Guard_Detection>().isActive = false;
        //Target.GetComponent<SpriteRenderer>().enabled = false;
		Target.GetComponent<Guard_Detection>().hasSeenShadow = true;

    }
}
