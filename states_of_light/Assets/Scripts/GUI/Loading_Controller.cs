using UnityEngine;
using System.Collections;

public class Loading_Controller : MonoBehaviour {

    public float waiting_Time;

	void Start () {

        if (waiting_Time == 0)
            waiting_Time = 5.0F;

        StartCoroutine(Loading());
	}

    IEnumerator Loading()
    {
        yield return new WaitForSeconds(waiting_Time);
        Application.LoadLevel(3);
    }
}
