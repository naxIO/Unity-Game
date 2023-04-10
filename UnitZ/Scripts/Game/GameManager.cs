using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using UnityEngine.SceneManagement;
using Mirror;

public class GameManager : MonoBehaviour
{
    public string UserName = "";
    public string Team = "";
    public string UserID = "";
    public string CharacterKey = "";
    public int PlayerNetID = -1;

    [HideInInspector]
    public bool IsRefreshing = false;
    [HideInInspector]
    public AsyncOperation loadingScene;

    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    void Start()
    {
        UserName = PlayerPrefs.GetString("user_name");
    }

    public void StartGame(string level, UnitZGameType gametype)
    {
        if (gametype == UnitZGameType.Single)
        {
            Debug.Log("Single Player Game");
            // Remove the call to StartHostSolo
        }

        if (gametype == UnitZGameType.HostOnline || gametype == UnitZGameType.HostLan)
        {
            Debug.Log("Host Game");
            // Use the default StartHost method
            NetworkManager.singleton.StartHost();
        }

        if (gametype == UnitZGameType.Connect)
        {
            Debug.Log("Connect Game");
            // Use the default StartClient method
            NetworkManager.singleton.networkAddress = "localhost";
            NetworkManager.singleton.StartClient();
        }

        PlayerPrefs.SetString("user_name", UserName);
    }

    public void RestartGame()
    {
        if (UnitZ.playerManager != null)
            UnitZ.playerManager.Reset();
    }

    public void QuitGame()
    {
        if (UnitZ.NetworkObject() != null)
            UnitZ.NetworkObject().chatLog.Clear();

        if (UnitZ.playerManager != null)
            UnitZ.playerManager.Reset();

        // Use the default StopHost and StopClient methods
        NetworkManager.singleton.StopHost();
        NetworkManager.singleton.StopClient();

        UnitZ.aiManager.Clear();
    }

    public void StartLoadLevel(string level)
    {
        loadingScene = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(level);
    }
}
