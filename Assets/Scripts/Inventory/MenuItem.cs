using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuItem : InteractableObject
{
    public PickableObjectType type;
    public Inventory inventory;

    public override void IsActivated()
    {
        base.IsActivated();

        switch (type)
        {
            case PickableObjectType.KEY:
                if (inventory.hasKey)
                {
                    inventory.key.gameObject.SetActive(true);
                    inventory.hasKey = false;
                    if (Gaze.playerSettings.heldItem)
                    {
                        Gaze.playerSettings.heldItem.Deactivate();
                    }
                    Gaze.playerSettings.heldItem = inventory.key;
                }
            break;
            case PickableObjectType.PLUNGER:
                if (inventory.hasPlunger)
                {
                    inventory.plunger.gameObject.SetActive(true);
                    inventory.hasPlunger = false;
                    if (Gaze.playerSettings.heldItem)
                    {
                        Gaze.playerSettings.heldItem.Deactivate();
                    }
                    Gaze.playerSettings.heldItem = inventory.plunger;
                }
                break;
        }
    }

}
