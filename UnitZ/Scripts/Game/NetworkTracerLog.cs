using UnityEngine;
using System;
using System.IO;
using Mirror;

public class NetworkTracerLog : MonoBehaviour {

    private TextWriter tw;
    private string TW_Filename;

    void Start () {
        // Open a log file
        TW_Filename = String.Format("NetworkBPS_{0}.log", DateTime.Now.ToString("yyyy.MM.dd.hh.mm"));
        tw = new StreamWriter(TW_Filename, true);
        tw.Close();
    }

    private string LogText;
    private float timeTmp;

    private void OnGUI()
    {
        // If a network game
        if (NetworkServer.active || NetworkClient.active)
        {
            //  If it's time to recalculate
            if (Time.time >= timeTmp + 1)
            {
                // Set recording time
                timeTmp = Time.time;

                // Save findings to disk
                LogText = string.Format("{0:00000000}: {1}", timeTmp, DateTime.Now.ToString());
                File.AppendAllText(TW_Filename, LogText + Environment.NewLine);
            }
            // Output usage
            GUI.skin.label.fontSize = 16;
            GUI.skin.label.alignment = TextAnchor.UpperLeft;
            GUI.Label(new Rect(5, 5, Screen.width / 2, 100), LogText);
        }
    }
}
