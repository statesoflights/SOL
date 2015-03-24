using UnityEngine;
using System.Collections;

public class LaserScript : MonoBehaviour {

    LineRenderer line;
    PlayerController pc_Zac;

    private Vector3 mouse;
    
    public int PlanPosition_current;
    private int PlanPosition_min;
    private int PlanPosition_max;


    void Awake()
    {
        pc_Zac = transform.GetComponentInParent<PlayerController>();
    }

    // Use this for initialization
    void Start()
    {
        line = gameObject.GetComponent<LineRenderer>();

        PlanPosition_min = 0;
        PlanPosition_max = 2;

        //gameObject.light.enabled = false;
        line.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire2"))
        {
            StopCoroutine("FireLaser"); 
            StartCoroutine("FireLaser");
        }
    }

    IEnumerator FireLaser()
    {
        line.enabled = true;

        //gameObject.light.enabled = true;
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
            //gameObject.transform.LookAt(mouse);
            Ray rayFromGun = new Ray(transform.position, mouse - transform.position );
            RaycastHit hit;

            line.SetPosition(0, transform.position);

            if (Physics.Raycast(rayFromGun, out hit, 100))
            {
                line.SetPosition(1, hit.point);
            }
            else
                line.SetPosition(1, rayFromGun.GetPoint(10));

            yield return null;
        }

        line.enabled = false;
        //gameObject.GetComponent<Light>().enabled = false;
    }

    void UpdateLaserPlan()
    {
        if (Input.GetAxis("Mouse ScrollWheel") > 0 && PlanPosition_current + 1 <= PlanPosition_max)
            PlanPosition_current += 1;
        else if (Input.GetAxis("Mouse ScrollWheel") < 0 && PlanPosition_current - 1 >= PlanPosition_min)
            PlanPosition_current -= 1;
    }
}
