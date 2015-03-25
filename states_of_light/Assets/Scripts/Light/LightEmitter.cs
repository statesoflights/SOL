﻿using UnityEngine;
using System.Collections;

public class LightEmitter : MonoBehaviour {

    public Transform target;

    private LineRenderer line;
    
    private bool isInterruptor1_Active;
    private bool isInterruptor2_Active;

    public bool canEmitLight;

	void Start () {
        isInterruptor1_Active = false;
        isInterruptor2_Active = false;
        canEmitLight = false;

        line = GetComponent<LineRenderer>();
        line.enabled = false;
        light.enabled = false;
	}

    public void InterruptorIsActive(int id)
    {
        if (id == 0)
            isInterruptor1_Active = true;
        else
            isInterruptor2_Active = true;

        if (!canEmitLight && isInterruptor1_Active && isInterruptor2_Active)
        {
            canEmitLight = true;

            StopAllCoroutines();
            StartCoroutine(DrawLight());
        }
    }
    public void InterruptorIsInactive(int id)
    {
        if (id == 0)
            isInterruptor1_Active = false;
        else
            isInterruptor2_Active = false;

        if (!isInterruptor1_Active || !isInterruptor2_Active)
            canEmitLight = false ;
    }

    IEnumerator DrawLight()
    {
        line.enabled = true;
        light.enabled = true;

        while (canEmitLight)
        {
            transform.LookAt(target);
            Ray rayToTarget = new Ray(transform.position, target.position - transform.position);
            RaycastHit hit;

            line.SetPosition(0, transform.position);

            if (Physics.Raycast(rayToTarget, out hit, 100))
            {
                line.SetPosition(1, hit.point);
            }
            else
                line.SetPosition(1, rayToTarget.GetPoint(10));

            yield return null;
        }

        line.enabled = false;
        light.enabled = false;
    }
}