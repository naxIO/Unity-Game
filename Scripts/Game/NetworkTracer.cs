﻿using Mirror;
using UnityEngine;

public class NetworkTracer : MonoBehaviour
{
    void Start()
    {
        instance = this;
    }
    // private int userbyteTmp;
    // private int totalbyteTmp;
    private float timeTmp;
    // private int userbytePerSec = 0;
    // private int totalbytePerSec = 0;
    public string Detail;
    public static NetworkTracer instance;
    public bool ShowGUI;

    public void Update()
    {
        // Commented out the parts related to NetworkTransport
        /*
        if (NetworkServer.active)
        {
            int userbyte = Transport.activeTransport.GetOutgoingUserBytes();
            int totalbyte = Transport.activeTransport.GetOutgoingFullBytes();

            if (Time.time >= timeTmp + 1)
            {
                userbytePerSec = userbyte - userbyteTmp;
                totalbytePerSec = totalbyte - totalbyteTmp;

                userbyteTmp = userbyte;
                totalbyteTmp = totalbyte;
                timeTmp = Time.time;
            }
            Detail = "Server Total byte (" + totalbyte + "):" + totalbytePerSec + "byte/sec | User total byte (" + userbyte + "):" + userbytePerSec + "byte/sec";
        }
        else
        {
            Detail = "";
        }
        */
    }

    private void OnGUI()
    {
        if (!ShowGUI)
            return;

        GUILayout.Label(Detail);
    }
}
