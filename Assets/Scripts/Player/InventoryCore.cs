using Microsoft.Unity.VisualStudio.Editor;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Image = UnityEngine.UI.Image;

public class InventoryCore : MonoBehaviour
{
    public GameObject inventoryUI;
    
    public static InventoryCore Instance;
    public List<Item> Items = new List<Item>();

    public Transform itemContent;
    public GameObject inventoryItem;

    private void Awake()
    {
        Instance = this;
        ListItems();
    }

    public void Add(Item item)
    {
        Items.Add(item);
        ListItems();
    }

    public void Remove(Item item)
    {
        Items.Remove(item);
    }

    public void openInventory()
    {
        if (inventoryUI.activeInHierarchy)
        {
            inventoryUI.SetActive(false);
        }

        else
        {
            inventoryUI.SetActive(true);
        }

    }

    public void ListItems()
    {
        foreach (Transform item in itemContent)
        {
            Destroy(item.gameObject);
        }

        foreach (var item in Items)
        {
            GameObject obj = Instantiate(inventoryItem, itemContent);
            var itemName = obj.transform.Find("ItemName").GetComponent<TMP_Text>();
            var itemIcon = obj.transform.Find("ItemIcon").GetComponent<Image>();

            itemName.text = item.itemName;
            itemIcon.sprite = item.icon;
        }
    }
}
