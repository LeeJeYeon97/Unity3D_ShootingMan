using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_Popup_Matching : UI_Popup
{
    enum Texts
    {
        Text_Info,
    }

    enum Buttons
    {
        Button_Close,
        Button_Exit,
    }

    private Button[] btn_Exits;
    private TextMeshPro text_Info;

    public override void Init()
    {
        base.Init();
        Bind<TextMeshPro>(typeof(Texts));
        Bind<Button>(typeof(Buttons));

        SetCloseButton();
        text_Info = Get<TextMeshPro>((int)Texts.Text_Info);
    }

    private void SetCloseButton()
    {
        //PhotonManager photon = GameObject.Find("@PhotonManager").GetComponent<PhotonManager>();
        //int length = System.Enum.GetValues(typeof(Buttons)).Length;
        //
        //btn_Exits = new Button[length];
        //
        //for (int i = 0; i <btn_Exits.Length ; i++)
        //{
        //    Button btn_Exit = GetButton(i);
        //
        //    btn_Exit.onClick.AddListener(() =>
        //    {
        //        photon.LeaveRoom();
        //
        //        ClosePopupUI();
        //    });
        //}
    }
}
