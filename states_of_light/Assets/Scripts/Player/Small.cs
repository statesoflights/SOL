using UnityEngine;
using System.Collections;

public class Small : MonoBehaviour {

    public GameObject playerBiggy;

    private PlayerController pc;
    public bool canFollow;
    public bool isFollowing;
    private Vector3 posFollow;
	private SpriteRenderer sr;

    void Awake()
    {
        pc = GetComponent<PlayerController>();
		sr = GetComponent<SpriteRenderer> ();
    }

	void Start () 
    {
        Init();
	}

	void FixedUpdate () 
    {
        if (isFollowing)
            FollowPlayer();
		if (pc.isCurrentPlayer && (transform.position.z==0 ||transform.position.z==5 ||transform.position.z==10 )) 
		{
			Vector3 temp = transform.position;
			temp.z -=0.2F;
			transform.position = temp;
		}
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
		pc.GetComponent<Rigidbody>().isKinematic = true;
        pc.isGrounded = false;
		pc.canSwitch = false;
        isFollowing = true;
		sr.enabled = false;

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
