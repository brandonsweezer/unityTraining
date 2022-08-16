using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{

    public GameObject playerModel;
    public float moveSpeed;
    public float temperature;
    public Rigidbody2D rb;
    private GameObject deathEffect;
    private GameObject deathFlash;
    private Thermodynamics thermals;
    public GameObject temperatureText;

    // Start is called before the first frame update
    void Start()
    {
        deathFlash = Resources.Load<GameObject>("Enemies/DeathFlash");
        deathEffect = Resources.Load<GameObject>("Enemies/deathEffect");
        if (playerModel == null) {
            playerModel = GameObject.FindGameObjectWithTag("Player");
        }
        if (rb == null) {
            rb = transform.GetComponent<Rigidbody2D>();
        }
        moveSpeed = Random.Range(1.0f, 5.0f);
        thermals = transform.gameObject.GetComponent<Thermodynamics>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 playerPosition = playerModel.transform.position;
        Vector3 direction = new Vector3(playerPosition.x - transform.position.x, playerPosition.y - transform.position.y, 0).normalized;

        rb.velocity = direction * moveSpeed;

        if (thermals.temperature >= thermals.maxTemp) {
            Kill();
        }
        
    }

    public void Kill()
    {
        GameObject explosion = Instantiate(deathEffect, transform.position, Quaternion.identity);
        Destroy(temperatureText);
        Destroy(transform.gameObject);
        Destroy(explosion, 1);
        Camera.main.GetComponent<CameraMovement>().Shake(.25f, .1f);
        Camera.main.GetComponent<CameraMovement>().LightFlash(transform.position, .2f, 1.0f, Color.yellow);
    }
}
