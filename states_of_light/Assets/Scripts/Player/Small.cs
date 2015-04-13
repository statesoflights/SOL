using UnityEngine;
using System.Collections;

public class Small : MonoBehaviour {

    public Transform playerBiggy;

    public PlayerController pc;
    public bool canFollow;
    public bool isFollowing;
    private Vector3 posFollow;
	private SpriteRenderer sr;
    private Collider collider;

    void Awake()
    {
        pc = GetComponent<PlayerController>();
		sr = GetComponent<SpriteRenderer> ();
        collider = GetComponent<Collider>();
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

        StopCoroutine("FollowPlayer");
        StartCoroutine("FollowPlayer");
    }

    IEnumerator FollowPlayer()
    {
        PlayerController pc_Biggy = playerBiggy.GetComponent<PlayerController>();
        Collider collider_Biggy = playerBiggy.GetComponent<Collider>();
        while (isFollowing)
        {
            pc.playerPosition = pc_Biggy.playerPosition;
            posFollow = playerBiggy.position;
            posFollow.y = playerBiggy.position.y +
                collider_Biggy.bounds.extents.y -
                collider.bounds.extents.y;

            //		posFollow.y = playerBiggy.position.y + 
            //			(playerBiggy.GetComponent<CapsuleCollider>().height / 2) + 
            //				(GetComponent<CapsuleCollider>().height * transform.localScale.y / 2);

            transform.position = posFollow;

            if (pc.isLookingRight != pc_Biggy.isLookingRight)
            {
                pc.isLookingRight = pc_Biggy.isLookingRight;
                transform.localScale = playerBiggy.lossyScale;
            }
            yield return null;
        }
    }
}
