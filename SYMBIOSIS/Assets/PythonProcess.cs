using System;
using System.Diagnostics;
using System.IO;

namespace Assets
{
    class PythonProcess
    {
        // Private Attributes


        // Custom Constructor
        public PythonProcess()
        {
        }

        /// <summary>
        /// Creates a python process and opens and runs the prototype solution, 
        /// passing in x, y, z values from user input via command line
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public string RunFlightApplication(string filePath)
        {
            // Attempt to start a Python.exe process and run the python prototype file
            string output = "";
            try
            {
                ProcessStartInfo start = new ProcessStartInfo();

                start.FileName = "python.exe";
                start.Arguments = string.Format("{0}", filePath);

                start.UseShellExecute = false;
                start.RedirectStandardOutput = true;

                using (Process process = Process.Start(start))
                {
                    using (StreamReader reader = process.StandardOutput)
                    {
                        string result = reader.ReadToEnd();
                        output = result;
                    }
                }
            }
            catch (Exception e)
            {
                output = e.Message;
            }

            return output;
        }
    }
}


