﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    public float movementSpeed = 1.0f;
    public float maxDashTime = 0.25f;
    public float dashSpeed = 2.0f;
    public float dashStoppingSpeed = 0.1f;
    public bool isDashing = false;

    private bool isMovementLocked = false;
    private float currentDashTime;
    private Vector3 stashedDashVector;

	// Use this for initialization
	void Start () {
        currentDashTime = maxDashTime;
	}
	
	// Update is called once per frame
	void Update ()
    {
        Debug.DrawRay(transform.position, transform.forward, Color.red);

        chargeLoop();
        lookLoop();
        if (!isMovementLocked)
        {
            movementLoop();
        }
    }

    private void lookLoop()
    {
        var ray = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Debug.DrawRay(Camera.main.transform.position, ray, Color.yellow);
        
        transform.LookAt(ray, new Vector3(0, 0, -1));
        transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
    }

    private void movementLoop()
    {
        var x = Input.GetAxis("Horizontal") * Time.deltaTime * movementSpeed;
        var z = Input.GetAxis("Vertical") * Time.deltaTime * movementSpeed;
        transform.Translate(x, 0, z, Space.World);
    }

    private void chargeLoop()
    {
        //Check if we're still dashing
        if (currentDashTime < maxDashTime)
        {
            Debug.Log("Dashing");
            isDashing = true;
            currentDashTime += Time.deltaTime;
            transform.Translate(stashedDashVector * Time.deltaTime * dashSpeed, Space.World);
            isMovementLocked = true;
        }
        else
        {
            isDashing = false;
            if(Input.GetButton("Fire1") != true)
            {
                isMovementLocked = false;
            }
            
            if (Input.GetButtonDown("Fire1"))
            {
                isMovementLocked = true;
                startChargeParticles();

            } 
            else if (Input.GetButtonUp("Fire1"))
            {
                stopChargeParticles();
                currentDashTime = 0.0f;
                stashedDashVector = transform.forward;
            }
        }
    }

    private void startChargeParticles()
    {
        var chargeParticle = GetComponentInChildren<ParticleSystem>();
        if (chargeParticle != null)
        {
            chargeParticle.Play();
        }
    }

    private void stopChargeParticles()
    {
        var chargeParticle = GetComponentInChildren<ParticleSystem>();
        if (chargeParticle != null)
        {
            chargeParticle.Stop();
        }
    }

}
