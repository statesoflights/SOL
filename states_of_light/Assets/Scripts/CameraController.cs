using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour
{

    private Transform playerPos;

    private bool isMovingTowardPlayer;
    private Vector3 translateDestination;
    public float speed;

    void Awake()
    {
        playerPos = GameObject.FindGameObjectWithTag("Player").transform;
        speed = 10;
    }

    void Update()
    {
        if (!isMovingTowardPlayer)        MoveCamera();
        else MoveTowardPlayer();
    }

    private void MoveCamera()
    {
        transform.position = new Vector3(playerPos.position.x, transform.position.y, transform.position.z);
    }

    private void MoveTowardPlayer()
    {
        translateDestination = transform.position;
        translateDestination.x = playerPos.position.x;

        float distance = Mathf.Abs(transform.position.x - playerPos.position.x);
        float step = speed * distance * Time.deltaTime;
        //if (step <= 5)
        //    step = 5;
        if (distance<0.1F) 
            isMovingTowardPlayer = false;
        else        
            transform.position = Vector3.MoveTowards(transform.position, translateDestination, step);

    }

    public void SwitchPlayer(Transform playerPos)
    {
        this.playerPos = playerPos;
        isMovingTowardPlayer = true;
    }
}