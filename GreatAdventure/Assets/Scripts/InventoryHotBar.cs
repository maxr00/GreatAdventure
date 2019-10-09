using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryHotBar : MonoBehaviour
{
    bool isActive = false;
    public int currentSelectedItemIndex = 0;
    bool canChangeSelection = true;

    public Rect itemStartPos;
    public float widthBetweenItems;
    public GameObject ui_item_prefab;
    public Sprite default_texture;

    private class uiItemData
    {
        public GameObject ui_item_object;
        public string itemName;
    }
    List<uiItemData> current_ui_items;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            if (isActive)
            {
                ClearHotBar();
                isActive = false;
                return;
            }
            else
            {
                isActive = true;
                PopulateItemList();
            }
        }
        if (isActive)
        {
            float horizontalInput = Input.GetAxisRaw("Horizontal");
            if (horizontalInput != 0 && canChangeSelection)
            {
                canChangeSelection = false;
                StartCoroutine(ChangeSelectedItem(current_ui_items.Count, horizontalInput));
            }

            DisplayItems();

            bool itemSelected = Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Mouse0);
            if (itemSelected)
            {
                Item item = Inventory.GetItemFromName(current_ui_items[currentSelectedItemIndex].itemName);
                item.ItemSelectedInInventory();
            }
        }
    }

    IEnumerator ChangeSelectedItem(int optionCount, float horizontalInput)
    {
        if (horizontalInput > 0)
        {
            currentSelectedItemIndex = (currentSelectedItemIndex >= optionCount - 1) ? 0 : currentSelectedItemIndex + 1;
        }
        else if (horizontalInput < 0)
        {
            currentSelectedItemIndex = (currentSelectedItemIndex - 1 < 0) ? optionCount - 1 : currentSelectedItemIndex - 1;
        }
        yield return new WaitForSeconds(0.2f);
        canChangeSelection = true;
    }

    private void ClearHotBar()
    {
        currentSelectedItemIndex = 0;
        for (int i = 0; i < current_ui_items.Count; ++i)
        {
            Destroy(current_ui_items[i].ui_item_object);
        }
    }

    private void PopulateItemList()
    {
        current_ui_items = new List<uiItemData>();
        foreach(KeyValuePair<string, Item> item_pair in Inventory.GetCurrentItems())
        {
            Item item = item_pair.Value;
            uiItemData itemData = new uiItemData();
            itemData.itemName = item.itemName;
            itemData.ui_item_object = Instantiate(ui_item_prefab, GetComponent<Transform>());
            current_ui_items.Add(itemData);
        }
    }

    private void DisplayItems()
    {
        float currentOffset = 0;
        for (int i = 0; i < current_ui_items.Count; ++i)
        {
            GameObject uiObj = current_ui_items[i].ui_item_object;
            Item item = Inventory.GetItemFromName(current_ui_items[i].itemName);

            //if (currentSelectedItemIndex == i) // current item is selected
            //{
            //    uiObj.GetComponent<Image>().color = Color.yellow;
            //}
            uiObj.GetComponent<Image>().sprite = item.itemIcon;

            uiObj.GetComponent<RectTransform>().position = new Vector3(itemStartPos.x + currentOffset, itemStartPos.y, 0);
            uiObj.GetComponent<RectTransform>().sizeDelta = new Vector2(itemStartPos.width, itemStartPos.height);
            currentOffset += widthBetweenItems + itemStartPos.width;
        }
    }
}
