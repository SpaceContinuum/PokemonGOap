using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightForFood : GAction
{
    GameObject attackTarget;
    Pokemon p;
    public override bool PostPerform()
    {
        p = GetComponent<Pokemon>();
        GameObject f = inventory.FindItemWithTag("Food");
        /*if (p.IsWinning(target.GetComponent<Pokemon>()))
        {
            // stun opponent
            Debug.Log(p + " won the fight");
            //attackTarget.GetComponent<GAgent>().inventory.RemoveItem(f);
            //gameObject.GetComponent<GAgent>().inventory.AddItem(f);
            Debug.Log(target.GetComponent<Pokemon>() + " is stunned");
            GWorld.Instance.PokemonFighting2Free(gameObject);
            target.GetComponent<Pokemon>().SetStun(true);
            GWorld.Instance.PokemonFighting2Stunned(target);
        }
        else
        {
            Debug.Log(target.GetComponent<Pokemon>() + " won the fight");
            Debug.Log(p + " is stunned");
            
            gameObject.GetComponent<Pokemon>().SetStun(true);
            GWorld.Instance.PokemonFighting2Stunned(gameObject);

            GWorld.Instance.PokemonFighting2Free(target);
        }*/

        beliefs.RemoveState(WorldState.Label.attacking);
        beliefs.RemoveState(WorldState.Label.isViolent);
        inventory.RemoveItem(attackTarget);
        return true;
    }

    public override bool PrePerform()
    {
        p = GetComponent<Pokemon>();
        attackTarget = p.inventory.FindItemWithTag("Pokemon");
        if (attackTarget == null) {
            return false;
        }
        target = attackTarget;
        return true;
    }

    public override void Reset()
    {
        actionName = "FightForFood";
        cost = 3;
        duration = 5;
        preConditions = new WorldState[1];
        preConditions[0] = new WorldState(WorldState.Label.attacking, 0);
        afterEffects = new WorldState[1];
        afterEffects[0] = new WorldState(WorldState.Label.isPeaceful, 0);
        //afterEffects[1] = new WorldState(WorldState.Label.hasFood, 0);

    }
}
