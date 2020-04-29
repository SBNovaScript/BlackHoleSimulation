using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitObjectToggle : MonoBehaviour
{
    public GameObject orbitObjectContainer;

    void OnSelect()
    {
        orbitObjectContainer.BroadcastMessage("OnToggleOrbitObject");
    }
}
