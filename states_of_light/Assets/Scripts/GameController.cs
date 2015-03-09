using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

    private PlayerController player1;
    private PlayerController player2;
    private PlayerController currentPlayer;
    private CameraController cameraController;

    void Awake()
    {
        player1 = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        player2 = GameObject.FindGameObjectWithTag("PlayerSmall").GetComponent<PlayerController>();

        cameraController = Camera.main.GetComponent<CameraController>();       
    }

    void Start()
    {
        InitScene();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl) && currentPlayer.isGrounded) SwitchPlayer();
    }

    private void InitScene()
    {
        player1.ActivatePlayer();
        player2.DesactivatePlayer();
        cameraController.target = player1.transform;
        currentPlayer = player1;
    }

    private void SwitchPlayer()
    {
        if (player1.isCurrentPlayer)
        {
            currentPlayer = player2;
            player1.DesactivatePlayer();
            cameraController.SwitchPlayer(player2.transform);
        }
        else
        {
            currentPlayer = player1;
            player2.DesactivatePlayer();
            cameraController.SwitchPlayer(player1.transform);
        }
    }
}
