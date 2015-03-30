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

        line.enabled = true;
        light.enabled = true;

        PlanPosition_current = pc_Zac.playerPosition;

        while (Input.GetButton("Fire2"))
        {
            UpdateLaserPlan();

            mouse = RetrieveMousePosition();

            gameObject.transform.LookAt(mouse);

            SetLinePositions();

            yield return null;
        }

        line.enabled = false;
        light.enabled = false;
        isFiringLaser = false;
    }

    Vector3 RetrieveMousePosition()
    {
        Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);

        float planeZ = PlanPosition_current * 5;

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
                lentille.UpdateSpotLight(lentilleSpotLight_ID, Quaternion.LookRotation(hit.point - transform.position));
            }

            line.SetPosition(1, hit.point);

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
