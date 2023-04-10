using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Mirror;

public enum UnitZGameType
{
    Connect, HostOnline, HostLan, Single
}

public class MainMenuManager : PanelsManager
{
    public string SceneStart = "zombieland";
    public Text CharacterName;
    public GameObject Preloader;
    [HideInInspector]
    public UnitZGameType StartType = UnitZGameType.Single;
    private CharacterCreatorCanvas characterCreator;
    private NetworkManager networkManager;

    void Start()
    {
        networkManager = FindObjectOfType<NetworkManager>();
        MouseLock.MouseLocked = false;
        characterCreator = (CharacterCreatorCanvas)GameObject.FindObjectOfType(typeof(CharacterCreatorCanvas));
        // load latest scene played
        if (PlayerPrefs.GetString("StartScene") != "")
        {
            SceneStart = PlayerPrefs.GetString("StartScene");
        }
    }

    void Update()
    {
        if (CharacterName && UnitZ.gameManager)
        {
            CharacterName.text = UnitZ.gameManager.UserName;
        }
    }

    public void LevelSelected(string name)
    {
        SceneStart = name;
        // save scene for the next time.
        PlayerPrefs.SetString("StartScene", SceneStart);
    }

    public void StopFindGame()
    {
        if (networkManager)
            networkManager.StopHost();
    }

    public void ConnectIP()
    {
        StartType = UnitZGameType.Connect;
        OpenPanelByName("LoadCharacter");
    }

    public void StartSinglePlayer()
    {
        if (UnitZ.gameManager)
        {
            networkManager.StopHost();
            StartType = UnitZGameType.Single;
            OpenPanelByName("LoadCharacter");
        }
    }

    public void HostGameOnline()
    {
        if (networkManager)
        {
            networkManager.StartHost();
            StartType = UnitZGameType.HostOnline;
            OpenPanelByName("LoadCharacter");
        }
    }
    public void HostGame()
    {
        if (networkManager)
        {
            networkManager.StopHost();
            StartType = UnitZGameType.HostLan;
            OpenPanelByName("LoadCharacter");
        }
    }

    public void FindInternetGame()
    {
        networkManager.StartClient();
        OpenPanelByName("FindGame");
    }

    public void EnterWorld()
    {
        if (UnitZ.gameManager)
        {
            if (characterCreator)
                characterCreator.SetCharacter();

            UnitZ.gameManager.StartGame(SceneStart, StartType);
            OpenPanelByName("Connecting");
        }
    }

    public void ConnectingDeny()
    {
        networkManager.StopHost();
    }

    public void ExitGame()
    {
        networkManager.StopHost();
        Application.Quit();
    }
}
