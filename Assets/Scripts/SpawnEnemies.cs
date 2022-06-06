using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SpawnEnemies : MonoBehaviour
{
    public float timer = 0.0f;
    public int seconds = 0;

    public Camera camera;
    public GameObject enemyToSpawn;
    public TMP_Text timerText;

    private int lastSpawned = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        seconds = (int)timer % 60;
        updateText();

        // Instantiate at position (0, 0, 0) and zero rotation.
        if (seconds % 2 == 0) {
            if (lastSpawned != seconds) {
                Instantiate(enemyToSpawn, getSpawnLocation(), Quaternion.identity);
                lastSpawned = seconds;
            }
        }
    }

    void updateText() {
        int displayMinutes = seconds / 60;
        int displaySeconds = seconds - (displayMinutes * 60);
        string displayText = "00:00";
        if (displaySeconds < 10) {
            displayText = $"{displayMinutes.ToString()}:0{displaySeconds.ToString()}";
        } else {
            displayText = $"{displayMinutes.ToString()}:{displaySeconds.ToString()}";
        }
        timerText.SetText(displayText);
    }

    Vector3 getSpawnLocation()
    {
        // 2 randoms
        // 0-1, 1-2, 2-3, 3-4, top bottom left right
        // position on screen side
        Vector3 spawnPoint = new Vector3(0,0,0);

        // 0 1 2 3
        int screen = Mathf.FloorToInt(Random.Range(0.0f,3.99999f));
        float randPos = Random.Range(-1.0f, 1.0f);
        Vector3 pos = new Vector3(0, 0, camera.nearClipPlane);
        switch (screen) {
            case 0:
                // top
                pos = new Vector3(randPos, 1, camera.nearClipPlane);
                spawnPoint = camera.ViewportToWorldPoint(pos);
            break;
            case 1:
                // bottom
                pos = new Vector3(randPos, -1, camera.nearClipPlane);
                spawnPoint = camera.ViewportToWorldPoint(pos);
            break;
            case 2:
                // left
                pos = new Vector3(-1, randPos, camera.nearClipPlane);
                spawnPoint = camera.ViewportToWorldPoint(pos);
            break;
            case 3:
                // right
                pos = new Vector3(1, randPos, camera.nearClipPlane);
                spawnPoint = camera.ViewportToWorldPoint(pos);
            break;
            default:
            break;
        }

        return spawnPoint;
    }
}
