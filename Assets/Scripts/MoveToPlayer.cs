using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToPlayer : MonoBehaviour
{

    public GameObject playerModel;
    public float moveSpeed;
    // Start is called before the first frame update
    void Start()
    {
        if (playerModel == null) {
            playerModel = GameObject.FindGameObjectWithTag("Player");
        }
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 playerPosition = playerModel.transform.position;
        Vector3 direction = Vector3.Normalize(playerPosition - transform.position);

        transform.position = transform.position + (direction * moveSpeed * Time.deltaTime);
    }
}
