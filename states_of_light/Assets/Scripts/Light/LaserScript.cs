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

    void Awake()
    {
        pc_Zac = transform.GetComponentInParent<PlayerController>();
        light = transform.GetComponentInChildren<Light>();
    }

    // Use this for initialization
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
    }

    // Update is called once per frame
    void Update()
    {
        if (!isFiringLaser)
        {
            if (Input.GetButtonDown("Fire2"))
            {
                isFiringLaser = true;
                StopCoroutine("FireLaser");
                StartCoroutine("FireLaser");
            }
            else if (lentilleSpotLight_ID != -1)
            {
                lentille.Desactivate(lentilleSpotLight_ID);
                lentilleSpotLight_ID = -1;
            }
        }
    }

    IEnumerator FireLaser()
    {
        float hit_Distance;

        line.enabled = true;
        light.enabled = true;

        PlanPosition_current = pc_Zac.playerPosition;
        
        while (Input.GetButton("Fire2"))
        {
            //line.renderer.material.mainTextureOffset = new Vector2(0, Time.time);

            UpdateLaserPlan();

            Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);

            float planeZ = PlanPosition_current * 5;

            float distance = (planeZ - mouseRay.origin.z) / mouseRay.direction.z;

            mouse = mouseRay.GetPoint(distance);

            //Debug.Log(mouse);
            gameObject.transform.LookAt(mouse);
            Ray rayFromGun = new Ray(transform.position, mouse - transform.position );
            RaycastHit hit;

            line.SetPosition(0, transform.position);

            if (Physics.Raycast(rayFromGun, out hit, 100))
            {
                if (lentilleSpotLight_ID == -1)
                {
                    if (hit.collider.tag == "Lentille")
                    {
                        lentille = hit.collider.GetComponent<Lentille>();
                        lentilleSpotLight_ID = lentille.Activate(Quaternion.LookRotation(hit.point - transform.position));
                        light.enabled = false;
                    }
                    else if (hit.collider.tag == "Player")
                    {
                        line.enabled = false;
                        light.enabled = false;
                    }
                    else
                    {
                        line.enabled = true;
                        light.enabled = true;
                    }
                }
                else if (hit.collider.tag != "Lentille")
                {
                    lentille.Desactivate(lentilleSpotLight_ID);
                    lentilleSpotLight_ID = -1;
                    light.enabled = true;
                }
                else
                {
                  
                }

                line.SetPosition(1, hit.point);
                hit_Distance = Vector3.Distance(transform.position, hit.point);

                //Update la position du point light, position juste avant le hit point de la line renderer
                light.transform.position = rayFromGun.GetPoint(hit_Distance - 1F);
            }
            else
            {
                line.SetPosition(1, rayFromGun.GetPoint(15));
                light.transform.position = rayFromGun.GetPoint(14.5F);
            }

            yield return null;
        }

        line.enabled = false; 
        light.enabled = false;
        isFiringLaser = false;
    }

    void UpdateLaserPlan()
    {
        if (Input.GetAxis("Mouse ScrollWheel") > 0 && PlanPosition_current + 1 <= PlanPosition_max)
            PlanPosition_current += 1;
        else if (Input.GetAxis("Mouse ScrollWheel") < 0 && PlanPosition_current - 1 >= PlanPosition_min)
            PlanPosition_current -= 1;
    }
}
