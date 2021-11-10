using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineController : MonoBehaviour
{
    private Transform[] rocks;
    public PlayerMovements playerMovements;

    private void Update()
    {
        // SetUpLine();
    }

    public void SetUpLine()
    {
        for (int i = 0; i < rocks.Length; i++)
        {
            float lineLength = 1.5f;
            LineRenderer lineRenderer = rocks[i].GetComponent<LineRenderer>();
            Vector3 startLinePos = rocks[i].position;
            Vector3 endLinePos = rocks[i].position.normalized - (playerMovements.GetPosition().normalized * lineLength);

            // lineRenderer.SetPosition(0, startLinePos);
            // lineRenderer.SetPosition(1, endLinePos);
        }
    }
}
