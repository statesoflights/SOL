using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

    public Transform target;

    private bool isMovingTowardPlayer;
    private Vector3 translateDestination;
	private ArrayList listElementTransparent = new ArrayList();

	public Material matOpaque;
	public Material matTransparent;
    public float speed;
	public float Xmin;
	public float Xmax;

    void Start()
    {
        speed = 10;
    }

    void FixedUpdate()
    {

        if (!isMovingTowardPlayer) MoveCamera();
        else MoveTowardPlayer();

		CheckTransparenty ();

    }

	private void CheckTransparenty()
	{
		for (int j = 0; j < listElementTransparent.Count; j++)
		{
			SpriteRenderer rend = (SpriteRenderer) listElementTransparent[j];
			if (rend) {
				rend.sharedMaterial = matOpaque;
//				Color tempColor = rend.color;
//				tempColor.a = 1F;
//				rend.color = tempColor;
			}
		}
		listElementTransparent.Clear();
		
		Vector3 directionCamPlayer = new Vector3 (target.position.x - transform.position.x, target.position.y - transform.position.y, target.position.z - transform.position.z);
		float distanceCamPlayer = directionCamPlayer.magnitude;
		RaycastHit[] hits;
		hits = Physics.RaycastAll(transform.position, directionCamPlayer, distanceCamPlayer - 1.0F);

        Debug.DrawLine(transform.position, new Vector3(target.position.x,target.position.y,target.position.z-1), Color.red);

		for (int i = 0; i < hits.Length; i++) {
			RaycastHit hit = hits [i];
			SpriteRenderer[] spriteRenderers = hit.collider.GetComponentsInChildren<SpriteRenderer> ();
			if (hit.collider.tag != "Statue") {
				foreach (SpriteRenderer rend in spriteRenderers) 
                {
					if (rend) {
						rend.sharedMaterial = matTransparent;
//					Debug.Log ("TRANSPARENT BITCH");
//					Color tempColor = rend.color;
//					tempColor.a = 0.3F;
//					rend.color = tempColor;
					}
					listElementTransparent.Add (rend);
				}
                //if (hit.collider.GetComponent<SpriteRenderer>())
                //{
                //    hit.collider.GetComponent<SpriteRenderer>().sharedMaterial = matTransparent;
                //    listElementTransparent.Add(hit.collider.GetComponent<SpriteRenderer>());
                //}

				//SpriteRenderer rend = hit.transform.GetComponent<SpriteRenderer>();		
			}
		}
	}

    private void MoveCamera()
    {
        transform.position = new Vector3(target.position.x, transform.position.y, transform.position.z);
		Vector3 newPositionMinMax = transform.position;
		if (transform.position.x <= Xmin) newPositionMinMax.x = Xmin;
		if (transform.position.x >= Xmax) newPositionMinMax.x = Xmax;
		transform.position = newPositionMinMax;
    }

    private void MoveTowardPlayer()
    {
        translateDestination = transform.position;
        translateDestination.x = target.position.x;

        float distance = Mathf.Abs(transform.position.x - target.position.x);
        float step = speed * distance * Time.deltaTime;

        //if (step <= 0.1)
        //step = 0.1F;
        if (distance < 0.01F)
        {
            target.GetComponent<PlayerController>().ActivatePlayer();
            isMovingTowardPlayer = false;
        }
        else
            transform.position = Vector3.MoveTowards(transform.position, translateDestination, step);

    }


    public void SwitchPlayer(Transform playerPos)
    {
        target = playerPos;
        isMovingTowardPlayer = true;
    }
}
