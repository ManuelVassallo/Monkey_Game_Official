using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collector : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D target)
    {
        // collect and remove all assets when they are of the screen
        if(target.tag == "BG" || target.tag == "Platform" || target.tag == "NormalPush" || target.tag == "ExtraPush" || target.tag == "Bird")
        {
            target.gameObject.SetActive(false);
        }
    }

}
