using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{    
    public Dictionary<string,int> inventory;

    // Start is called before the first frame update
    void Start()
    {
        inventory = new Dictionary<string,int>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other) {
        Debug.Log(other);
        if (other.gameObject.tag == "ItemPickup") {
            inventory.Add(other.gameObject.name, 1);
            Destroy(other.gameObject);
            Debug.Log(inventory);
        }
    }
}
