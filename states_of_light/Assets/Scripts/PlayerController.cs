using UnityEngine;
using System.Collections;

[RequireComponent (typeof(Animator),typeof(Rigidbody),typeof(Collider))]
public class PlayerController : MonoBehaviour
{

    private Animator animator;
    private Rigidbody body;

    private bool isLookingRight;
    private bool isMovingVertically;
    private bool directionUp;

    public bool isGrounded;
    public bool isCurrentPlayer;

    public int playerPosition;
    public int playerId;

    public Vector3 moveDirection = Vector3.zero;

    void Awake()
    {
        animator = GetComponent<Animator>();
        body = GetComponent<Rigidbody>();

        isLookingRight = true;
        isMovingVertically = false;
        isGrounded = false;

        moveDirection = new Vector3(0, 0, 0);

        playerPosition = 0;
    }

    void FixedUpdate()
    {
        if (!isLookingRight && Input.GetAxis("Horizontal") > 0.01) isLookingRight = true;
        else if (Input.GetAxis("Horizontal") < -0.01) isLookingRight = false;
        
        if (isCurrentPlayer && !isMovingVertically) UpdateMovement();

        UpdateAnimator();
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

    private void FollowPlayer()
    {
    }

    public void ActivatePlayer()
    {
        isCurrentPlayer = true;
        //rigidbody.isKinematic = false;
        //collider.enabled = true;
    }
    public void DesactivatePlayer()
    {
        isCurrentPlayer = false;
        //rigidbody.isKinematic = true;
        //collider.enabled = false;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Floor")
        {
            isGrounded = true;
            moveDirection = new Vector3(0, 0, 0);
        }

    }
    void OnCollisionExit(Collision collisionInfo)
    {
        if (collisionInfo.gameObject.tag == "Floor") isGrounded = false;
    }
    
}
