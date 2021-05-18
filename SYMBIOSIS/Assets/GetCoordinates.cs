using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Text;
using System.IO;

namespace Assets
{
    public class GetCoordinates : MonoBehaviour
    {

        static public int WayPointsCount;
        public int TimerCount = 0;
        static public Vector3 Coo;
        public GameObject Drone;
        static public List<Vector3> Flightpoints = new List<Vector3>();
        static public List<string> Timers = new List<string>();
        PythonProcess pp;
 

        // Use this for initialization
        void Start()
        {
            pp = new PythonProcess();
            const string PATH_APPLICATION = @"C:\Test\Multi-waypoints\multi_v4.py";
            const string PATH_OUTPUT_FILE = @"C:\Test\Multi-waypoints\Waypoints.txt";
            string coordinates = "";
            StringBuilder sb = new StringBuilder();
            List<string> waypoints = new List<string>();
            //List<string> timeValues = new List<string>();

            List<GameObject> lst = new List<GameObject>();

            char[] trimChars = { '(', ')' };

            // Extract and store co-ordinate waypoint data for each child game object created under the parent
            foreach (Transform child in this.transform)
            {

                lst.Add(child.gameObject);

                Timers.Add(child.gameObject.GetComponent<GetTime>().Time);
                Coo = new Vector3(child.gameObject.transform.position.x, child.gameObject.transform.position.y, child.gameObject.transform.position.z);
                Flightpoints.Add(Coo);
                waypoints.Add(Coo.ToString());
            }

            WayPointsCount = waypoints.Count;
            // Run a loop to remove all special characters from each coordinate and write to string builder object
            for (int index = 0; index < waypoints.Count; index++)
            {
                string coord = waypoints[index].Trim(trimChars);    // Remove brackets
                coordinates = coord.Replace(" ", string.Empty);      // Remove whitespace
                sb.Append(string.Format("{0},{1}", coordinates, Timers[index]));
                sb.AppendLine();
            }

            // Output Waypoints to Unity Console
            Debug.Log(sb.ToString());

            // Attempt to create a new file with write acces
            try
            {
                FileStream fs = new FileStream(PATH_OUTPUT_FILE, FileMode.Create, FileAccess.Write);
                StreamWriter sw = new StreamWriter(fs);
                sw.Write(sb.ToString());
                sw.Close();
            }
            catch (Exception e)
            {
                Debug.Log(e.Message);
                throw;
            }

            // Run Python Flight-Control Application (Output To Console)
            Debug.Log(pp.RunFlightApplication(PATH_APPLICATION));
        }


        // Update is called once per frame
        void Update()
        {

        }
    }
}
