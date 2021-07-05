using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GoToAvailableFood : GAction
{
    public override bool PostPerform()
    {
        Debug.Log(gameObject.name + " has reached food");
        //grab the food you're looking at and remove it from availability
        bool possession = GWorld.Instance.ClaimFood(target.GetComponent<Food>());
        if (possession) {
            Debug.Log(gameObject.name + " has claimed " + target.name);
            target.GetComponent<Food>().owner = gameObject;
            inventory.AddItem(target);
            return true;
        }
        else {
            Debug.Log(gameObject + " could not claim " + target.name);
            target = null;
            return false; //could not secure the food
        }
    }

    public new int cost { 
        get {
            Food f = GWorld.Instance.GetClosestFreeFood(gameObject);
            NavMeshPath path = new NavMeshPath();

            agent.CalculatePath(f.transform.position, path);
            return (int)(PathLength(path));
        }
    }
    public override bool PrePerform()
    {
        target = GWorld.Instance.GetClosestFreeFood(gameObject).gameObject;
        if (target == null) {
                Debug.Log(gameObject.name + " couldn't find food");    
                return false;
        }
        Debug.Log(gameObject.name + " is going towards " + target.name);
        
        return true;
    }

    public override void Reset()
    {
        duration = 10;
        actionName = "FindAvailableFood";
        
        preConditions = new WorldState[2];
        preConditions[0] = new WorldState("availableFood", 0);
        preConditions[1] = new WorldState("isHungry", 0);
        afterEffects = new WorldState[1];
        afterEffects[0] = new WorldState("hasFood", 0);
 
    }
}
