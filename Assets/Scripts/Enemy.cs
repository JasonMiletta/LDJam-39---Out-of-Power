using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    public float movementSpeed = 1.0f;
    public float shootingSpeed = 1.0f;
    public bool canShoot = true;
    public GameObject projectile;
    public Transform gunBarrel;

    private GameObject target;
    private GameManager gameManager;
    private bool isAlive = true;
    private float shootingCooldown = 0.0f;

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
            lookAtTarget();
            if (canShoot)
            {
                shootAtTarget();
            }
        }
	}

    private void moveToTarget()
    {
        var movementVector = target.transform.position - transform.position;
        movementVector.Normalize();
        transform.Translate(movementVector * Time.deltaTime * movementSpeed, Space.World);
    }

    private void lookAtTarget()
    {
        transform.LookAt(target.transform.position, new Vector3(0, 0, -1));
        transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
    }

    private void shootAtTarget()
    {
        if (shootingCooldown < shootingSpeed)
        {
            shootingCooldown += Time.deltaTime;
        }
        else if (projectile != null && gunBarrel != null)
        {
            GameObject newProjectile = Instantiate(projectile, gunBarrel.position, gunBarrel.rotation);
            Vector3 shootVector = target.transform.position - transform.position;
            shootVector.Normalize();
            shootVector.y = 0;
            newProjectile.GetComponent<Rigidbody>().AddForce( shootVector * 20.0f, ForceMode.Impulse);
            Destroy(newProjectile, 10.0f);
            shootingCooldown = 0.0f;
        }
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
