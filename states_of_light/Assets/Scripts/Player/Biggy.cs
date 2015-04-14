using UnityEngine;
using System.Collections;

public class Biggy : MonoBehaviour
{

    public GameObject playerSmall;
    public bool Fusion;

    private Animator animator;
    private PlayerController pc;

    public float dragDistance;
    Trigger_Anim tranim;
    private Collider collider;
    Vector3 destinationpos;

    void Awake()
    {
        pc = GetComponent<PlayerController>();
        animator = GetComponent<Animator>();
        collider = GetComponent<Collider>();
        Fusion = true;
    }
    void Start()
    {
        pc.canJump = false;
        pc.jumpForce = 250;
        pc.speed = 3;
    }

    void FixedUpdate()
    {
        if (pc.isCurrentPlayer && pc.isGrounded)
        {
            //if (!isJumping && isGrounded && !isCLimbing)

            if (!pc.isJumping && !pc.isCLimbing && !pc.isDragging && 
                Input.GetButton("Horizontal") && Input.GetButton("Jump")&&!CanClimbWall())
            {
                pc.rb.AddForce(0, pc.jumpForce, 0);
                pc.canSwitch = false;
                pc.isJumping = true;
            }
            if (!playerSmall.GetComponent<Small>().isFollowing &&
                Input.GetButtonDown("RecallSmallPlayer"))
                RecallSmall();

            if (Input.GetButton("Action") && !pc.isDragging)
            {
                StopCoroutine(DragObject());
                StartCoroutine(DragObject());
            }
            
        }


        if (!pc.isCurrentPlayer && !pc.isMovingVertically)
            Fusion = false;

        animator.SetBool("Fusion", Fusion);
    }

    private void RecallSmall()
    {
        playerSmall.GetComponent<Small>().StartFollowPlayerAnim();
        Fusion = true;
    }

    IEnumerator HitStart(Trigger_Anim target)
    {
        tranim = target;
        animator.SetBool("Hit", true);
        yield return new WaitForSeconds(animator.GetCurrentAnimatorClipInfo(0).Length);
        animator.SetBool("Hit", false);
        
    }

    public void StartAnimTarget()
    {
        tranim.StartAnim();
    }

    bool CanClimbWall()
    {
        Vector3 headPosition = transform.position;
        headPosition.y += collider.bounds.extents.y - 0.1F;
        Vector3 lookDirection = Input.GetAxis("Horizontal") >= 0 ? transform.right : -transform.right;

        RaycastHit hit;
        if (Physics.Raycast(transform.position, lookDirection, out hit, collider.bounds.extents.x + 0.1F))
        {
            if (!Physics.Raycast(headPosition, lookDirection, collider.bounds.extents.x + 0.1F))
            {
                //StopCoroutine("ClimbWall");
                //StartCoroutine("ClimbWall", hit);
                ClimbWall(hit);
                return true;

            }
        }
        return false;
    }

    void ClimbWall(RaycastHit hit)
    {
        pc.isCLimbing = true;
        pc.isGrounded = false;
        GetComponent<SpriteRenderer>().enabled = false;
        transform.GetComponentInChildren<BiggyParent>().StartAnim();

            Vector3 targetSize = hit.collider.GetComponent<BoxCollider>().center + hit.collider.bounds.size;
            destinationpos = transform.position;
            destinationpos.y = hit.collider.transform.position.y + (targetSize.y / 2) + collider.bounds.extents.y;
            destinationpos.x += Input.GetAxis("Horizontal") >= 0 ? collider.bounds.size.x : -collider.bounds.size.x;
    }
    public void ClimbWallEnded()
    {
        transform.position = destinationpos;
        animator.enabled = true; 
        GetComponent<SpriteRenderer>().enabled = true;
        pc.isCLimbing = false;
    }

    IEnumerator DragObject()
    {

        RaycastHit hit;
        float minDistance;
        bool isSeeingObject = true;
        bool pushDirection = pc.isLookingRight;

        while (Input.GetButton("Action") && pc.isGrounded && isSeeingObject && !pc.isMovingVertically)
        {
            if (pushDirection && Physics.Raycast(transform.position, transform.right, out hit, collider.bounds.extents.x + 0.5f))
            {
                UpdateDirection(false);
                if (hit.collider.GetComponent<DragableStone>() && hit.collider.GetComponent<DragableStone>().canBeDragged)
                {
                    pc.isDragging = true;

                    animator.SetBool("isDragging", true);
                    //if(animator.GetCurrentAnimatorStateInfo(0).IsName( "Push_Right_Fusion"))
                    //animator.speed = 1;

                    if (dragDistance == 0)
                        dragDistance = hit.collider.transform.position.x - transform.position.x;

                    minDistance = collider.bounds.extents.x + hit.collider.bounds.extents.x + 0.15F;
                    if (dragDistance <= minDistance)
                        dragDistance = minDistance;

                    Vector3 temp_pos = hit.collider.transform.position;
                    temp_pos.x = transform.position.x + dragDistance;
                    hit.collider.GetComponent<DragableStone>().IsDragged(temp_pos);
                }
            }
            else
                if (!pushDirection && Physics.Raycast(transform.position, -transform.right, out hit, collider.bounds.extents.x + 0.5f))
                {
                    UpdateDirection(true);
                    if (hit.collider.GetComponent<DragableStone>() && hit.collider.GetComponent<DragableStone>().canBeDragged)
                    {
                        pc.isDragging = true;
                        
                        animator.SetBool("isDragging", true);
                        //if(animator.GetCurrentAnimatorStateInfo(0).IsName( "Push_Right_Fusion"))
                            //animator.speed = -1;
                        
                        if (dragDistance == 0)
                            dragDistance = transform.position.x - hit.collider.transform.position.x;

                        minDistance = collider.bounds.extents.x + hit.collider.bounds.extents.x + 0.2F;
                        if (dragDistance <= minDistance)
                            dragDistance = minDistance;

                        Vector3 temp_pos = hit.collider.transform.position;
                        temp_pos.x = transform.position.x - dragDistance;
                        hit.collider.GetComponent<DragableStone>().IsDragged(temp_pos);
                    }
                }
                else
                    isSeeingObject = false;


            yield return null;
        }
        pc.isLookingRight = pushDirection;

        animator.speed = 1;
        animator.SetBool("isDragging", false);
        pc.isDragging = false;
        dragDistance = 0;
    }

    void UpdateDirection(bool inverse)
    {
        if (Input.GetAxis("Horizontal") > 0.01)
        {
            if(!inverse)
                pc.isLookingRight = true;
            else
                pc.isLookingRight = false;

        }
        else if (Input.GetAxis("Horizontal") < -0.01)
        {
            if(!inverse)
                pc.isLookingRight = false;
            else
                pc.isLookingRight = true;
        }
                
    }

    public void moveToEndLevel()
    {
        pc.playerPosition += 1;
        pc.directionUp = true;
        //rb.velocity = Vector3.zero;
        Vector3 gotoPosition = transform.position;
        gotoPosition.z = (pc.playerPosition * pc.verticalPace);
        pc.canSwitch = false;
        pc.isMovingVertically = true;
        pc.StopCoroutine("VerticalGoto");
        pc.StartCoroutine("VerticalGoto", gotoPosition);
    }
}
