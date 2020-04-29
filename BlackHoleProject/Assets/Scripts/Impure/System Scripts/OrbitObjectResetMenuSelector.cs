using UnityEngine;

public class OrbitObjectResetMenuSelector : MenuSelector
{
    public GameObject blackHoleSimulation;

    public override void OnSelect()
    {
        blackHoleSimulation.BroadcastMessage("OnStopOrbit");
    }
}
