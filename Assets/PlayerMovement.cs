using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    public float movementSpeed = 1.0f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        var x = Input.GetAxis("Horizontal") * Time.deltaTime * movementSpeed;
        var z = Input.GetAxis("Vertical") * Time.deltaTime * movementSpeed;

        transform.Translate(x, 0, z);
    }


}
