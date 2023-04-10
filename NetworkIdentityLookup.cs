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

    private void OnObjectSpawnStarted(ObjectSpawnStartedMessage message)
    {
        var go = message.prefab;
        if (go == null) return;
        var identity = go.GetComponent<NetworkIdentity>();
        if (identity == null) return;
        RegisterNetworkIdentity(identity);
    }

    private void OnObjectSpawnFinished(ObjectSpawnFinishedMessage message)
    {
        foreach (var identity in message.netIdentities)
        {
            RegisterNetworkIdentity(identity.Value);
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
