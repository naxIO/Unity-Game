using UnityEngine;
using Mirror;
using System.Collections;

public class DamageBase : NetworkBehaviour {
	[SyncVar]
	public int OwnerID;
	[SyncVar]
	public byte OwnerTeam;

}
