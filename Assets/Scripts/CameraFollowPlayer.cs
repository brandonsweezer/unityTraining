using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowPlayer : MonoBehaviour
{
    private GameObject playerModel;
    // Start is called before the first frame update
    void Start()
    {
        playerModel = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(playerModel.transform.position.x, playerModel.transform.position.y, transform.position.z);
    }
}
