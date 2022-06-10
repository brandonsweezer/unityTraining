using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public GameObject playerModel;
    public float moveSpeed;
    public float dashSpeed;
    public float dashDuration;

    private bool dashing;
    private float dashTimer;
    private Vector3 dashVelocity;
    private Rigidbody2D rb;
    public ParticleSystem dashEffect;
    // Start is called before the first frame update
    void Start()
    {
        if (!playerModel) {
            playerModel = GameObject.FindWithTag("Player");
        }
        dashEffect.Stop();
        rb = playerModel.GetComponent<Rigidbody2D>();
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.Space) && !dashing) {
            Dash();
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (dashTimer > dashDuration) {
            dashing = false;
            dashEffect.Stop();
        }

        if (dashing) {
            applyDashMovement();
            dashTimer += Time.fixedDeltaTime;
        } else {
            applyRegularMovement();
        }
        
    }

    void applyRegularMovement() {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        Vector3 velocity = new Vector3(moveX, moveY, 0).normalized * moveSpeed;
        rb.velocity = velocity;
    }

    void applyDashMovement() {
        rb.velocity = dashVelocity;
        Thermodynamics thermals = playerModel.GetComponent<Thermodynamics>();
        thermals.temperature += 1.0f;
    }


    void Dash() {
        dashTimer = 0;
        dashing = true;
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");
        dashVelocity = new Vector3(moveX, moveY, 0).normalized * dashSpeed;
        rb.velocity = dashVelocity;
        // emit particles
        dashEffect.Play();
        Camera.main.GetComponent<CameraMovement>().Shake(.75f, dashDuration);
    }
}
