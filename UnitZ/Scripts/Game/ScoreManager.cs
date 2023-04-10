﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Mirror;

public class ScoreManager : NetworkBehaviour
{

	public bool Toggle;
	private GUIKillBadgeManager guiBadgeManager;

	void Start ()
	{
		guiBadgeManager = (GUIKillBadgeManager)GameObject.FindObjectOfType (typeof(GUIKillBadgeManager));

	}

	void Update ()
	{
		if (Input.GetKeyDown (KeyCode.Tab)) {
			if (UnitZ.gameManager)
				Toggle = !Toggle;
		}
	}

	public void UpdatePlayerScore (int id, int score, int dead)
	{
		if (!isServer || UnitZ.NetworkObject () == null || UnitZ.NetworkObject ().playersManager == null)
			return;

		UnitZ.NetworkObject ().playersManager.AddScore(id,score,dead);
	}

	public void AddScore (int score, int id)
	{
		UpdatePlayerScore (id, score, 0);	
	}

	public void AddDead (int dead, int id)
	{
		UpdatePlayerScore (id, 0, dead);
	}

	public void AddKillText (int killer, int victim, string killtype)
	{
		if(guiBadgeManager == null)
			guiBadgeManager = (GUIKillBadgeManager)GameObject.FindObjectOfType (typeof(GUIKillBadgeManager));

		if (UnitZ.NetworkObject () == null || UnitZ.NetworkObject ().playersManager == null)
			return;
		
		PlayersManager playersManager = UnitZ.NetworkObject ().playersManager;

		if (guiBadgeManager != null && playersManager) {
			//Debug.Log("add killer22");
			PlayerData killerData = playersManager.GetPlayerData (killer);
			PlayerData victimData = playersManager.GetPlayerData (victim);
			string killername = "N/A";
			string victimname = "N/A";

			if (killerData.Name != "") {
				killername = killerData.Name;
			}
			if (victimData.Name != "") {
				victimname = victimData.Name;
			}

			guiBadgeManager.PushKillText (killername, victimname, killtype);
		}
	}


}
