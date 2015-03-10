using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    private Animator animator;

    public bool isLookingRight;
    public bool isMovingVertically;
    private bool directionUp;

    public bool isGrounded;
    public bool isCurrentPlayer;
    public bool canSwitch;

    public int playerPosition;
    public float spacebetweenPlayers;
    public int verticalPace;

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

        canSwitch = true;
        isLookingRight = true;
        isMovingVertically = false;
        isGrounded = false;

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

                if (Input.GetAxis("Vertical")!= 0 && isGrounded)
                    VerticalMoveStart();
            }
            UpdateAnimator();
        }
    }

    private void UpdateMovement()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.right * Input.GetAxis("Horizontal"), out hit, collider.bounds.extents.x + 0.1F) && hit.collider.tag == "Wall")
        {           
            rigidbody.velocity = Vector3.up * rigidbody.velocity.y;
        }
        else
            rigidbody.velocity = new Vector3(Input.GetAxis("Horizontal") * 5, rigidbody.velocity.y);

        if (Input.GetButton("Jump") && isGrounded)
        {
            rigidbody.AddForce(0, 300, 0);
            isGrounded = false;
            canSwitch = false;
        }
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

        rigidbody.velocity = Vector3.zero;
        Vector3 gotoPosition = transform.position;
        gotoPosition.z = (playerPosition * verticalPace) + spacebetweenPlayers;

        canSwitch = false;
        isMovingVertically = true;
        StopCoroutine("VerticalGoto");
        StartCoroutine("VerticalGoto", gotoPosition);
    }

    IEnumerator VerticalGoto(Vector3 target)
    {
        rigidbody.isKinematic = true;
        while (Vector3.Distance(transform.position, target) > 0.05f)
        {
            transform.position = Vector3.Lerp(transform.position, target, 4 * Time.deltaTime / Vector3.Distance(transform.position, target));
            yield return null;
        }
        rigidbody.isKinematic = false;
        isMovingVertically = false;
        canSwitch = true;

    }


    public void ActivatePlayer()
    {
        rigidbody.velocity = Vector3.zero;
        isCurrentPlayer = true;
    }

    public void DesactivatePlayer()
    {
        isCurrentPlayer = false;
        animator.SetFloat("HorizontalSpeed", 0F);

        //reset the movedirection of the player when idle
        rigidbody.velocity = Vector3.zero;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (Physics.Raycast(transform.position, -Vector3.up, collider.bounds.extents.y) &&
            (collision.gameObject.tag == "Floor" ||collision.gameObject.tag == "Wall"))
        {
            Debug.Log("Here");
            isGrounded = true;
            canSwitch = true;
        }
	}

	void OnCollisionExit(Collision collisionInfo)
    {
        if (collisionInfo.gameObject.tag == "Floor") isGrounded = false;
    }
}