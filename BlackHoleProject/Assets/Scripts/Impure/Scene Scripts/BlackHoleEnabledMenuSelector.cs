using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackHoleEnabledMenuSelector : MenuSelector
{

    public GameObject mainCamera;

    public override void OnSelect()
    {
        CamToShader blackHoleShader = mainCamera.GetComponent<CamToShader>();
        blackHoleShader.enabled = !blackHoleShader.enabled;
    }
}
