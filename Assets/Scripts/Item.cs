using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
    public int ID;
    public string Name;

    // 아이템을 주운상태다 true, 아니면 false
    public bool isPickedUp = false;

    void Start()
    {
        GameObject parent = transform.parent?.gameObject;
        // 부모가 없다면 떨어져있는 아이템,아니라면 UI
        if (parent == null)
        {
            isPickedUp = false;
        }
        else
        {
            isPickedUp = true;
        }
    }

    public virtual void UseItem()
    {
        Debug.Log("Using item " + Name);
    }

    public virtual void PickUp()
    {
        Sprite itemIcon = GetComponent<Image>().sprite;
        if (ItemPickupUIController.Instance != null)
        {
            ItemPickupUIController.Instance.ShowItemPickup(Name, itemIcon);
        }
    }
}
