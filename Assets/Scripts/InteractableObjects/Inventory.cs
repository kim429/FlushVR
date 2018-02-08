using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class InventoryObject
{
    public int id;
    public InteractableObject IObject;
    public RectTransform element;
}

public class Inventory : InteractableObject {
    [Header("Inventory")]
    public Canvas canvas;
    public List<InventoryObject> inventoryObjects = new List<InventoryObject>();
    public GameObject element;

    public override void IsActivated()
    {
        base.IsActivated();
    }

    public override void Update()
    {
        base.Update();
        UpdateCanvas();
    }

    private void UpdateCanvas()
    {
        canvas.GetComponent<RectTransform>().sizeDelta = new Vector3(70, 20 + inventoryObjects.Count * 40);

        int i = 0;
        foreach(InventoryObject IObject in inventoryObjects)
        {
            if (!IObject.element)
            {
                IObject.element = Instantiate(element, canvas.transform).GetComponent<RectTransform>();
            }
            IObject.element.localPosition = new Vector3(0, 0 , 0);
            i++;
        }
    }
}
