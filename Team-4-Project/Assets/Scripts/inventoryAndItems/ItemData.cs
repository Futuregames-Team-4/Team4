using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ItemData : MonoBehaviour
{
    public string itemName;
    public Sprite icon;
    [TextArea]
    public string description;

    public string itemName2;
    public Sprite icon2;
    [TextArea]
    public string description2;
}
