using UnityEngine;
using System.Collections;

public class Trigger_GUI : MonoBehaviour {

    public CanvasGroup anchor;

    void Start()
    {
        anchor.alpha = 0;
    }

    void OnTriggerStay(Collider other)
    {
        anchor.alpha = 1;
    }
    void OnTriggerExit(Collider other)
    {
        anchor.alpha = 0;
    }
}
