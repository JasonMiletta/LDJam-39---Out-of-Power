using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

    public GameObject particleBurst;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        PlayerMovement player = other.gameObject.GetComponent<PlayerMovement>();
        ParticleSystem particles = other.GetComponent<ParticleSystem>();
        if (particles != null)
        {
            Debug.Log("Deflected!");
            player = other.gameObject.GetComponentInParent<PlayerMovement>();
            player.takeShieldPower(20.0f);
            destroySelf();
        }
        else if (player != null)
        {
            Debug.Log("Hit!");
            player.takeDamage(10.0f);
            destroySelf();
        }
    }

    private void destroySelf()
    {
        if (particleBurst != null)
        {
            var newParticleBurst = Instantiate(particleBurst, transform.position, transform.rotation);
            Destroy(newParticleBurst, 5.0f);
        }
        Destroy(this.gameObject);
    }
}
