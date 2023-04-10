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
            GameNetwork.singleton.StartHostSolo(level);
        }

        if (gametype == UnitZGameType.HostOnline)
        {
            Debug.Log("Host Game");
            GameNetwork.singleton.StartHost(level, true);
        }

        if (gametype == UnitZGameType.HostLan)
        {
            Debug.Log("Host Game");
            GameNetwork.singleton.StartHost(level, false);
        }

        if (gametype == UnitZGameType.Connect)
        {
            Debug.Log("Connect Game");
            GameNetwork.singleton.JoinGame("localhost", 7777);
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

        GameNetwork.singleton.Disconnect();
        UnitZ.aiManager.Clear();
    }

    public void StartLoadLevel(string level)
    {
        loadingScene = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(level);
    }
}
