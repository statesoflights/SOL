using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

	public static GameController Instance { get; private set; }

    private PlayerController pc_Biggy;
    private PlayerController pc_Small;
    private Small small;
    private Biggy biggy;
    private PlayerController currentPlayer;
    private CameraController cameraController;
    
    public bool isRecalling;

    void Awake()
    {
		Instance = this;

        pc_Biggy = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        biggy = pc_Biggy.GetComponent<Biggy>();
        pc_Small = GameObject.FindGameObjectWithTag("PlayerSmall").GetComponent<PlayerController>();
        small = pc_Small.GetComponent<Small>();

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
        isRecalling = false;

        InitScene();
    }

    void FixedUpdate()
    {
        if (Input.GetButtonDown("SwitchPlayer") && currentPlayer.canSwitch) SwitchPlayer();

        if (Input.GetButtonDown("RecallSmallPlayer") && currentPlayer.isGrounded && !isRecalling)
        {
            if (pc_Biggy.isCurrentPlayer && !pc_Small.GetComponent<Small>().isFollowing)
            {
                RecallSmall();
            }
            if (pc_Small.isCurrentPlayer)
            {
                StartCoroutine("Recall");
            }
        }
        
    }

    private void InitScene()
    {
        pc_Biggy.ActivatePlayer();
        pc_Small.DesactivatePlayer();
        cameraController.target = pc_Biggy.transform;
        currentPlayer = pc_Biggy;
    }

    private void RecallSmall()
    {
        pc_Small.GetComponent<Small>().StartFollowPlayerAnim();
        biggy.Fusion = true;
    }
    IEnumerator Recall()
    {
        isRecalling = true;
        currentPlayer = pc_Biggy;
        pc_Small.DesactivatePlayer();
        cameraController.SwitchPlayer(pc_Biggy.transform);

        while (!pc_Biggy.isCurrentPlayer) { yield return null; }

        RecallSmall();
        isRecalling = false;
    }

    private void SwitchPlayer()
    {
        if (pc_Biggy.isCurrentPlayer)
        {
            currentPlayer = pc_Small;
            pc_Biggy.DesactivatePlayer();
            if (small.isFollowing)
            {
                small.isFollowing = false;
                pc_Small.StartCoroutine("WaitUntilIsGrounded");
            }
            cameraController.SwitchPlayer(pc_Small.transform);
        }
        else
        {
            currentPlayer = pc_Biggy;
            pc_Small.DesactivatePlayer();
            cameraController.SwitchPlayer(pc_Biggy.transform);
        }
    }
}
