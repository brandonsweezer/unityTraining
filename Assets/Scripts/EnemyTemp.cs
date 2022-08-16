using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnemyTemp : MonoBehaviour
{
    private Thermodynamics thermals;
    private EnemyAI enemyAIScript;
    private float myTemp;
    public TMP_Text enemyThermalsText;
    private GameObject tempText;
    private GameObject tempObj;
    

    // Start is called before the first frame update
    void Start()
    {
        thermals = transform.gameObject.GetComponent<Thermodynamics>();
        enemyAIScript = transform.gameObject.GetComponent<EnemyAI>();
        tempText = Resources.Load<GameObject>("UI/TempNumber");
    
        // myTemp = thermals.temperature;
        tempObj = Instantiate(tempText, new Vector3(0,0,0), Quaternion.identity);
        enemyThermalsText = tempObj.GetComponent<TMP_Text>();

        enemyAIScript.temperatureText = tempObj;

    }

    // Update is called once per frame
    void Update()
    {
        enemyThermalsText.SetText($"{Mathf.Round(thermals.temperature)}");
        tempObj.transform.position = new Vector3(transform.position.x, transform.position.y+0.75f, transform.position.z+1);
    }
}
