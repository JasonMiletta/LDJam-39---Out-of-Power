using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    public float movementSpeed = 1.0f;
    public float maxDashTime = 0.25f;
    public float dashSpeed = 2.0f;
    public float dashStoppingSpeed = 0.1f;
    public bool isDashing = false;

    public AudioSource dashAudio;
    public AudioSource chargeAudio;
    public AudioSource absorbAudio;
    public AudioSource hitAudio;

    public float currentChargeAmount = 100.0f;

    private GameManager gameManager;
    private bool isMovementLocked = false;
    private float currentDashTime;
    private Vector3 stashedDashVector;

	// Use this for initialization
	void Start () {
        currentDashTime = maxDashTime;
        gameManager = FindObjectOfType<GameManager>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        Debug.DrawRay(transform.position, transform.forward, Color.red);

        if (Time.timeScale != 0.0f)
        {
            dashLoop();
        }
        lookLoop();
        if (!isMovementLocked)
        {
            movementLoop();
        }
    }

    public void takeDamage(float damageValue)
    {
        gameManager.updateEnergyValue(-damageValue);
        hitAudio.Play();
    }

    public void takeShieldPower(float chargeValue)
    {
        gameManager.updateEnergyValue(chargeValue);
        absorbAudio.Play();
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

    private void dashLoop()
    {
        //Check if we're still dashing
        if (currentDashTime < maxDashTime)
        {
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
        if (chargeParticle != null )
        {
            chargeAudio.Play();
            chargeParticle.Play();
            
        }
        SphereCollider particleCollider = GetComponentInChildren<SphereCollider>();
        if(particleCollider != null)
        {
            particleCollider.enabled = true;
        }
    }

    private void stopChargeParticles()
    {
        var chargeParticle = GetComponentInChildren<ParticleSystem>();
        if (chargeParticle != null)
        {
            chargeAudio.Stop();
            dashAudio.Play();
            chargeParticle.Stop();
        }
        SphereCollider particleCollider = GetComponentInChildren<SphereCollider>();
        if (particleCollider != null)
        {
            particleCollider.enabled = false;
        }
    }

}
