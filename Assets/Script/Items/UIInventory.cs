using System;
using TMPro;
using UnityEngine;

public class UIInventory : MonoBehaviour
{
    public UI_ItemSlot[] bagSlots;
    public UI_EquipSlot[] equipSlots;
    public GameObject inventoryBG;
    public Transform dropPosition;
    public Transform itemBag;
    public Transform equip;

    [Header("Selected Item")]
    public SelectedItem selItem;

    private void Start()
    {
        // set the drop position by connecting to player transform

        inventoryBG.gameObject.SetActive(false);
        bagSlots = new UI_ItemSlot[itemBag.childCount];
        for(int a=0; a<itemBag.childCount; a++)
        {
            bagSlots[a] = itemBag.GetChild(a).GetComponent<UI_ItemSlot>();
            bagSlots[a].Idx = a;
            //bagSlots[a].inventory = this;
        }
        equipSlots = new UI_EquipSlot[equip.childCount];
        for(int a=0; a<equip.childCount; a++)
        {
            equipSlots[a] = equip.GetChild(a).GetComponent<UI_EquipSlot>();
            equipSlots[a].Idx = a;
            // equipSlots[a].inventory = this;
        }
    }

}

[Serializable]
public class SelectedItem
{
    public ItemBase item;
    public TextMeshProUGUI name;
    public TextMeshProUGUI desc;
    public TextMeshProUGUI stat;
    public TextMeshProUGUI value;
    public GameObject[] btns;

    public void Clear()
    {
        name.text = string.Empty;
        desc.text = string.Empty;
        stat.text = string.Empty;
        value.text = string.Empty;
        for(int a=0; a<btns.Length; a++)
        {
            btns[a].gameObject.SetActive(false);
        }
    }
}

public enum ButtonType
{
    None,
    UseBtn,
    EquipBtn,
    UnEquipBtn,
    DropBtn
}
