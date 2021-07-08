using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fight : GAction
{
    GameObject attackTarget;
    Pokemon p;
    public override bool PostPerform()
    {
        p = GetComponent<Pokemon>();
        if (p.IsWinning(attackTarget.GetComponent<Pokemon>()))
        {
            // stun opponent
            //loot them
            p.Loot(attackTarget.GetComponent<Pokemon>());
            GWorld.Instance.PokemonFighting2Free(gameObject);
        }
        else
        {
            gameObject.GetComponent<Pokemon>().SetStun(true);
            //they loot us
            attackTarget.GetComponent<Pokemon>().Loot(p);
            GWorld.Instance.PokemonFighting2Stunned(gameObject);
        }

        beliefs.RemoveState(WorldState.Label.attacking);
        beliefs.RemoveState(WorldState.Label.isViolent);

        return true;
    }

    public override bool PrePerform()
    {
        attackTarget = inventory.FindItemWithTag("Pokemon");
        if (attackTarget == null)
        {
            Debug.Log(gameObject.name + " is trying to fight without a target");
            return false;
        }
        inventory.RemoveItem(attackTarget);
        return true;
    }

    public override void Reset()
    {
        actionName = "Fight";
        cost = 3;
        duration = 5;
        preConditions = new WorldState[1];
        preConditions[0] = new WorldState(WorldState.Label.attacking, 0);
        afterEffects = new WorldState[1];
        afterEffects[0] = new WorldState(WorldState.Label.isPeaceful, 0);

    }
}
