using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class NetworkIdentityLookup : MonoBehaviour
{
    public static NetworkIdentityLookup singleton;

    private Dictionary<uint, GameObject> networkIdentities;

    private void Awake()
    {
        if (singleton == null)
        {
            singleton = this;
            networkIdentities = new Dictionary<uint, GameObject>();
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    private void OnEnable()
    {
        NetworkServer.RegisterHandler<ObjectSpawnStartedMessage>(OnObjectSpawnStarted);
        NetworkServer.RegisterHandler<ObjectSpawnFinishedMessage>(OnObjectSpawnFinished);
    }

    private void OnDisable()
    {
        NetworkServer.UnregisterHandler<ObjectSpawnStartedMessage>();
        NetworkServer.UnregisterHandler<ObjectSpawnFinishedMessage>();
    }

    private void OnObjectSpawnStarted(NetworkConnectionToClient conn, ObjectSpawnStartedMessage message)
    {
        var go = message.spawned;
        if (go == null) return;
        var identity = go.GetComponent<NetworkIdentity>();
        if (identity == null) return;
        if (identity.connectionToClient == null || identity.connectionToClient == conn)
        {
            RegisterNetworkIdentity(identity);
        }
    }

    private void OnObjectSpawnFinished(NetworkConnectionToClient conn, ObjectSpawnFinishedMessage message)
    {
        var go = message.spawned;
        if (go == null) return;
        var identity = go.GetComponent<NetworkIdentity>();
        if (identity == null) return;
        if (identity.connectionToClient == null || identity.connectionToClient == conn)
        {
            RegisterNetworkIdentity(identity);
        }
    }

    private void RegisterNetworkIdentity(NetworkIdentity identity)
    {
        networkIdentities[identity.netId] = identity.gameObject;
    }

    public GameObject FindLocalObject(uint netId)
    {
        if (networkIdentities.TryGetValue(netId, out GameObject obj))
        {
            return obj;
        }
        return null;
    }
}
