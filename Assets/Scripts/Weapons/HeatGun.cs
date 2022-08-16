using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeatGun : MonoBehaviour
{
    private float cooldown;
    public float fireRate;
    public float bulletVelocity;
    public GameObject projectile;
    // Start is called before the first frame update
    void Start()
    {
        cooldown = 0;
        projectile = Resources.Load<GameObject>("Weapons/HeatGunProjectile");
    }

    void FixedUpdate() {
        if (cooldown == 0) {
            Fire();
            cooldown = 60.0f/(fireRate); // 60 rpm = 1 per second
        } 
        if (cooldown < 0) {
            cooldown = 0;
        } else {
            cooldown -= Time.fixedDeltaTime;
        }
        
    }

    void Fire() {
        Vector3 right = new Vector3(1.0f, 1.0f, 0);
        GameObject projectileObj1 = Instantiate(projectile, transform.position + right, Quaternion.identity);
        projectileObj1.GetComponent<Rigidbody2D>().velocity = bulletVelocity * right;

        Vector3 left = new Vector3(-1.0f, -1.0f, 0);
        GameObject projectileObj2 = Instantiate(projectile, transform.position + left, Quaternion.identity);
        projectileObj2.GetComponent<Rigidbody2D>().velocity = bulletVelocity * left;

        Vector3 up = new Vector3(-1.0f, 1.0f, 0);
        GameObject projectileObj3 = Instantiate(projectile, transform.position + up, Quaternion.identity);
        projectileObj3.GetComponent<Rigidbody2D>().velocity = bulletVelocity * up;

        Vector3 down = new Vector3(1.0f, -1.0f, 0);
        GameObject projectileObj4 = Instantiate(projectile, transform.position + down, Quaternion.identity);
        projectileObj4.GetComponent<Rigidbody2D>().velocity = bulletVelocity * down;
    }
}
