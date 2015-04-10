using UnityEngine;
using System.Collections;

public class Guard_Trigger : MonoBehaviour {

    public Guard_Detection guard;
    public float wait_time;
    public int countDetected;

    void Start()
    {
        //wait_time = 0f;
        countDetected = 0;
    }

    IEnumerator WaitfewSecs(Collider other)
    {
        guard.PlayerDetected();
        yield return new WaitForSeconds(wait_time);

        other.GetComponent<Rigidbody>().velocity = Vector3.zero;
        Vector3 dest_pos = other.transform.position;
        dest_pos.x = 72.5f;
        other.transform.position = dest_pos;
    }

    void OnTriggerEnter(Collider other)
    {
        if ((other.tag.Equals("Player") || other.tag.Equals("PlayerSmall")) && guard.isActive)
        {
            countDetected++;
            //StopCoroutine("WaitfewSecs");
            StartCoroutine("WaitfewSecs",other);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if ((other.tag.Equals("Player") || other.tag.Equals("PlayerSmall")))
        {
            countDetected--;
            if (countDetected == 0)
            {
                StopAllCoroutines();
                guard.PlayerNotDetected();
            }
        }
    }
}
