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
        if (p.IsWinning(attacker.GetComponent<Pokemon>()))
        {
            GWorld.Instance.PokemonFighting2Free(gameObject);
        }
        else
        {
            gameObject.GetComponent<Pokemon>().SetStun(true);
            GWorld.Instance.PokemonFighting2Stunned(gameObject);
        }

        beliefs.RemoveState(WorldState.Label.isDefensive);

        return true;
    }

    public override bool PrePerform()
    {
        p = gameObject.GetComponent<Pokemon>();
        attacker = inventory.FindItemWithTag("Pokemon");
        if (attacker == null || p.GetOpponent() == null)
        {
            //where's our attacker? that's odd.
            Debug.Log(name + " can't find attacker object");
            beliefs.RemoveState(WorldState.Label.isDefensive);
            GWorld.Instance.PokemonFighting2Free(gameObject);
            return false;
        }

        inventory.RemoveItem(attacker);
        return true;

    }

    public override void Reset()
    {
        actionName = "Defend";
        duration = 3;
        cost = 0; //this should always be a priority since there's no alternative.
        preConditions = new WorldState[2];
        preConditions[0] = new WorldState(WorldState.Label.isDefensive, 0);
        afterEffects = new WorldState[1];
        afterEffects[0] = new WorldState(WorldState.Label.isPeaceful, 0);

    }

}
