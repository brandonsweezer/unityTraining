using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToPlayer : MonoBehaviour
{

    public GameObject playerModel;
    public float moveSpeed;
    public Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        if (playerModel == null) {
            playerModel = GameObject.FindGameObjectWithTag("Player");
        }
        if (rb == null) {
            rb = transform.GetComponent<Rigidbody2D>();
        }
        moveSpeed = Random.Range(1.0f, 5.0f);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 playerPosition = playerModel.transform.position;
        Vector3 direction = new Vector3(playerPosition.x - transform.position.x, playerPosition.y - transform.position.y, 0).normalized;

        rb.velocity = direction * moveSpeed;
    }
}
