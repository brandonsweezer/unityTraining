using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickupFloatMovement : MonoBehaviour
{
    public float distance;
    public float duration;
    private Vector3 start;
    private Vector3 end;

    private bool rising;
    private float elapsedTime;

    // Start is called before the first frame update
    void Start()
    {   
        rising = true;
        elapsedTime = 0;
        start = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        end = new Vector3(transform.position.x, transform.position.y + distance, transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        float smoothie;
        smoothie = Mathf.SmoothStep(0, 1, elapsedTime/duration);

        elapsedTime += Time.deltaTime;
        
        if (rising) {
            transform.position = Vector3.Lerp(start, end, smoothie);
        } else {
            transform.position = Vector3.Lerp(end, start, smoothie);
        }

        if (elapsedTime > duration) {
            elapsedTime = 0;
            rising = !rising;
        }        
        // transform.position = new Vector3(transform.position.x, Vector3.Lerp(transform.position.y, newPosition, 0.5f), transform.position.z);
    }
}
