using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E)) {
            StartCoroutine(Shake(1.0f, 1.0f));
        }
    }

    public IEnumerator Shake(float magnitude, float duration) {
        Vector3 originalPos = transform.localPosition;

        float elapsedTime = 0;
        while (elapsedTime < duration) {
            float xOffset = Random.Range(-.5f, .5f) * magnitude;
            float yOffset = Random.Range(-.5f, .5f) * magnitude;

            transform.localPosition = new Vector3(originalPos.x + xOffset, originalPos.y + yOffset, originalPos.z);

            elapsedTime += Time.deltaTime;

            // wait one frame
            yield return null;
        }

        transform.localPosition = originalPos;
    }
}
