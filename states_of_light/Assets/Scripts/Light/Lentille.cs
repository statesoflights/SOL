using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class Lentille : MonoBehaviour {

    public List<GameObject> lights;
    public GameObject object_spotlight;

    void Awake()
    {
        lights = new List<GameObject>();
    }

    public int Activate(Quaternion target_Pos)
    {
        GameObject object_spotlight_temp = (GameObject)Instantiate(object_spotlight, transform.position, target_Pos);
        lights.Add(object_spotlight_temp);
        return lights.Count - 1;
    }

    public void Desactivate(int index)
    {
        Destroy(lights[index]);
        lights.RemoveAt(index);
    }

    //public void Desactivate(string id)
    //{
    //    lights.Find(O => O.id == id).SetActive(false);
    //}
     

    public void UpdateSpotLight(int index,Quaternion target_Pos)
    {
        lights[index].transform.rotation = target_Pos;
    }
}
