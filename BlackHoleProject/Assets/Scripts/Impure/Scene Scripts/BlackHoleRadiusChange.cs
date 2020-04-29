using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackHoleRadiusChange : MonoBehaviour
{
    public float amountToScaleBlackHole;
    
    public CamToShader blackHoleShader;
    public Transform objectToScale;
    public float amountToScaleConsoleObject;

    void OnSelect()
    {
        float newAmount = blackHoleShader.rad + amountToScaleBlackHole;
        if (newAmount > 0 && newAmount < 1)
        {
            blackHoleShader.rad += amountToScaleBlackHole;
            objectToScale.localScale = new Vector3(objectToScale.localScale.x + amountToScaleConsoleObject, objectToScale.localScale.y + amountToScaleConsoleObject, objectToScale.localScale.z + amountToScaleConsoleObject);
        }
    }
}
