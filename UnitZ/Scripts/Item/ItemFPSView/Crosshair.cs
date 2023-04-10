﻿// HELLO THERE

using UnityEngine;
using System.Collections;

public class Crosshair : MonoBehaviour
{
	
	private FPSController fpsController;
	public Texture2D CrosshairImg;
	public Texture2D CrosshairZoomImg;
	public Texture2D CrosshairHit;

	public float HitDuration = 0.2f;
	private float timeTemp = 0;

	void Start ()
	{
		if (this.transform.root) {
			fpsController = this.transform.root.GetComponent<FPSController> ();
		} else {
			fpsController = this.transform.GetComponent<FPSController> ();
		}
	}

	public void Hit ()
	{
		timeTemp = Time.time;
	}

	void Update ()
	{
	
	}

	void OnGUI ()
	{
		if (fpsController) {
			if (fpsController.zooming) {
				if (CrosshairZoomImg) {
					GUI.DrawTexture (new Rect (Screen.width / 2 - CrosshairZoomImg.width / 2, Screen.height / 2 - CrosshairZoomImg.height / 2, CrosshairZoomImg.width, CrosshairZoomImg.height), CrosshairZoomImg);
                }

            } else {
				if (CrosshairImg) {
					GUI.DrawTexture (new Rect (Screen.width / 2 - CrosshairImg.width / 2, Screen.height / 2 - CrosshairImg.height / 2, CrosshairImg.width, CrosshairImg.height), CrosshairImg);	
				}
			}
		} else {
			if (CrosshairImg) {
				GUI.DrawTexture (new Rect (Screen.width / 2 - CrosshairImg.width / 2, Screen.height / 2 - CrosshairImg.height / 2, CrosshairImg.width, CrosshairImg.height), CrosshairImg);	
			}
		}

		if (Time.time < timeTemp + HitDuration) {
			if (CrosshairHit) {
				GUI.DrawTexture (new Rect (Screen.width / 2 - CrosshairHit.width / 2, Screen.height / 2 - CrosshairHit.height / 2, CrosshairHit.width, CrosshairHit.height), CrosshairHit);	
			}
		}

	}
}
