using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeatGunProjectile : MonoBehaviour
{
    public float lifespan;
    public float lifetime;
    // Start is called before the first frame update
    void Start()
    {
        lifetime = 0;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (lifetime >= lifespan) {
            Destroy(transform.gameObject);
        }
        lifetime += Time.fixedDeltaTime;
    }

    void OnCollisionEnter2D(Collision2D collision) {
        // if (collision.gameObject.tag == "Enemy") {
        //     Thermodynamics thermals = collision.gameObject.GetComponent<Thermodynamics>();
        //     thermals.temperature;
        // }
        //Camera.main.GetComponent<CameraMovement>().LightFlash(transform.position, 0.1f, .2f, Color.white);
        Destroy(transform.gameObject, .1f);
    }
}
