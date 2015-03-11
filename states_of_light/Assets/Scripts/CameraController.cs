using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

    public Transform target;

    private bool isMovingTowardPlayer;
    private Vector3 translateDestination;
	private ArrayList listElementTransparent = new ArrayList();

    public float speed;

    void Start()
    {
        speed = 10;
    }

    void FixedUpdate()
    {

        if (!isMovingTowardPlayer) MoveCamera();
        else MoveTowardPlayer();

		for (int j = 0; j < listElementTransparent.Count; j++)
		{
			SpriteRenderer rend = (SpriteRenderer) listElementTransparent[j];
			if (rend) {
				Color tempColor = rend.color;
				tempColor.a = 1F;
				rend.color = tempColor;
			}
		}
		listElementTransparent.Clear();
		Vector3 directionCamPlayer = new Vector3 (target.position.x - transform.position.x, target.position.y - transform.position.y, target.position.z - transform.position.z);
		float distanceCamPlayer = directionCamPlayer.magnitude;
		RaycastHit[] hits;
		hits = Physics.RaycastAll(transform.position, directionCamPlayer, distanceCamPlayer - 1.0F);
		int i = 0;
		while (i < hits.Length) {
			RaycastHit hit = hits[i];
			SpriteRenderer rend = hit.transform.GetComponent<SpriteRenderer>();
			if (rend) {
				Color tempColor = rend.color;
				tempColor.a = 0.3F;
				rend.color = tempColor;
			}
			listElementTransparent.Add(rend);
			i++;

		}

    }

    private void MoveCamera()
    {
        transform.position = new Vector3(target.position.x, transform.position.y, transform.position.z);
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
