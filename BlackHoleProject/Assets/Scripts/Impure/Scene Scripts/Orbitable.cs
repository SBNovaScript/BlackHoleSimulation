using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orbitable : MonoBehaviour
{
    private Vector3 startPosition;
    private float timeToReachStart = 0.5f;

    private void Start()
    {
        startPosition = this.gameObject.transform.position;
    }

    void OnSelect()
    {
        if (!this.GetComponent<OrbitObjectScript>())
        {
            this.gameObject.AddComponent<OrbitObjectScript>();
        }
    }

    void OnBeginOrbit()
    {
        if (!this.GetComponent<OrbitObjectScript>())
        {
            this.gameObject.AddComponent<OrbitObjectScript>();
        }
    }

    void OnStopOrbit()
    {
        if (this.GetComponent<OrbitObjectScript>())
        {
            Destroy(this.gameObject.GetComponent<OrbitObjectScript>());
            this.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
            StartCoroutine(MoveToStart());
        }
    }

    private IEnumerator MoveToStart()
    {
        Vector3 currentPosition = this.gameObject.transform.position;
        float currentTime = 0;
        while (currentTime < 1)
        {
            currentTime += Time.deltaTime / timeToReachStart;
            transform.position = Vector3.Lerp(currentPosition, startPosition, currentTime);
            yield return null;
        }
    }
}
