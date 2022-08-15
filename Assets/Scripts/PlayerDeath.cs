using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerDeath : MonoBehaviour
{

    public TMP_Text deathText;
    private GameObject player;
    private GameObject playerPrefab;
    private Thermodynamics playerThermals;
    private bool playerIsDead;
    // Start is called before the first frame update
    void Start()
    {
        playerPrefab = Resources.Load<GameObject>("Player/Player");
        deathText.enabled = false;
        player = GameObject.FindWithTag("Player");
        playerThermals = player.GetComponent<Thermodynamics>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerIsDead) {
            if (Input.GetKeyDown(KeyCode.R)) {
                RespawnPlayer();
            }
        }
        if (playerThermals.temperature > playerThermals.maxTemp) {
            ShowDeathScreen();
            Destroy(player);
            playerIsDead = true;
        }
    }

    public void ShowDeathScreen() {
        deathText.enabled = true;
    }

    private void RespawnPlayer() {
        deathText.enabled = false;
        player = Instantiate(playerPrefab, new Vector3(0,0,0), Quaternion.identity);
    }
}
