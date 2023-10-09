using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UI_Lobby : UI_Scene
{
    enum Buttons
    {
        Button_Shop,
        Button_Inventory,
        Button_Battle,
        Button_Settings,
    }

    Button battleButton;


    public override void Init()
    {
        Bind<Button>(typeof(Buttons));
        battleButton = GetButton((int)Buttons.Button_Battle);    
    }
    void Start()
    {
        PhotonManager photon = GameObject.Find("@PhotonManager").GetComponent<PhotonManager>();

        battleButton.onClick.AddListener(() =>
        {
            photon.QuickMatch();
        });

    }
}
