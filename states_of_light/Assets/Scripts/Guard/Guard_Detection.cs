﻿using UnityEngine;
using System.Collections;

public class Guard_Detection : MonoBehaviour {

	private Animator animator;

    public bool isDetectingPlayer;
    public bool isActive;
    SpriteRenderer sprite;
    public bool hasSeenShadow;
    public Animator text_animator;

	// Use this for initialization
	void Start () {
        sprite = GetComponent<SpriteRenderer>();
        isActive = true;
		hasSeenShadow = false;
		animator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        if (isDetectingPlayer)
            sprite.color = Color.red;
        else
            sprite.color = Color.white;

        if (hasSeenShadow)
        {
            animator.SetBool("seenShadow", true);
            if (text_animator)
                text_animator.SetBool("seenShadow", true);
        }
	}

    public void PlayerDetected()
    {
        isDetectingPlayer = true;
    }

    public void PlayerNotDetected()
    {
        isDetectingPlayer = false;
    }
}
