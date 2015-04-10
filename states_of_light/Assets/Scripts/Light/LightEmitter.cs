using UnityEngine;
using System.Collections;

public class LightEmitter : MonoBehaviour {

    public Transform target;

    private LineRenderer line;
    
    private bool isInterruptor1_Active;
    private bool isInterruptor2_Active;

    public bool canEmitLight;


    private int lentilleSpotLight_ID;
    public Lentille lentille;

	void Start () {
        isInterruptor1_Active = false;
        isInterruptor2_Active = false;
        //canEmitLight = false;

        line = GetComponent<LineRenderer>();
        line.enabled = false;

        lentilleSpotLight_ID = -1;
        lentille = null;

        //GetComponent<Light>().enabled = false;
	}

    void Update()
    {
        if (canEmitLight)
        {
            StopAllCoroutines();
            StartCoroutine(DrawLight());
        }
        else if (lentilleSpotLight_ID != -1)
        {
            lentille.Desactivate("Lentille");
            lentilleSpotLight_ID = -1;
        }

    }

    public void InterruptorIsActive(int id)
    {
        if (id == 0)
            isInterruptor1_Active = true;
        else
            isInterruptor2_Active = true;

        if (!canEmitLight && (isInterruptor1_Active || isInterruptor2_Active))
        {
            canEmitLight = true;

            //StopAllCoroutines();
            //StartCoroutine(DrawLight());
        }
    }
    public void InterruptorIsInactive(int id)
    {
        if (id == 0)
            isInterruptor1_Active = false;
        else
            isInterruptor2_Active = false;

        if (!isInterruptor1_Active && !isInterruptor2_Active)
        {
            canEmitLight = false;

            //if (lentilleSpotLight_ID != -1)
            //{
            //    lentille.Desactivate(lentilleSpotLight_ID);
            //    lentilleSpotLight_ID = -1;
            //}
        }
    }

    IEnumerator DrawLight()
    {
        line.enabled = true;

        while (canEmitLight)
        {
            transform.LookAt(target);
            Ray rayToTarget = new Ray(transform.position, target.position - transform.position);
            RaycastHit hit;

            line.SetPosition(0, transform.position);
            
            if (Physics.Raycast(rayToTarget, out hit, 100))
            {
                line.SetPosition(1, hit.point);

                if (lentilleSpotLight_ID == -1)
                {
                    if (hit.collider.tag == "Lentille")
                    {
                        lentille = hit.collider.GetComponent<Lentille>();
                        lentilleSpotLight_ID = lentille.Activate(Quaternion.LookRotation(hit.point - transform.position),"LightEmitter");
                    }
                }
                else if (hit.collider.tag != "Lentille")
                {
                    lentille.Desactivate("LightEmitter");
                    lentilleSpotLight_ID = -1;
                }
                else
                {
                    lentille.UpdateSpotLight("LightEmitter", Quaternion.LookRotation(hit.point - transform.position));
                }
            }
            else
                line.SetPosition(1, rayToTarget.GetPoint(10));

            yield return null;
        }

        line.enabled = false;
    }
}
