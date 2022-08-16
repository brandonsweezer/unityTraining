using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickupHandler : MonoBehaviour

{
    public string itemName;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other) {
        
        if (other.gameObject.tag == "Player") {
            Debug.Log(other.gameObject.tag);
            PlayerInventory inv = other.gameObject.GetComponent<PlayerInventory>();
            inv.AddToInventory(itemName);            
            Destroy(gameObject);
        }
    }
}
