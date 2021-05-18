using Assets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets
{
    public class GenerateFlightPath : MonoBehaviour
    {

        static public int WayPointsNum = 0;
        private LineRenderer lineRenderer;
        // Use this for initialization
        void Start()
        {
            //Add LineRenderer Component
            lineRenderer = gameObject.AddComponent<LineRenderer>();
            //Set Materials
            lineRenderer.material = new Material(Shader.Find("Particles/Additive"));

            //Set Colors
            lineRenderer.startColor = Color.red;
            lineRenderer.endColor = Color.red;

            //Set width
            lineRenderer.startWidth = 0.05f;
            lineRenderer.endWidth = 0.05f;
        }

        // Update is called once per frame
        void Update()
        {
            lineRenderer = GetComponent<LineRenderer>();

            lineRenderer.positionCount = GetCoordinates.WayPointsCount;
            foreach (Vector3 Flightpoint in GetCoordinates.Flightpoints)
            {
                if (WayPointsNum < GetCoordinates.WayPointsCount)
                {
                    lineRenderer.SetPosition(WayPointsNum, Flightpoint);
                    WayPointsNum++;
                }
            }
        }
    }
}
