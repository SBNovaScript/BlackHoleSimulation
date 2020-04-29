using UnityEngine;

public class OrbitObjectBegin : MenuSelector
{
    public GameObject blackHoleSimulation;

    public override void OnSelect()
    {
        blackHoleSimulation.BroadcastMessage("OnBeginOrbit");
    }
}
