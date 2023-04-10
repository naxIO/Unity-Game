using UnityEngine;
using UnityEngine.UI;
using Mirror;
using System.Collections;
using System.Collections.Generic;

public class LevelManager : NetworkBehaviour
{
    public LevelPreset[] LevelPresets;
    public GameObject Preloader;
    private void Update()
    {
        if (Preloader != null)
            Preloader.SetActive(!UnitZ.gameNetwork.IsClientLoadedScene() && UnitZ.gameNetwork.isNetworkActive);
    }
}
