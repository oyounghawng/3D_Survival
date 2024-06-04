using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SlotItem : MonoBehaviour
{
    public ItemBase item;              // Item data
    public Button btn;                 // Click actions
    public Image icon;                 // icon on display
    // public UIInventory inventory;
    // not yet developed
    public int idx;                    // for identifying slots
    public TextMeshProUGUI quantityTxt;// number of stacks in inventory
    public int quantity;

    public virtual void Set()
    {
        icon.gameObject.SetActive(true);
        icon.sprite = item.icon;
        quantityTxt.text = quantity > 1 ? quantity.ToString() : string.Empty;
    }
    public virtual void Clear()
    {
        item = null;
        icon.gameObject.SetActive(false);
        quantityTxt.text = string.Empty;
    }

    public void OnClickBtn()
    {
        
    }
}
