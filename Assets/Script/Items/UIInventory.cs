using System;
using TMPro;
using UnityEngine;

public class UIInventory : MonoBehaviour
{
    public SlotItem[] bagSlots;
    public SlotEquip[] equipSlots;
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
        bagSlots = new SlotItem[itemBag.childCount];
        for(int a=0; a<itemBag.childCount; a++)
        {
            bagSlots[a] = itemBag.GetChild(a).GetComponent<SlotItem>();
            bagSlots[a].idx = a;
            //bagSlots[a].inventory = this;
        }
        equipSlots = new SlotEquip[equip.childCount];
        for(int a=0; a<equip.childCount; a++)
        {
            equipSlots[a] = equip.GetChild(a).GetComponent<SlotEquip>();
            equipSlots[a].idx = a;
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
