using UnityEngine;
using System.Collections;

public class Biggy : MonoBehaviour {

    public GameObject playerSmall;
	public bool Fusion;

	private Animator animator;
    private PlayerController pc;

    public float dragDistance;

    void Awake()
    {
        pc = GetComponent<PlayerController>();
		animator = GetComponent<Animator>();
		Fusion = true;
    }
	void Start()
	{
		pc.jumpForce = 200;
        pc.speed = 3;
	}

    void FixedUpdate()
    {
        if (pc.isGrounded)
        {
            if (!playerSmall.GetComponent<Small>().isFollowing &&
                Input.GetButtonDown("RecallSmallPlayer") &&
                pc.isCurrentPlayer)
                RecallSmall();

            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                StopCoroutine(DragObject());
                StartCoroutine(DragObject());
            }
        }

		if (!pc.isCurrentPlayer &&!pc.isMovingVertically) 
			Fusion = false;
		
		animator.SetBool ("Fusion", Fusion);
	}

    private void RecallSmall()
    {
        playerSmall.GetComponent<Small>().StartFollowPlayerAnim();
		Fusion = true;
    }

    //private void DragObject()
    //{
    //    RaycastHit hit;
    //    if (pc.isLookingRight && Physics.Raycast(transform.position, transform.right, out hit, GetComponent<Collider>().bounds.extents.x + 1))
    //    {
    //        if (hit.collider.GetComponent<DragableStone>() && hit.collider.GetComponent<DragableStone>().canBeDragged)
    //        {
    //            Vector3 temp_pos = hit.collider.transform.position;
    //            temp_pos.x = transform.position.x;
    //            temp_pos.x += GetComponent<Collider>().bounds.extents.x + hit.collider.bounds.extents.x + 0.3F;

    //            hit.collider.GetComponent<DragableStone>().IsDragged(temp_pos);
    //            pc.isDragging = true;
    //        }
    //    }
    //    else
    //        if (!pc.isLookingRight && Physics.Raycast(transform.position, -transform.right, out hit, GetComponent<Collider>().bounds.extents.x + 0.3f))
    //        {
    //            if (hit.collider.GetComponent<DragableStone>() && hit.collider.GetComponent<DragableStone>().canBeDragged)
    //            {
    //                Vector3 temp_pos = hit.collider.transform.position;
    //                temp_pos.x = transform.position.x;
    //                temp_pos.x -= GetComponent<Collider>().bounds.extents.x + hit.collider.bounds.extents.x + 0.2F;

    //                hit.collider.GetComponent<DragableStone>().IsDragged(temp_pos);
    //                pc.isDragging = true;
    //            }
    //        }
    //}
    IEnumerator DragObject()
    {

        RaycastHit hit;
        float minDistance;
        bool isSeeingObject = true;
        pc.isDragging = true;

        while (Input.GetKey(KeyCode.LeftShift) && pc.isGrounded && isSeeingObject)
        {
            if (pc.isLookingRight && Physics.Raycast(transform.position, transform.right, out hit, GetComponent<Collider>().bounds.extents.x + 1))
            {
                if (hit.collider.GetComponent<DragableStone>() && hit.collider.GetComponent<DragableStone>().canBeDragged)
                {
                    if (dragDistance == 0)
                        dragDistance = hit.collider.transform.position.x - transform.position.x;

                    minDistance = GetComponent<Collider>().bounds.extents.x + hit.collider.bounds.extents.x + 0.15F;
                    if (dragDistance <= minDistance)
                        dragDistance = minDistance;

                    Vector3 temp_pos = hit.collider.transform.position;
                    temp_pos.x = transform.position.x + dragDistance;
                    hit.collider.GetComponent<DragableStone>().IsDragged(temp_pos);
                }
            }
            else
                if (!pc.isLookingRight && Physics.Raycast(transform.position, -transform.right, out hit, GetComponent<Collider>().bounds.extents.x + 1f))
                {
                    if (hit.collider.GetComponent<DragableStone>() && hit.collider.GetComponent<DragableStone>().canBeDragged)
                    {
                        if (dragDistance == 0)
                            dragDistance = transform.position.x - hit.collider.transform.position.x;

                        minDistance = GetComponent<Collider>().bounds.extents.x + hit.collider.bounds.extents.x + 0.2F;
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

        pc.isDragging = false;
        dragDistance = 0;
    }
}
