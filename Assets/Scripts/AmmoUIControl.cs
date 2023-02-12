using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AmmoUIControl : MonoBehaviour
{
    public TMP_Text bulletCountText;
    public GunController gunControl;
    public GameObject gunLoader;

    // Update is called once per frame
    void Update()
    {
        gunControl = gunLoader.transform.GetChild(0).GetComponent<GunController>();
        bulletCountText.SetText(gunControl.ammo.ToString() + "/" + gunControl.maxAmmo.ToString());
    }
}
