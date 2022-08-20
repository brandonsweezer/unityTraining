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
    public GameObject hotEnemy;
    public GameObject coldEnemy;
    public GameObject enemy;
    public TMP_Text timerText;

    public GameObject enemyGroup; 

    private int lastSpawned = -1;
    private int enemyNumber = 0;
     public List<string> wavesToSpawn;
    // Start is called before the first frame update
    void Start()
    {
        enemy = Resources.Load<GameObject>("Enemies/Square");
        cam = Camera.main;
        wavesToSpawn = new List<string>{ "smallBasicWave", "largeBasicWave" };
        // spawnRandomWave();
    }

    // Update is called once per frame
    void Update()
    {
        // TODO: i think this doesn't belong here right?
        timer += Time.deltaTime;
        seconds = (int)timer;
        updateText();
        if ((seconds % 20) == 0) {
            if (lastSpawned != seconds) {
                lastSpawned = seconds;
                spawnRandomWave();
            }
        }
        // (OLD SPAWN CODE)
        // Instantiate at position (0, 0, 0) and zero rotation.
        // if (lastSpawned != seconds) {
        //     if (enemyGroup.transform.childCount < maxEnemies) {
        //         for (int i = 0; i < enemiesPerSecond; i ++) {
        //             spawnSquareAtRandomPoint();
        //         }
        //     }
        // }
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

    void spawnSquareAtRandomPoint() {
        GameObject obj;
        obj = Instantiate(enemy, getSpawnLocation(), Quaternion.identity);
        obj.GetComponent<Thermodynamics>().temperature = Random.Range(-1,1) * obj.GetComponent<Thermodynamics>().maxTemp;
        obj.name = $"enemy_{enemyNumber}";
        obj.transform.SetParent(enemyGroup.transform);
        
        enemyNumber += 1;
    }

    void spawnRandomWave() {
        var RndB = new System.Random();
        int index = RndB.Next(wavesToSpawn.Count);
        if (wavesToSpawn[index] == "smallBasicWave") {
            for (int i = 0; i < 10; i ++) {
                spawnSquareAtRandomPoint();
            }            
        } else if (wavesToSpawn[index] == "largeBasicWave") {
            for (int i = 0; i < 15; i ++) {
                spawnSquareAtRandomPoint();
            }
        } else if (wavesToSpawn[index] == "completeCircleWave") {
            // obj.GetComponent<UnityEngine.Rendering.Universal.Light2D>().color = Color.magenta;
            Debug.Log("Nah!");
        }
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
