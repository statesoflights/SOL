using UnityEngine;
using System.Collections;

public class LightObject  {

    public GameObject spotlight { get; private set; }
    public string id { get; private set; }

    public LightObject(string id, GameObject spotlight)
    {
        this.id = id;
        this.spotlight = spotlight;
    }  


}
