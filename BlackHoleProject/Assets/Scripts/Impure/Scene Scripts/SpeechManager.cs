using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Windows.Speech;

public class SpeechManager : MonoBehaviour
{

    // Skeleton from:
    // https://docs.microsoft.com/en-us/windows/mixed-reality/holograms-101

    KeywordRecognizer keywordRecognizer = null;
    Dictionary<string, System.Action> keywords = new Dictionary<string, System.Action>();

    private void Start()
    {
        keywords.Add("Begin Simulation", () =>
        {
            this.BroadcastMessage("OnBeginSimulation");
        });

        keywords.Add("Begin Orbit", () =>
        {
            GameObject focusObject = GazeGestureManager.Instance.FocusedObject;
            if (focusObject != null)
            {
                focusObject.SendMessage("OnBeginOrbit", SendMessageOptions.DontRequireReceiver);
            }
        });

        keywords.Add("Stop All Orbits", () =>
        {
            this.BroadcastMessage("OnStopOrbit");
        });

        keywords.Add("Slow Down Time", () =>
        {
            this.BroadcastMessage("OnReduceTime");
        });

        keywordRecognizer = new KeywordRecognizer(keywords.Keys.ToArray());

        keywordRecognizer.OnPhraseRecognized += KeywordRecognizer_OnPhraseRecognized;
        keywordRecognizer.Start();
    }

    private void KeywordRecognizer_OnPhraseRecognized(PhraseRecognizedEventArgs args)
    {
        System.Action keywordAction;
        if (keywords.TryGetValue(args.text, out keywordAction))
        {
            keywordAction.Invoke();
        }
    }
}
