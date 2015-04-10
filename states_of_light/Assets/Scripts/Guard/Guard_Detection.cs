using UnityEngine;
using System.Collections;

public class Guard_Detection : MonoBehaviour {

    bool isDetectingPlayer;
    public bool isActive;
    SpriteRenderer sprite;

	// Use this for initialization
	void Start () {
        sprite = GetComponent<SpriteRenderer>();
        isActive = true;
	}
	
	// Update is called once per frame
	void Update () {
        if (isDetectingPlayer)
            sprite.color = Color.red;
        else
            sprite.color = Color.white;
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
