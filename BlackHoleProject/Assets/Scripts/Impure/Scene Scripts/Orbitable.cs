using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orbitable : MonoBehaviour
{
    void OnSelect()
    {
        if (!this.GetComponent<OrbitObjectScript>())
        {
            this.gameObject.AddComponent<OrbitObjectScript>();
        }
    }
}
