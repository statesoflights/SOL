using UnityEngine;
using System.Collections;

public class DragableStone : MonoBehaviour {

    public float max_x;
    public float min_x;

    public bool canBeDragged;
    public Vector3 animPosition;

    private Animator anim;


    void Awake()
    {

        canBeDragged = true;

        anim = GetComponent<Animator>();
        anim.enabled = false;
    }

    void FixedUpdate()
    {
        //Debug.Log(max_x - transform.position.x);
        if (transform.position.x >= max_x)
            StartAnim();
    }

    public void IsDragged(Vector3 gotoPos)
    {
        rigidbody.isKinematic = true;
        if (gotoPos.x <= max_x+0.1F && gotoPos.x >= min_x)
            transform.position = gotoPos;
    }

    private void StartAnim()
    {
        anim.enabled = true;
        canBeDragged = false;
        Debug.Log("Started Anim");
        anim.SetBool("Fall",true);
    }
    
}
