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
        anim = GetComponent<Animator>();
    }
    void Start()
    {
        canBeDragged = true;
        anim.enabled = false;

    }
    public void IsDragged(Vector3 gotoPos)
    {
        Debug.Log(canBeDragged);
        GetComponent<Rigidbody>().isKinematic = true;
        if (gotoPos.x <= max_x+0.1F && gotoPos.x >= min_x)
            transform.position = gotoPos;
        if (haveAnim && transform.position.x >= max_x-0.1F)
        { 
            StartAnim();
            Debug.Log(transform.position.x);
        }
    }

    private void StartAnim()
    {
        anim.enabled = true;
        canBeDragged = false;
        //anim.SetBool("Trigger",true);
    }
    
}
