using UnityEngine;
using System.Collections;

public class DragableStone : MonoBehaviour {

    public float max_x;
    public float min_x;

    public bool canBeDragged;
    public bool haveAnim;
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
        if (haveAnim && transform.position.x >= max_x)
            StartAnim();
    }

    public void IsDragged(Vector3 gotoPos)
    {
        GetComponent<Rigidbody>().isKinematic = true;
        if (gotoPos.x <= max_x+0.1F && gotoPos.x >= min_x)
            transform.position = gotoPos;
    }

    private void StartAnim()
    {
        anim.enabled = true;
        canBeDragged = false;
        anim.SetBool("Trigger",true);
    }
    
}
