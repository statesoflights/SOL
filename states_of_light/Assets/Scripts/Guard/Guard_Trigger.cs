using UnityEngine;
using System.Collections;

public class Guard_Trigger : MonoBehaviour {

    public Guard_Detection guard;
    float wait_time;

    void Start()
    {
        wait_time = 0.5f;
    }

    IEnumerator WaitfewSecs()
    {
        yield return new WaitForSeconds(wait_time);
        guard.PlayerDetected();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Player") || other.tag.Equals("PlayerSmall"))
        {
            StopAllCoroutines();
            StartCoroutine(WaitfewSecs());
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag.Equals("Player") || other.tag.Equals("PlayerSmall"))
        {
            StopAllCoroutines();
            guard.PlayerNotDetected();
        }
    }
}
