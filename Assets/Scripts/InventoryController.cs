using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryController : MonoBehaviour
{
    public GameObject pistolObject;
    public GameObject smgObject;
    public GameObject akObject;
    public RawImage pistolIcon;
    public RawImage smgIcon;
    public RawImage akIcon;
    public AmmoUIControl ammoUIControl;
    public TotalAmmoUIControl totalAmmoUIControl;
    public GunLoader gunLoader;
    

    public GunItem weapon1;
    public GunItem weapon2;
    public GunItem weapon3;

    [Header("Current Weapon")]
    public GunItem gun;

    void Awake()
    {
        LoadWeaponLoaderSlots();
    }

    void Start()
    {
        Debug.Log("YES");
        LoadCurrentWeapon();
    }

    private void LoadWeaponLoaderSlots()
    {
        gunLoader = GetComponentInChildren<GunLoader>();
        
    }

    private void LoadCurrentWeapon()
    {
        gunLoader.LoadWeaponModel(gun);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            selectPistol();
        }
        if(Input.GetKeyDown(KeyCode.Alpha2))
        {
            selectSMG();
        }
        if(Input.GetKeyDown(KeyCode.Alpha3))
        {
            selectAK();
        }
    }
    private void selectPistol()
    {
        gunLoader.LoadWeaponModel(weapon1);
        pistolIcon.color = new Color32(62,141,255,255);
        smgIcon.color = new Color32(255,255,255,255);
        akIcon.color = new Color32(255,255,255,255);
    }
    private void selectSMG()
    {
        gunLoader.LoadWeaponModel(weapon2);
        pistolIcon.color = new Color32(255,255,255,255);
        smgIcon.color = new Color32(62,141,255,255);
        akIcon.color = new Color32(255,255,255,255);
    }
    private void selectAK()
    {
        gunLoader.LoadWeaponModel(weapon3);
        pistolIcon.color = new Color32(255,255,255,255);
        smgIcon.color = new Color32(255,255,255,255);
        akIcon.color = new Color32(62,141,255,255);
    }
}
