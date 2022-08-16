using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SpawnItemPickups : MonoBehaviour
{
    public float timer = 0.0f;
    public int seconds = 0;
    public int itemsPerSecond;
    public int maxItems;

    public Camera cam;
    public GameObject itemPickup;    

    public GameObject itemPickupGroup; 

    private int lastSpawned = 0;
    private int itemNumber = 0;
    // Start is called before the first frame update
    void Start()
    {
        itemPickup = Resources.Load<GameObject>("Pickups/ItemPickupObject");
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        seconds = (int)timer;        

        // Instantiate at position (0, 0, 0) and zero rotation.
        if (lastSpawned != seconds) {
            if (itemPickupGroup.transform.childCount < maxItems) {
                for (int i = 0; i < itemsPerSecond; i ++) {
                    GameObject obj;
                    obj = Instantiate(itemPickup, getSpawnLocation(), Quaternion.identity);
                    obj.GetComponent<ItemPickupHandler>().itemName = "coldDash";
                    obj.name = $"item_{itemNumber}";
                    obj.transform.SetParent(itemPickupGroup.transform);
                    lastSpawned = seconds;
                    itemNumber += 1;
                }
            }
            
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
