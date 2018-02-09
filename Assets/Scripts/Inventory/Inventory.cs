using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : InteractableObject
{
    [Header("Inventory")]
    public Text itemOne;
    public Text itemTwo;

    public bool hasKey;
    public bool hasPlunger;

    public PickableObject key;
    public PickableObject plunger;

    public override void IsActivated()
    {
        base.IsActivated();

        PickableObject pickObject = Gaze.playerSettings.heldItem;
        if (!pickObject) return;
        switch (pickObject.type)
        {
            case PickableObjectType.KEY:
                hasKey = true;
                pickObject.gameObject.SetActive(false);
                key = pickObject;
                Gaze.playerSettings.heldItem = null;
                break;
            case PickableObjectType.PLUNGER:
                hasPlunger = true;
                pickObject.gameObject.SetActive(false);
                plunger = pickObject;
                Gaze.playerSettings.heldItem = null;
                break;
        }
    }

    public override void Update()
    {
        base.Update();

        if (hasKey == false)
        {
            itemOne.color = Color.grey;
        }
        if (hasPlunger == false)
        {
            itemTwo.color = Color.grey;
        }
        if (hasKey == true)
        {
            itemOne.color = Color.black;
        }
        if (hasPlunger == true)
        {
            itemTwo.color = Color.black;
        }
    }
}
