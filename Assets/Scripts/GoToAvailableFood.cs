using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoToAvailableFood : GAction
{
    public override bool PostPerform()
    {
        //grab the food you're looking at and remove it from availability
        bool possession = GWorld.Instance.ClaimFood(target.GetComponent<Food>());
        if (possession) {
            inventory.AddItem(target);
            return true;
        }
        else {
            target = null;
            return false; //could not secure the food
        }
    }

    public override bool PrePerform()
    {
        
        target = GWorld.Instance.GetClosestFreeFood(gameObject).gameObject;
        if (target == null) {
                return false;
        }
        return true;
    }

    public override void Reset()
    {
        cost = 1;
        actionName = "FindAvailableFood";
        
        preConditions = new WorldState[1];
        preConditions[0] = new WorldState("availableFood", 0);
        afterEffects = new WorldState[1];
        afterEffects[0] = new WorldState("hasFood", 0);
 
    }
}
