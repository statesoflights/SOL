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
	public bool isDragging;
	public int jumpForce;

    public int playerPosition;
    public int verticalPace;

	private CapsuleCollider capsule;

	//Retourne le rayon du capsule collider du perso, qui permettra de faire un sphereCast pour savoir si le perso peut changer de plan
	public float Radius
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
    }

	void Start()
	{
		canSwitch = true;
		isLookingRight = true;
		isMovingVertically = false;
		isGrounded = false;
		isDragging = false;


		if (jumpForce == 0)
		{
			jumpForce = 300;
		}
		
		verticalPace = 5;
		playerPosition = (int)(transform.position.z / verticalPace);
	}

    void FixedUpdate()
    {
        if (isCurrentPlayer)
        {
            if (!isMovingVertically)
            {
                if (!isDragging)
                {
                    if (!isLookingRight && Input.GetAxis("Horizontal") > 0.01) isLookingRight = true;
                    else if (Input.GetAxis("Horizontal") < -0.01) isLookingRight = false;
                }

                UpdateMovement();

                if (Input.GetAxis("Vertical") != 0 && isGrounded)
                    VerticalMoveStart();
            }
            UpdateAnimator();
        }
    }

    private void UpdateMovement()
    {
        RaycastHit hit;
        Vector3 legposition = transform.position;
        legposition.y -= GetComponent<Collider>().bounds.extents.y;

        if (Physics.Raycast(legposition, Vector3.right * Input.GetAxis("Horizontal"), out hit, GetComponent<Collider>().bounds.extents.x + 0.1F) && hit.collider.tag == "Wall" && !hit.collider.isTrigger)
        {
            GetComponent<Rigidbody>().velocity = Vector3.up * GetComponent<Rigidbody>().velocity.y;
        }
        else
            GetComponent<Rigidbody>().velocity = new Vector3(Input.GetAxis("Horizontal") * 5, GetComponent<Rigidbody>().velocity.y);


        if (Input.GetButton("Jump"))
        {
			Debug.Log ("Input.GetButton(Jump) : " + Input.GetButton ("Jump"));
			Debug.Log ("isGrounded : " + isGrounded);
			if (isGrounded)
			{
				GetComponent<Rigidbody>().AddForce(0, jumpForce, 0);
				isGrounded = false;
				canSwitch = false;
			}
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

        GetComponent<Rigidbody>().velocity = Vector3.zero;
        Vector3 gotoPosition = transform.position;
        gotoPosition.z = (playerPosition * verticalPace);

        canSwitch = false;
        isMovingVertically = true;
        StopCoroutine("VerticalGoto");
        StartCoroutine("VerticalGoto", gotoPosition);
    }


	//Retourne true si le perso peut se déplacer verticalement
    private bool CanMoveVertically(Vector3 rayDirection)
    {
        RaycastHit hit;
        Vector3 p1 = transform.position + (2.5f * GetComponent<CapsuleCollider>().radius * rayDirection);
		if (Physics.SphereCast(p1, Radius, rayDirection, out hit, verticalPace))
        {
			if (hit.collider.tag == "Wall" )
            {
                return false;
			}
		}
		if(!Physics.Raycast(transform.position, -Vector3.up, GetComponent<Collider>().bounds.extents.y +0.1F))
		   return false;
        return true;
    }

    IEnumerator VerticalGoto(Vector3 target)
    {
        GetComponent<Rigidbody>().isKinematic = true;
        while (Vector3.Distance(transform.position, target) > 0.05f)
        {
            transform.position = Vector3.Lerp(transform.position, target, 4 * Time.deltaTime / Vector3.Distance(transform.position, target));
            yield return null;
        }
        GetComponent<Rigidbody>().isKinematic = false;
        isMovingVertically = false;
        canSwitch = true;
    }


    public void ActivatePlayer()
	{
		GetComponent<Rigidbody>().isKinematic = false;
		GetComponent<Rigidbody>().useGravity = true;
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        isCurrentPlayer = true;
    }

    public void DesactivatePlayer()
    {
        isCurrentPlayer = false;
        animator.SetFloat("HorizontalSpeed", 0F);

        //reset the movedirection of the player when idle
        GetComponent<Rigidbody>().velocity = Vector3.zero;
    }

    void OnCollisionEnter(Collision collision)
    {
//        if (Physics.Raycast(transform.position, -Vector3.up, GetComponent<Collider>().bounds.extents.y + 0.6F) &&
//            (collision.gameObject.tag == "Floor" ||collision.gameObject.tag == "Wall"))
		Debug.DrawRay (transform.position, -Vector3.up);
		Debug.Log ("DrawRay");
		Debug.Log ("Radius : " + Radius);
		Debug.Log ("GetComponent<Collider>().bounds.extents.y : " + GetComponent<Collider>().bounds.extents.y);
		//if (Physics.SphereCast(new Ray(transform.position, -Vector3.up), Radius, GetComponent<Collider>().bounds.extents.y + 800F))
		if (Physics.Raycast(transform.position, -Vector3.up, GetComponent<Collider>().bounds.extents.y + 0.1F))
		{
			Debug.Log ("1er if");
			if (collision.gameObject.tag == "Floor" ||collision.gameObject.tag == "Wall")
			{
				isGrounded = true;
				canSwitch = true;
				Debug.Log ("isGrounded : " + isGrounded);
			}
            
        }
	}

	void OnCollisionExit(Collision collisionInfo)
    {
        if (collisionInfo.gameObject.tag == "Floor")
		{
			isGrounded = false;
		}
    }
}