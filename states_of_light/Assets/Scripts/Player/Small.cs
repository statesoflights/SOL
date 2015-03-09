using UnityEngine;
using System.Collections;

public class Small : MonoBehaviour {

    public GameObject playerBiggy;

    private PlayerController pc;
    public bool isFollowing;
    private Vector3 posFollow;

    void Awake()
    {
        pc = GetComponent<PlayerController>();
    }

	void Start () 
    {
        pc.spacebetweenPlayers = -0.3F;
        InitPosition();
	}

	void Update () 
    {
        if (isFollowing)
            FollowPlayer();
	}

    void InitPosition()
    {
        transform.position = new Vector3(transform.position.x,
            transform.position.y,
            pc.spacebetweenPlayers + (pc.playerPosition * pc.verticalPace));

        StartFollowPlayerAnim();
    }

    public void StartFollowPlayerAnim()
    {
        isFollowing = true;
        //rigidbody.isKinematic = true;
        //StopCoroutine("AnimGoToPlayerPos");
        //StartCoroutine("AnimGoToPlayerPos", playerBiggy.transform.position);
        FollowPlayer();
    }

    IEnumerator AnimGoToPlayerPos(Vector3 target)
    {
        target.y = transform.position.y;
        while (Vector3.Distance(transform.position, target) > 0.05f)
        {
            transform.position = Vector3.Lerp(transform.position, target, 4 * Time.deltaTime / Vector3.Distance(transform.position, target));
            yield return null;
        }

        isFollowing = true;

        //Only After the Recall animation finishes we can move the player 1
        playerBiggy.GetComponent<PlayerController>().ActivatePlayer();
    }

    private void FollowPlayer()
    {
        posFollow = playerBiggy.transform.position;
        posFollow.y = playerBiggy.transform.position.y + 
            (playerBiggy.GetComponent<CapsuleCollider>().height / 2) + 
            (GetComponent<CapsuleCollider>().height * transform.localScale.y / 2);

        transform.position = posFollow;
    }
}
