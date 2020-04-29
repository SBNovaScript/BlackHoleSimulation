using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackHoleRadiusChange : MonoBehaviour
{
    public float amount;
    public CamToShader blackHoleShader;

    void OnSelect()
    {
        float newAmount = blackHoleShader.rad + amount;
        if (newAmount > 0 && newAmount < 1)
        {
            blackHoleShader.rad += amount;
        }
    }
}
