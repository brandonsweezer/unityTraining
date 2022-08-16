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

    public void AddToInventory(string itemName)
    {   
        inventory[itemName] = inventory.ContainsKey(itemName) ? inventory[itemName] + 1 : 1; // 1line Gang represent
    }
}
