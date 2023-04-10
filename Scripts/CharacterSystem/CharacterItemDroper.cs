﻿using UnityEngine;
using Mirror;
using System.Collections;

[RequireComponent (typeof(CharacterSystem))]
public class CharacterItemDroper : NetworkBehaviour
{
	
	public GameObject Backpack;
	CharacterSystem character;

	void Start ()
	{
		character = this.GetComponent<CharacterSystem> ();
	}

	void Update ()
	{
	
	}

	[Command(channel=0)]
	void CmdDropItem (string itemdata)
	{
		if (Backpack) {
			UnitZ.gameNetwork.RequestSpawnBackpack (Backpack.gameObject, itemdata, this.transform.position, Quaternion.identity);
		}
	}

	public void DropItem ()
	{
		if (isLocalPlayer) {
			if (character != null && character.inventory != null) {
				CmdDropItem (character.inventory.GetItemDataText ());
			}
		}
	}
}
