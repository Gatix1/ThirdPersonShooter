using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TotalAmmoUIControl : MonoBehaviour
{
    public TMP_Text bulletCountText;
    public GunController gunControl;
    public GameObject gunLoader;

    // Update is called once per frame
    void Update()
    {
        gunControl = gunLoader.transform.GetChild(0).GetComponent<GunController>();
        bulletCountText.SetText(gunControl.totalAmmo.ToString());
    }
}
