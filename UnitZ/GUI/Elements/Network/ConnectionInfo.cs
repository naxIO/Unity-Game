using UnityEngine;
using UnityEngine.UI;
using Mirror;

public class ConnectionInfo : MonoBehaviour
{
    public InputField PortText;
    public InputField PasswordText;
    public InputField ServerIPText;
    public InputField ServerNameText;
    public InputField ServerNameFilterText;

    void Start()
    {

    }

    void OnEnable()
    {
        NetworkManager manager = NetworkManager.singleton;
        if (manager)
        {
            TelepathyTransport transport = manager.transport as TelepathyTransport;
            if (transport)
            {
                if (PortText)
                    PortText.text = transport.port.ToString();

                if (ServerIPText)
                    ServerIPText.text = manager.networkAddress;
            }

            // You might need to handle ServerNameText and ServerNameFilterText differently,
            // as there are no direct replacements for matchName and HostNameFilter.
            // You can store and manage these custom properties in your own script.

            // There is no serverBindPassword in Mirror. You can create a custom password system
            // using NetworkManager customizations.
        }
    }

    public void SetServerIP(InputField num)
    {
        if (NetworkManager.singleton)
        {
            NetworkManager.singleton.networkAddress = num.text;
        }
    }

    public void SetServerNameFilter(InputField num)
    {
        // Create your custom implementation for setting server name filter.
    }

    public void SetServerName(InputField num)
    {
        // Create your custom implementation for setting server name.
    }

    public void SetPassword(InputField num)
    {
        // Create your custom implementation for setting password.
    }

public void SetPort(InputField num)
{
    if (NetworkManager.singleton)
    {
        TelepathyTransport transport = NetworkManager.singleton.transport as TelepathyTransport;
        if (transport)
        {
            ushort val;
            if (ushort.TryParse(num.text, out val))
            {
                transport.port = val;
            }
        }
    }
}

}
