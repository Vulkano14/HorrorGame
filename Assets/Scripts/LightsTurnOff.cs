using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightsTurnOff : MonoBehaviour
{


    bool inArea = false;
    bool runOnece = false; 

    private Light light;

    void Start()
    {
        GetComponent<Collider>().isTrigger = true;
        light = GetComponent<Light>(); 
    }

    void Update()
    {
        if (runOnece == false && inArea)
        {
          light.enabled = !light.enabled;
          runOnece = true;
        }
    }

    void OnGUI()
    {
        if (inArea)
        {
            GUI.Label(new Rect(Screen.width / 2 - 75, Screen.height - 100, 155, 30), "Dobranoc :>");
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            inArea = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            inArea = false;
        }
    }
}