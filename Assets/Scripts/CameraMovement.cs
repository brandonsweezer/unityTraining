using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{

    private GameObject lightFlash;
    void Start() {
        lightFlash = Resources.Load<GameObject>("Enemies/deathFlash");
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E)) {
            Shake(1.0f, 1.0f);
        }
    }

    public void Shake(float magnitude, float duration) {
        StartCoroutine(ShakeC(magnitude, duration));
    }
    private IEnumerator ShakeC(float magnitude, float duration) {
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

    public void LightFlash(Vector3 position, float duration, float intensity, Color color) {
        StartCoroutine(LightFlashC(position, duration, intensity, color));
    }
    private IEnumerator LightFlashC(Vector3 position, float duration, float intensity, Color color) {
        GameObject lightObj = Instantiate(lightFlash, position, Quaternion.identity);
        UnityEngine.Rendering.Universal.Light2D light2 = lightObj.GetComponent<UnityEngine.Rendering.Universal.Light2D>();
        float elapsedTime = 0.0f;
        while (elapsedTime < duration) {
            float percentComplete = (elapsedTime / duration);
            light2.intensity = (1 - percentComplete) * intensity;
            Debug.Log($"time: {elapsedTime}");
            Debug.Log($"duration: {duration}");
            elapsedTime += Time.deltaTime;
            Debug.Log($"time2: {elapsedTime}");
            // wait a frame
            Debug.Log("waiting a frame");
            yield return null;
        }
        Debug.Log("Destroying it!");
        Destroy(lightObj, .1f);
    }
}
