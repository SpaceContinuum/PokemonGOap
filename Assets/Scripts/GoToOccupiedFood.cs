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

            Pokemon other = owner.GetComponent<Pokemon>();
            if (other == null) {
                Debug.Log(name + " trying to attack a non-pokemon " + owner.name);
                return false;
            }
            //Declare attack on opponent
            Debug.Log(gameObject.name + " attacking " + other.name + " for " + target.name);
            other.Interrupt();
            other.beliefs.ModifyState("underAttack",1);
            other.inventory.AddItem(gameObject);
            beliefs.ModifyState("attacking", 1);
            inventory.AddItem(other.gameObject);

            return true;
        }

        return false;
    }
/*
    public new int cost {
        get {
            Food f = GWorld.Instance.GetClosestEatenFood(gameObject);
            NavMeshPath path = new NavMeshPath();

            agent.CalculatePath(f.transform.position, path);

            //cost is length of the path + slight factor for fight.
            //TODO: replace config constant with relative strength calculation
            return config.OccupiedFoodCostFactor+(int)(PathLength(path));
            }
    }*/
    public override void Reset()
    {
        duration = 10;
        cost = 5;
        actionName = "GoToOccupiedFood";
        preConditions = new WorldState[1];
        preConditions[0] = new WorldState("foodEaten", 0);
        //preConditions[1] = new WorldState("isHungry", 0);
        afterEffects = new WorldState[1];
        afterEffects[0] = new WorldState("attacking", 0);
    }

   
}
