using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameController : MonoBehaviour
{
    
    public GameObject illumine;
    public List<GameObject> floors = new List<GameObject>();

    private int player_planPosition;
    private int pointed_plan;

    private bool reset_PlanShaders;
    private bool buttonUp;

    private Shader shader1;
    private Shader shader2;

    private PlayerController player1;
    private PlayerController player2;

    private CameraController cameraController;

    public GameObject lightobject;
    //private LaserScript laser;

    void Awake()
    {
        reset_PlanShaders = true;
        buttonUp = true;
        pointed_plan = 0;

        shader1 = Shader.Find("Self-Illumin/Diffuse");
        shader2 = Shader.Find("Diffuse");

        player1 = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        player2 = GameObject.FindGameObjectWithTag("Player2").GetComponent<PlayerController>();
        cameraController = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraController>();
        //laser = lightobject.GetComponent<LaserScript>();
    }

    void Start()
    {
        player1.ActivatePlayer();
        player2.DesactivatePlayer();
        player1.playerId = 0;
        player1.playerId = 1;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl)) SwitchPlayer();
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            HighlightNextPlan();
        }
        else if (!reset_PlanShaders && buttonUp) ResetShaders();
        if (Input.GetKeyUp(KeyCode.Tab)) buttonUp = true;
        //laser.layer = pointed_plan * 10;
    }


    private void HighlightNextPlan()
    {
        GetNextPointedPlan();
        illumine.transform.position = new Vector3(floors[pointed_plan].transform.position.x, 0.03f, floors[pointed_plan].transform.position.z);
        illumine.renderer.enabled = true;
        reset_PlanShaders = false;
        buttonUp = false;
    }
    private void GetNextPointedPlan()
    {
        if (pointed_plan + 1 < floors.Count)
            pointed_plan++;
        else pointed_plan = 0;

        //return pointed_plan;
    }

    private void ResetShaders()
    {
        illumine.renderer.enabled = false;
    }

    private void SwitchPlayer()
    {
        if (player1.isCurrentPlayer)
        {
            player1.DesactivatePlayer();
            player2.ActivatePlayer();
            cameraController.SwitchPlayer(player2.transform);
        }
        else
        {
            player2.DesactivatePlayer();
            player1.ActivatePlayer();
            cameraController.SwitchPlayer(player1.transform);
        }
    }

}
