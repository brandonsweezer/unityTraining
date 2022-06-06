using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public GameObject playerModel;
    private float horizontalMovement;
    private float verticalMovement;
    public float moveSpeed;
    // Start is called before the first frame update
    void Start()
    {
        horizontalMovement = 0.0f;
        verticalMovement = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        horizontalMovement = Input.GetAxisRaw("Horizontal");
        verticalMovement = Input.GetAxisRaw("Vertical");
        float moveX = (horizontalMovement * moveSpeed * Time.deltaTime);
        float moveY = (verticalMovement * moveSpeed * Time.deltaTime);
        Vector3 newPos = new Vector3(moveX, moveY, 0) + playerModel.transform.position;
        playerModel.transform.position = newPos;
    }
}
