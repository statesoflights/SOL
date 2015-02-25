using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour
{

    private Transform playerPos;

    private bool isMovingTowardPlayer;
    private Vector3 translateDestination;
    private float speed;

    void Awake()
    {
        playerPos = GameObject.FindGameObjectWithTag("Player").transform;
        speed = 1F;
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
        float step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, playerPos.position, step);
        if(transform.position == playerPos.position)
            isMovingTowardPlayer = false;
    }

    public void SwitchPlayer(Transform playerPos)
    {
        this.playerPos = playerPos;
        //translateDestination =  
        isMovingTowardPlayer = true;
    }
}