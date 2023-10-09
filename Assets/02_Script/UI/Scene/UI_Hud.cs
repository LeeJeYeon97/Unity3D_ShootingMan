using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Hud : MonoBehaviour
{
    Collider coll;
    PlayerController player;
    WeaponController weapon;
    public Slider hpBar;
    public Slider ammoBar;

    private void Start()
    {
        weapon = transform.parent.GetComponent<WeaponController>();
        player = transform.parent.GetComponent<PlayerController>();
        coll = transform.parent.GetComponent<Collider>();
        transform.position = new Vector3(0, coll.bounds.size.y + 3.0f, 0);
    }
    private void Update()
    {
        hpBar.value = player.CurHp / player.MaxHp;
        ammoBar.value = weapon.CurAmmo / (float)weapon.MaxAmmo;
        transform.rotation = Camera.main.transform.rotation;
    }

}
