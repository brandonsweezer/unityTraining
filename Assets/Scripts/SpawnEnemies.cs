using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SpawnEnemies : MonoBehaviour
{
    public float timer = 0.0f;
    public int seconds = 0;
    public int enemiesPerSecond;
    public int maxEnemies;

    public Camera cam;
    public GameObject enemyToSpawn;
    public TMP_Text timerText;

    public GameObject enemyGroup; 

    private int lastSpawned = 0;
    private int enemyNumber = 0;
    // Start is called before the first frame update
    void Start()
    {
        enemyToSpawn = Resources.Load<GameObject>("Enemies/square_red");
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        seconds = (int)timer;
        updateText();

        // Instantiate at position (0, 0, 0) and zero rotation.
        if (lastSpawned != seconds) {
            if (enemyGroup.transform.childCount < maxEnemies) {
                for (int i = 0; i < enemiesPerSecond; i ++) {
                    GameObject obj = Instantiate(enemyToSpawn, getSpawnLocation(), Quaternion.identity);
                    obj.name = $"enemy_{enemyNumber}";
                    obj.transform.SetParent(enemyGroup.transform);
                    lastSpawned = seconds;
                    enemyNumber += 1;
                }
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
        float randPos = Random.Range(0f, 1.0f);
        Vector3 pos = new Vector3(0, 0, cam.nearClipPlane);
        switch (screen) {
            case 0:
                // top
                pos = new Vector3(randPos, 1, cam.nearClipPlane);
                spawnPoint = cam.ViewportToWorldPoint(pos);
            break;
            case 1:
                // bottom
                pos = new Vector3(randPos, 0, cam.nearClipPlane);
                spawnPoint = cam.ViewportToWorldPoint(pos);
            break;
            case 2:
                // left
                pos = new Vector3(0, randPos, cam.nearClipPlane);
                spawnPoint = cam.ViewportToWorldPoint(pos);
            break;
            case 3:
                // right
                pos = new Vector3(1, randPos, cam.nearClipPlane);
                spawnPoint = cam.ViewportToWorldPoint(pos);
            break;
            default:
            break;
        }

        return spawnPoint;
    }
}
