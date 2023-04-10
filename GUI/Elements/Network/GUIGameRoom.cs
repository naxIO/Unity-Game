using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Mirror;

public class ServerInfo
{
    public string address;
}

public class GUIGameRoom : MonoBehaviour
{
    public Text RoomName;
    public ServerInfo Match;
    private NetworkManager networkManager;

    void Start()
    {
        networkManager = FindObjectOfType<NetworkManager>();
    }

    public void JoinRoom()
    {
        if (networkManager)
        {
            // Set the address of the remote server to the match's address
            networkManager.networkAddress = Match.address;

            // Start the client and connect it to the remote server
            networkManager.StartClient();

            MainMenuManager panelsManage = (MainMenuManager)GameObject.FindObjectOfType(typeof(MainMenuManager));
            if (panelsManage)
            {
                panelsManage.StartType = UnitZGameType.Connect;
                panelsManage.OpenPanelByName("LoadCharacter");
            }
        }
    }
}
