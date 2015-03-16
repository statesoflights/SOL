using UnityEngine;
using System.Collections;

public class Small : MonoBehaviour {

    public GameObject playerBiggy;

    private PlayerController pc;
    public bool canFollow;
    public bool isFollowing;
    private Vector3 posFollow;

    void Awake()
    {
        pc = GetComponent<PlayerController>();
    }

	void Start () 
    {
        Init();
	}

	void FixedUpdate () 
    {
        if (isFollowing)
            FollowPlayer();
	}

    void Init()
    {
        //transform.position = new Vector3(transform.position.x,
        //    transform.position.y,
        //    pc.spacebetweenPlayers + (pc.playerPosition * pc.verticalPace));

        StartFollowPlayerAnim();
    }

    public void StartFollowPlayerAnim()
	{
		pc.rigidbody.isKinematic = true;
        pc.isGrounded = false;
		pc.canSwitch = false;
        isFollowing = true;

        FollowPlayer();
    }

    private void FollowPlayer()
    {
		pc.playerPosition = playerBiggy.GetComponent<PlayerController> ().playerPosition;
        posFollow = playerBiggy.transform.position;
        posFollow.y = playerBiggy.transform.position.y + 
            (playerBiggy.GetComponent<CapsuleCollider>().height / 2) + 
            (GetComponent<CapsuleCollider>().height * transform.localScale.y / 2);

        transform.position = posFollow;
    }
}
