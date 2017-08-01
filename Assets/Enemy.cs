using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    public float movementSpeed = 1.0f;

    private GameObject target;
    private GameManager gameManager;
    private bool isAlive = true;

	// Use this for initialization
	void Start () {
        target = GameObject.Find("Player");
        var playerContainer = FindObjectOfType<GameManager>();
        if(playerContainer != null)
        {
            gameManager = playerContainer;
        }
    }
	
	// Update is called once per frame
	void Update () {
        if (isAlive)
        {
            moveToTarget();
        }
	}

    private void moveToTarget()
    {
        var movementVector = target.transform.position - transform.position;
        transform.Translate(movementVector * Time.deltaTime * movementSpeed, Space.World);
    }

    //Oh god this is gruesome huh?
    private void killSelf(Vector3 deathForce)
    {
        givePower();
        isAlive = false;
        var rigidbody = GetComponent<Rigidbody>();
        rigidbody.freezeRotation = false;
        rigidbody.AddForce(deathForce * 20.0f, ForceMode.Impulse);
        Destroy(this.gameObject, 5.0f);
    }

    private void OnCollisionEnter(Collision collision)
    {
        var playerMovement = collision.gameObject.GetComponent<PlayerMovement>();
        if (playerMovement != null)
        {
            if (playerMovement.isDashing)
            {
                //Player was dashing, kill this enemy
                var deathVector = transform.position - collision.transform.position;
                killSelf(deathVector);

            } else
            {
                //Hurt player?
            }
        }
    }

    private void givePower()
    {
        gameManager.currentChargeAmount += 10.0f;
    }
}
