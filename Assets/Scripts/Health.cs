using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public float health;
    public float maxHealth;
    GameObject deathEffect;
    // Start is called before the first frame update
    void Start()
    {
        deathEffect = Resources.Load<GameObject>("Enemies/deathEffect");
    }

    // Update is called once per frame
    void Update()
    {
        if (health < 0) {
            health = 0;
            Destroy(transform.parent);
        }

        Transform healthbar = transform.Find("HealthBar");
        healthbar.localScale = new Vector3(health / maxHealth, healthbar.localScale.y, healthbar.localScale.z);
        healthbar.localPosition = new Vector3((1 - (health / maxHealth)) / -2, healthbar.localPosition.y, healthbar.localPosition.z);
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            health -= 5;
            Vector3 enemyPos = collision.gameObject.transform.position;
            GameObject explosion = Instantiate(deathEffect, enemyPos, Quaternion.identity);
            Destroy(collision.gameObject);
            Destroy(explosion, 1);
            StartCoroutine(Camera.main.GetComponent<CameraMovement>().Shake(.25f, .1f));
        }
    }
}
