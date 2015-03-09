using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    private Animator animator;

    public bool isLookingRight;
    public bool isMovingVertically;
    private bool directionUp;

    public bool isGrounded;
    public bool isCurrentPlayer;

    public int playerPosition;
    public float spacebetweenPlayers;
    public int verticalPace;

    public Vector3 moveDirection;

	private CapsuleCollider capsule;

	public float Radius //test git
	{
		get
		{
			Vector3 scale = transform.lossyScale;
			return capsule.radius * Mathf.Max(scale.x, scale.y, scale.z);
		}
	}

    void Awake()
    {
		capsule = GetComponent<CapsuleCollider> ();
        animator = GetComponent<Animator>();
        isLookingRight = true;
        isMovingVertically = false;
        isGrounded = false;

        moveDirection = Vector3.zero;

        playerPosition = 1;
        verticalPace = 5;
        spacebetweenPlayers = 0;
    }

    void FixedUpdate()
    {
        if (isCurrentPlayer)
        {
            if (!isMovingVertically)
            {
                if (!isLookingRight && Input.GetAxis("Horizontal") > 0.01) isLookingRight = true;
                else if (Input.GetAxis("Horizontal") < -0.01) isLookingRight = false;

                UpdateMovement();

                if (Input.GetButtonDown("Vertical") && isGrounded)
                    VerticalMoveStart();
            }
            UpdateAnimator();
        }
    }

    private void UpdateMovement()
    {
        moveDirection.x = Input.GetAxis("Horizontal") / 10;
        if (Input.GetButton("Jump") && isGrounded)
            moveDirection.y = 0.1F;

        transform.position += moveDirection;
    }
    private void UpdateAnimator()
    {
        animator.SetFloat("HorizontalSpeed", Input.GetAxis("Horizontal"));
        animator.SetFloat("VerticalSpeed", Input.GetAxis("Vertical"));
        animator.SetBool("isLookingRight", isLookingRight);
        animator.SetBool("directionUp", directionUp);
        animator.SetBool("isMovingVertically", isMovingVertically);
    }

    private bool CanMoveVertically(Vector3 rayDirection)
    {
        RaycastHit hit;
        Vector3 p1 = transform.position + (2.5f * GetComponent<CapsuleCollider>().radius * rayDirection);
		if (Physics.SphereCast(p1, Radius, rayDirection, out hit, 6f))
        {
            if (hit.collider.tag == "Wall")
            {
                return false;
            }
        }
        return true;
    }
    private void VerticalMoveStart()
    {
        if (Input.GetAxis("Vertical") > 0 && playerPosition + 1 <= 2 && CanMoveVertically(transform.forward))
        {
            playerPosition += 1;
            directionUp = true;
        }
        else if (Input.GetAxis("Vertical") < 0 && playerPosition - 1 >= 0 && CanMoveVertically(-transform.forward))
        {
            playerPosition -= 1;
            directionUp = false;
        }
        else return;

        Vector3 gotoPosition = transform.position;
        gotoPosition.z = (playerPosition * verticalPace) + spacebetweenPlayers;

        isMovingVertically = true;
        StopCoroutine("VerticalGoto");
        StartCoroutine("VerticalGoto", gotoPosition);
    }
    IEnumerator VerticalGoto(Vector3 target)
    {
        while (Vector3.Distance(transform.position, target) > 0.05f)
        {
            transform.position = Vector3.Lerp(transform.position, target, 4 * Time.deltaTime / Vector3.Distance(transform.position, target));
            yield return null;
        }
        isMovingVertically = false;
    }


    public void ActivatePlayer()
    {
        isCurrentPlayer = true;
    }

    public void DesactivatePlayer()
    {
        isCurrentPlayer = false;
        animator.SetFloat("HorizontalSpeed", 0F);

        //reset the movedirection of the player when idle
        moveDirection = new Vector3(0, 0, 0);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Floor")
        {
            isGrounded = true;

            //reset the direction of the player after each jump
            moveDirection = Vector3.zero;

            // Only when the small player touch the floor can we replay the follow animation
            //if (playerId == 1 && !isFollowing)
            //    gc.isPlayFollowAnim = false;
        }
    }
    void OnCollisionExit(Collision collisionInfo)
    {
        if (collisionInfo.gameObject.tag == "Floor") isGrounded = false;
    }
}