using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

    public Transform target;

    private bool isMovingTowardPlayer;
    private Vector3 translateDestination;
    public float speed;

    void Start()
    {
        speed = 10;
    }

    void FixedUpdate()
    {

        if (!isMovingTowardPlayer) MoveCamera();
        else MoveTowardPlayer();
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
