﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoToAvailableFood : GAction
{
    public override bool PostPerform()
    {
        Debug.Log(gameObject.name + " has reached food");
        //grab the food you're looking at and remove it from availability
        bool possession = GWorld.Instance.ClaimFood(target.GetComponent<Food>());
        if (possession) {
            Debug.Log(gameObject.name + " has claimed " + target.name);
            inventory.AddItem(target);
            return true;
        }
        else {
            Debug.Log(gameObject + " could not claim " + target.name);
            target = null;
            return false; //could not secure the food
        }
    }

    public override bool PrePerform()
    {
        target = GWorld.Instance.GetClosestFreeFood(gameObject).gameObject;
        if (target == null) {
            Debug.Log(gameObject.name + " is going towards " + target.name);
                return false;
        }
        Debug.Log(gameObject.name + " couldn't find food");
        return true;
    }

    public override void Reset()
    {
        cost = 1;
        duration = 10;
        actionName = "FindAvailableFood";
        
        preConditions = new WorldState[2];
        preConditions[0] = new WorldState("availableFood", 0);
        preConditions[1] = new WorldState("isHungry", 0);
        afterEffects = new WorldState[1];
        afterEffects[0] = new WorldState("hasFood", 0);
 
    }
}
