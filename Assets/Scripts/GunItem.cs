using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Items/Gun Item")]
public class GunItem : ScriptableObject
{
    [Header("Item Information")]
    public string itemName;
    public GameObject itemModel;
}
