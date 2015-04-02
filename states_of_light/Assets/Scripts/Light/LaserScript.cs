using UnityEngine;
using System.Collections;

public class LaserScript : MonoBehaviour {

    LineRenderer line;
    Light light;
    PlayerController pc_Zac;

    private Vector3 mouse;
    
    public int PlanPosition_current;
    private int PlanPosition_min;
    private int PlanPosition_max;

    public int lentilleSpotLight_ID;
    private Lentille lentille;
    public bool isFiringLaser;
	public bool canFire;

    void Awake()
    {
        pc_Zac = transform.GetComponentInParent<PlayerController>();
        light = transform.GetComponentInChildren<Light>();
    }

    void Start()
    {
        line = gameObject.GetComponent<LineRenderer>();

        PlanPosition_min = 0;
        PlanPosition_max = 2;

        light.enabled = false;
        line.enabled = false;

        lentilleSpotLight_ID = -1;
        lentille = null;

        isFiringLaser = false;
		canFire = true;
    }

    void Update()
    {
        if (!isFiringLaser)
        {
            if (Input.GetButtonDown("Fire2"))
            {
				CheckClickedPoint();
            }
            else if (lentilleSpotLight_ID != -1)
            {
                lentille.Desactivate(lentilleSpotLight_ID);
                lentilleSpotLight_ID = -1;
            }
        }
    }
	void CheckClickedPoint()
	{
		for(int i =0;i<5;i++)
		{
            Debug.Log("Here");
			mouse = RetrieveMousePosition(i);
			Ray rayFromGun = new Ray(transform.position, mouse - transform.position);
			RaycastHit hit;
			if (Physics.Raycast(rayFromGun, out hit, 30F)&& 
			    (hit.collider.tag == "Lentille" || hit.collider.tag == "LaserTrigger"))
			{
				if(hit.collider.tag == "LaserTrigger")
					hit.collider.GetComponent<Trigger_Laser>().StartAnim();
				isFiringLaser = true;
				PlanPosition_current = i;
                Debug.Log("Firelaser " + i); 
				StopCoroutine("FireLaser");
				StartCoroutine("FireLaser");
				return;
			}

			/*
			RaycastHit[] hits;
			hits =Physics.RaycastAll (rayFromGun, 30F);
			for (int y = 0; y < hits.Length; y++) {
				RaycastHit hit = hits [y];
				if (hit.collider.tag == "Lentille") {
					foreach (SpriteRenderer rend in spriteRenderers) {
						if (rend) {
							rend.sharedMaterial = matTransparent;
							//					Debug.Log ("TRANSPARENT BITCH");
							//					Color tempColor = rend.color;
							//					tempColor.a = 0.3F;
							//					rend.color = tempColor;
						}
						listElementTransparent.Add (rend);
					}
					//SpriteRenderer rend = hit.transform.GetComponent<SpriteRenderer>();		
				}
			}*/


		}

	}

    IEnumerator FireLaser()
    {
        line.enabled = true;
        light.enabled = true;
		canFire = true;
        PlanPosition_current = pc_Zac.playerPosition;

        while (canFire)
        {
            UpdateLaserPlan();

			//mouse = RetrieveMousePosition(PlanPosition_current);

            gameObject.transform.LookAt(mouse);

            SetLinePositions();

            yield return null;
        }

        line.enabled = false;
        light.enabled = false;
        isFiringLaser = false;
    }

    Vector3 RetrieveMousePosition(int PlanPosition)
    {
        Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);

		float planeZ = PlanPosition * 2.5F;

        float distance = (planeZ - mouseRay.origin.z) / mouseRay.direction.z;

        return mouseRay.GetPoint(distance);
    }

    void SetLinePositions()
    {
        line.SetPosition(0, transform.position);

        Ray rayFromGun = new Ray(transform.position, mouse - transform.position);
        RaycastHit hit;
        if (Physics.Raycast(rayFromGun, out hit, 30F))
        {
            if (lentilleSpotLight_ID == -1)
            {
                if (hit.collider.tag == "Lentille")
                {
                    lentille = hit.collider.GetComponent<Lentille>();
					lentilleSpotLight_ID = lentille.Activate(Quaternion.LookRotation(hit.transform.position - transform.position));
                    light.enabled = false;
                }
				if(hit.collider.tag == "LaserTrigger")
				{
					//StartCoroutine("hit.collider.GetComponent<Trigger_Laser>().StartAnim()");
				}
            }
			else if (hit.collider.tag != "Lentille" && hit.collider.tag != "LaserTrigger")
            {
                lentille.Desactivate(lentilleSpotLight_ID);
                lentilleSpotLight_ID = -1;
                light.enabled = true;
            }
            else
            {
				lentille.UpdateSpotLight(lentilleSpotLight_ID, Quaternion.LookRotation(hit.transform.position - transform.position));
            }

            line.SetPosition(1, hit.transform.position);

            //Update la position du point light, position juste avant le hit point de la line renderer
            float hit_Distance = Vector3.Distance(transform.position, hit.point);
            light.transform.position = rayFromGun.GetPoint(hit_Distance - 0.1F);
        }
        else
        {
            line.SetPosition(1, rayFromGun.GetPoint(20));
            light.transform.position = rayFromGun.GetPoint(19.5F);

            if (lentilleSpotLight_ID != -1)
            {
                lentille.Desactivate(lentilleSpotLight_ID);
                lentilleSpotLight_ID = -1;
            }
        }
    }

    void UpdateLaserPlan()
    {
        if (Input.GetAxis("Mouse ScrollWheel") > 0 && PlanPosition_current + 1 <= PlanPosition_max)
            PlanPosition_current += 1;
        else if (Input.GetAxis("Mouse ScrollWheel") < 0 && PlanPosition_current - 1 >= PlanPosition_min)
            PlanPosition_current -= 1;
    }
}
