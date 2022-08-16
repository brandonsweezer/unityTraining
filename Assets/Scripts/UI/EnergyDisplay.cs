using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnergyDisplay : MonoBehaviour
{
    Thermodynamics playerThermals;
    public TMP_Text playerThermalsText;
    private GameObject energyText;
    public GameObject canvas;
    // Start is called before the first frame update
    void Start()
    {
        playerThermals = GameObject.FindWithTag("Player").GetComponent<Thermodynamics>();
        energyText = Resources.Load<GameObject>("UI/EnergyNumber");
    }

    // Update is called once per frame
    void Update()
    {
        playerThermalsText.SetText($"{playerThermals.temperature * playerThermals.mass * playerThermals.specificHeat} J");
    }

    public void ThermalNumber(Vector3 position, float energy) {
        StartCoroutine(ThermalNumberC(position, energy));
    }

    IEnumerator ThermalNumberC(Vector3 position, float energy) {
        // Vector3 screenspace = Camera.main.WorldToScreenPoint(position);
        GameObject energyObj = Instantiate(energyText, position, Quaternion.identity);
        // // energyObj.GetComponent<RectTransform>().anchoredPosition = new Vector2(screenspace.x, screenspace.y);
        // energyObj.transform.SetParent(canvas.transform, true);
        energyObj.GetComponent<TMP_Text>().SetText($"{(int)energy + 1}"); // maek betr
        float elapsedTime = 0;
        float duration = Random.Range(0.5f,1f+(energy*10));
        float moveSpeed = 5.0f;
        float direction = Random.Range(-3f,3f);
        while (elapsedTime < duration) {
            Vector3 pos = energyObj.transform.position;
            energyObj.transform.position = new Vector3(pos.x + (direction * Time.deltaTime), pos.y + Mathf.Min(moveSpeed, moveSpeed/elapsedTime) * Time.deltaTime, -2);
            
            // Vector3 pos = energyObj.GetComponent<RectTransform>().anchoredPosition;
            // energyObj.GetComponent<RectTransform>().anchoredPosition = new Vector2(pos.x, pos.y + moveSpeed * Time.deltaTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        Destroy(energyObj);
    }
}
