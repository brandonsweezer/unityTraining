using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thermodynamics : MonoBehaviour
{

    private UnityEngine.Rendering.Universal.Light2D tempLight;
    public float temperature;
    public float mass;
    public float maxTemp;
    public float specificHeat;
    public float colorMultiplier;
    public float maxIntensity;
    // Start is called before the first frame update
    void Start()
    {
        maxIntensity = 4.5f;
        tempLight = transform.gameObject.GetComponent<UnityEngine.Rendering.Universal.Light2D>();
        if (temperature > maxTemp) {
            temperature = maxTemp;
        }
        tempLight.intensity = Mathf.Abs((temperature / maxTemp) * maxIntensity);
        if (tempLight.intensity > maxIntensity) {
            tempLight.intensity = maxIntensity;
        }
        tempLight.color = getLightColor();
        
    }

    void FixedUpdate() 
    {
        if (temperature > maxTemp) {
            temperature = maxTemp;
        }
        tempLight.intensity = Mathf.Abs((temperature / maxTemp) * maxIntensity);
        if (tempLight.intensity > maxIntensity) {
            tempLight.intensity = maxIntensity;
        }
        // h s v
        // h is good, ranges from 0 - .15 where 0 is red and .15 is yellow, more temperature = more red ( cooler )
        // s is saturation, ranges from 0 to 1, 0 = white (desaturated), 1 = color. 
        // want it so more temperature = more saturated, and an exponential decline in saturation once it gets below 20% temperature
        tempLight.color = getLightColor();
    }

    Color getLightColor() {
        float saturation = 1 - Mathf.Exp(10*((temperature / maxTemp) - 1));
        float coldColor = (-.1f/((temperature / maxTemp)-.17f)) + .38f;
        float hotColor = .15f*(temperature / maxTemp);
        float colorTemp = (temperature/maxTemp) < 0 ? coldColor : hotColor;
        
        return Color.HSVToRGB(colorTemp, saturation, 1);
    }

    float Transfer(Thermodynamics t) {
        float energy = (t.temperature - temperature) * t.specificHeat * t.mass * Time.fixedDeltaTime;
        temperature += energy;
        return energy;
    }
    void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.tag != transform.gameObject.tag) {
            Thermodynamics thermodynamics = collision.gameObject.GetComponent<Thermodynamics>();
            if (thermodynamics) {
                float energy = Transfer(thermodynamics);
            }
        }
        
    }

    void OnCollisionStay2D(Collision2D collision) {
        if (collision.gameObject.tag != transform.gameObject.tag) {
            Thermodynamics thermodynamics = collision.gameObject.GetComponent<Thermodynamics>();
            if (thermodynamics) {
                float energy = Transfer(thermodynamics);
                showNumber(collision.contacts[0].point, energy);
            }
        }
    }

    void showNumber(Vector3 position, float number) {
        GameObject.FindWithTag("GameController").GetComponent<EnergyDisplay>().ThermalNumber(position, number);
    }
}
