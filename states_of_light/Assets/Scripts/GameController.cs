using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

	public static GameController Instance { get; private set; }

    private PlayerController playerBiggy;
    private PlayerController playerSmall;
    private PlayerController currentPlayer;
    private CameraController cameraController;

    void Awake()
    {
		Instance = this;

        playerBiggy = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        playerSmall = GameObject.FindGameObjectWithTag("PlayerSmall").GetComponent<PlayerController>();

        cameraController = Camera.main.GetComponent<CameraController>();       
    }

	void OnDestroy()
	{
		if (Instance == this)
		{
			Instance = null;
		}
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
        playerBiggy.ActivatePlayer();
        playerSmall.DesactivatePlayer();
        cameraController.target = playerBiggy.transform;
        currentPlayer = playerBiggy;
    }

    private void SwitchPlayer()
    {
        if (playerBiggy.isCurrentPlayer)
        {
            currentPlayer = playerSmall;
            playerBiggy.DesactivatePlayer();
            playerSmall.GetComponent<Small>().isFollowing = false;
            cameraController.SwitchPlayer(playerSmall.transform);
        }
        else
        {
            currentPlayer = playerBiggy;
            playerSmall.DesactivatePlayer();
            cameraController.SwitchPlayer(playerBiggy.transform);
        }
    }
}
