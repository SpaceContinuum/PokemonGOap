using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GoToOccupiedFood : GAction
{
    public override bool PrePerform()
    {
        target = GWorld.Instance.GetClosestEatenFood(gameObject).gameObject;
        if (target == null) {
                Debug.Log(name + " was pursuing occupied food but couldn't find any");
                return false;
        }
        Debug.Log(name + " is moving towards "+target.name);
        return true;    }

    public override bool PostPerform()
    {   
        Food f = target.GetComponent<Food>();
        if (target != null && f != null && f.owner != null ) { //food is still there and it is being held by another pokemon!
            GameObject owner = f.owner;

            //Declare attack on opponent
            owner.GetComponent<GAgent>().Interrupt();
        }
    }

    public new int cost {
        get {
            Food f = GWorld.Instance.GetClosestEatenFood(gameObject);
            NavMeshPath path = new NavMeshPath();

            agent.CalculatePath(f.transform.position, path);

            //cost is length of the path + slight factor for fight.
            //TODO: replace config constant with relative strength calculation
            return config.OccupiedFoodCostFactor+(int)(PathLength(path));
            }
    }
    public override void Reset()
    {
        duration = 10;
        actionName = "FindOccupiedFood";
        preConditions = new WorldState[2];
        preConditions[0] = new WorldState("isHungry", 0);
        preConditions[1] = new WorldState("foodEaten", 0);
        afterEffects = new WorldState[1];
        afterEffects[0] = new WorldState("hasFood", 0);
    }

   
}
