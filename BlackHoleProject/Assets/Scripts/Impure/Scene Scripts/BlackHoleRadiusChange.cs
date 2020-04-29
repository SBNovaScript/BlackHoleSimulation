using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackHoleRadiusChange : MonoBehaviour
{
    public float amount;
    public CamToShader blackHoleShader;

    void OnSelect()
    {
        blackHoleShader.rad += amount;
    }
}
