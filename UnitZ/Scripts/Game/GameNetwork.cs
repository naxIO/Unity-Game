using UnityEngine;
using System.Collections.Generic;
using Mirror;

public class GameNetwork : NetworkManager
{
    public GameObject networkSyncObject;
    public string hostPassword = "";
    public int playerNetID = -1;

    public bool IsClientLoadedScene => NetworkClient.ready;

    public void RequestSpawnPlayer(Vector3 position, int connectID, string userID, string username, int characterIndex, string characterKey, byte team, int spawnPoint, NetworkConnectionToClient conn)
    {
        GameObject player = UnitZ.playerManager.InstantiatePlayer(connectID, userID, username, characterKey, characterIndex, team, spawnPoint);
        if (player == null)
        {
            return;
        }

        player.GetComponent<CharacterSystem>().NetID = connectID;
        player.GetComponent<CharacterSystem>().CmdOnSpawned(position);
        NetworkServer.ReplacePlayerForConnection((NetworkConnectionToClient)conn, player, true);
        Debug.Log("Spawn player " + connectID + " info " + characterIndex + " key " + characterKey);
    }

    public GameObject RequestSpawnObject(GameObject gameObj, Vector3 position, Quaternion rotation)
    {
        GameObject obj = Instantiate(gameObj, position, rotation);
        NetworkServer.Spawn(obj);
        return obj;
    }

    public GameObject RequestSpawnItem(GameObject gameObj, int numTag, int num, Vector3 position, Quaternion rotation)
    {
        GameObject obj = Instantiate(gameObj, position, rotation);
        ItemData data = obj.GetComponent<ItemData>();
        data.SetupDrop(numTag, num);
        NetworkServer.Spawn(obj);
        return obj;
    }

public GameObject RequestSpawnBackpack(GameObject gameObj, string backpackData, Vector3 position, Quaternion rotation)
{
    GameObject obj = Instantiate(gameObj, position, rotation);
    ItemBackpack data = obj.GetComponent<ItemBackpack>();
    data.SetDropItem(backpackData);
    NetworkServer.Spawn(obj);
    return obj;
}

public void HostGame(string levelName, bool online)
{
    onlineScene = levelName;
    StartHost();
    Debug.Log("Host game Max " + maxConnections);
}

public void JoinGame(string ipAddress)
{
    networkAddress = ipAddress;
    StartClient();
}


public void Disconnect()
{
    StopHost();
}
}
