using UnityEngine;

public class TimeDilationEnabledMenuSelector : MenuSelector
{
    public GameObject timeDilation;

    public override void OnSelect()
    {
        timeDilation.BroadcastMessage("OnToggleTimeDilation");
    }
}
