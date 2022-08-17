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
        if (itemName == "heatGunFireRate") {
            HeatGun heatGun = gameObject.GetComponent<HeatGun>();
            heatGun.fireRate = heatGun.fireRate + inventory[itemName]*80;
        }
        if (itemName == "playerMovementSpeed") {
            PlayerMovement playerMovement = gameObject.GetComponent<PlayerMovement>();
            playerMovement.moveSpeed = playerMovement.moveSpeed + inventory[itemName];
        }
        if (itemName == "heavyHeatGunFireRate") {
            HeavyHeatGun heavyHeatGun = gameObject.GetComponent<HeavyHeatGun>();
            heavyHeatGun.fireRate = heavyHeatGun.fireRate + inventory[itemName]*5;
        }
        
    }
}
