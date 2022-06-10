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
    // Start is called before the first frame update
    void Start()
    {
        tempLight = transform.gameObject.GetComponent<UnityEngine.Rendering.Universal.Light2D>();
    }

    void FixedUpdate() 
    {
        if (temperature > maxTemp) {
            temperature = maxTemp;
        }
        tempLight.intensity = (temperature / maxTemp) * 4.5f + .5f;
        // h s v
        // h is good, ranges from 0 - .15 where 0 is red and .15 is yellow, more temperature = more red ( cooler )
        // s is saturation, ranges from 0 to 1, 0 = white (desaturated), 1 = color. 
        // want it so more temperature = more saturated, and an exponential decline in saturation once it gets below 20% temperature

        // y = 1 - e^(10(x-1))
        float saturation = 1 - Mathf.Exp(10*((temperature / maxTemp) - 1));
        tempLight.color = Color.HSVToRGB(0.15f*(temperature / maxTemp), saturation, 1);
    }

    void Transfer(Thermodynamics t) {
        temperature += (t.temperature - temperature) * t.specificHeat * t.mass * Time.fixedDeltaTime;
    }

    void OnCollisionStay2D(Collision2D collision) {
        if (collision.gameObject.tag != transform.gameObject.tag) {
            Thermodynamics thermodynamics = collision.gameObject.GetComponent<Thermodynamics>();
            if (thermodynamics) {
                Transfer(thermodynamics);
            }
        }
        
    }
}
