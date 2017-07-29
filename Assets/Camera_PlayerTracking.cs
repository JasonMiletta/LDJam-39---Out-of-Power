using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_PlayerTracking : MonoBehaviour {
    public float smoothTime = 0.3f;

    private GameObject player;
    private Vector3 velocity = Vector3.zero;

	// Use this for initialization
	void Start () {
        player = GameObject.Find("Player");
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (player != null)
        {
            Vector3 targetPosition = player.transform.position;
            targetPosition.y = this.transform.position.y;
            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
        }
    }
}
