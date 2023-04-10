﻿using UnityEngine;
using System.Collections;
using Mirror;


public class GameMenu : MonoBehaviour
{
	// NOTE! ********************************************************
	// This is an older script for older gui, so nothing to do here.
	// **************************************************************
	
	public GUISkin skin;
	public Texture2D BG;
	public bool Toggle;

	void Start ()
	{
		
	}

	void Update ()
	{
		if (Input.GetKeyDown (KeyCode.Escape)) {
			Toggle = !Toggle;
			MouseLock.MouseLocked = !Toggle;
		}
	}
	
	void OnGUI ()
	{
		if (!Toggle)
			return;

		if (skin)
			GUI.skin = skin;	
		
		if (BG)
			GUI.DrawTexture (new Rect (0, 0, Screen.width, Screen.height), BG);
		
		if (GUI.Button (new Rect (Screen.width / 2 - 130, Screen.height / 2 - 80, 260, 50), "Resume")) {
			Toggle = false;
			MouseLock.MouseLocked = true;
		}
		
		if (GUI.Button (new Rect (Screen.width / 2 - 130, Screen.height / 2 - 20, 260, 50), "Disconnect")) {
			if (UnitZ.gameManager) {
				UnitZ.gameManager.QuitGame ();
			}
			MouseLock.MouseLocked = false;
		}
		
		GUI.skin.label.fontSize = 50;
		GUI.skin.label.alignment = TextAnchor.MiddleCenter;
		GUI.skin.label.normal.textColor = Color.white;
		GUI.Label (new Rect (0, Screen.height / 2 - 180, Screen.width, 30), "Main menu");
	
	}
}
