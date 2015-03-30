using UnityEngine;
using System.Collections;

public class Biggy : MonoBehaviour {

    public GameObject playerSmall;
	public bool Fusion;

	private Animator animator;
    private PlayerController pc;

    void Awake()
    {
        pc = GetComponent<PlayerController>();
		animator = GetComponent<Animator>();
		Fusion = true;
    }
	void Start()
	{
		pc.jumpForce = 350;
	}

    void FixedUpdate()
    {
        if (!playerSmall.GetComponent<Small>().isFollowing &&
            Input.GetButtonDown("RecallSmallPlayer") && 
            pc.isCurrentPlayer && 
            pc.isGrounded)
            RecallSmall();

        if (Input.GetKey(KeyCode.LeftShift) && pc.isGrounded) DragObject();
        else pc.isDragging = false;

		animator.SetBool("Fusion", Fusion); //Utilisé pour changer l'affichage de Cello : avec ou sans Zac
    }

    private void RecallSmall()
    {
        playerSmall.GetComponent<Small>().StartFollowPlayerAnim();
    }

    private void DragObject()
    {
        RaycastHit hit;
        if (pc.isLookingRight && Physics.Raycast(transform.position, transform.right, out hit, GetComponent<Collider>().bounds.extents.x + 1))
        {
            if (hit.collider.GetComponent<DragableStone>() && hit.collider.GetComponent<DragableStone>().canBeDragged)
            {
                Vector3 temp_pos = hit.collider.transform.position;
                temp_pos.x = transform.position.x;
                temp_pos.x += GetComponent<Collider>().bounds.extents.x + hit.collider.bounds.extents.x + 0.3F;

                hit.collider.GetComponent<DragableStone>().IsDragged(temp_pos);
                pc.isDragging = true;
            }
        }
        else
            if (!pc.isLookingRight && Physics.Raycast(transform.position, -transform.right, out hit, GetComponent<Collider>().bounds.extents.x + 0.3f))
            {
                if (hit.collider.GetComponent<DragableStone>() && hit.collider.GetComponent<DragableStone>().canBeDragged)
                {
                    Vector3 temp_pos = hit.collider.transform.position;
                    temp_pos.x = transform.position.x;
                    temp_pos.x -= GetComponent<Collider>().bounds.extents.x + hit.collider.bounds.extents.x + 0.2F;

                    hit.collider.GetComponent<DragableStone>().IsDragged(temp_pos);
                    pc.isDragging = true;
                }
            }
    }
}
