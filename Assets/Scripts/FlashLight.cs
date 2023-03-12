using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class FlashLight : MonoBehaviour {
    private Light flashLight;
     
    void Start () 
    {
        flashLight = GetComponent<Light>(); 
    }

    void OnGUI()
    {
            GUI.Label(new Rect(70, Screen.height - 100, 155, 50), "Press 'E' to turn on flashlight");
    }
    void Update ()
    {
        if(Input.GetKeyUp(KeyCode.E))
        {
            flashLight.enabled = !flashLight.enabled;
        }
    }
}