using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//TODO: current implemnentation means defending pokemon will always lose.

public class Defend : GAction
{
    Pokemon p;
    GameObject attacker;
    public override bool PostPerform()
    {
        if (beliefs.HasState(WorldState.Label.isStunned)) { //this means we lost
            GWorld.Instance.PokemonFighting2Stunned(gameObject);
        }
        else
        {
            GWorld.Instance.PokemonFighting2Free(gameObject);
        }

        beliefs.RemoveState(WorldState.Label.isDefensive);
        p.anim.SetBool("isFighting", false);
        return true;
    }

    public override bool PrePerform()
    {
        p = gameObject.GetComponent<Pokemon>();
        target = inventory.FindItemWithTag("Pokemon");
        if (target == null)
        {
            //where's our attacker? that's odd.
            Debug.Log(name + " can't find attacker object");
            beliefs.RemoveState(WorldState.Label.isDefensive);
            GWorld.Instance.PokemonFree2Fighting(gameObject);
            return false;
        }

        inventory.RemoveItem(target);
        p.anim.SetBool("isFighting", true);
        return true;

    }

    public override void Reset()
    {
        actionName = "Defend";
        duration = 3;
        cost = 1; //this should always be a priority since there's no alternative.
        preConditions = new WorldState[1];
        preConditions[0] = new WorldState(WorldState.Label.isDefensive, 0);
        afterEffects = new WorldState[1];
        afterEffects[0] = new WorldState(WorldState.Label.isPeaceful, 0);

    }

}
