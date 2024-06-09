using UnityEngine;
using UnityEngine.UI;

public class UI_EquipSlot : UI_ItemSlot
{
    public Image default_icon;
    public override void Set()
    {
        base.Set();
        default_icon.gameObject.SetActive(false);
    }
    public override void Clear()
    {
        base.Clear();
        default_icon.gameObject.SetActive(true);
    }
}