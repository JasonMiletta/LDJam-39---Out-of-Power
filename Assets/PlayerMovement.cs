using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    public float movementSpeed = 1.0f;

    private bool isMovementLocked = false;
    
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        look();
        if (isMovementLocked)
        {
            movement();
        }
    }

    private void look()
    {
        var ray = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Debug.DrawRay(Camera.main.transform.position, ray, Color.yellow);
        
        transform.LookAt(ray, new Vector3(0, 0, -1));
        transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
    }

    private void movement()
    {
        var x = Input.GetAxis("Horizontal") * Time.deltaTime * movementSpeed;
        var z = Input.GetAxis("Vertical") * Time.deltaTime * movementSpeed;

        transform.Translate(x, 0, z, Space.World);
    }
}
