using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fight : GAction
{
    GameObject attackTarget;
    Pokemon p;
    bool didWin;
    public new void Start() {
        p = GetComponent<Pokemon>();
    }
    public override bool PostPerform()
    {
        Debug.Log(name + " PostPerform regular Fight");
        if (didWin) {
            GWorld.Instance.PokemonFighting2Free(gameObject);
            beliefs.RemoveState(WorldState.Label.isViolent);
        }
        else {
            GWorld.Instance.PokemonFighting2Stunned(gameObject);
            gameObject.GetComponent<Pokemon>().beliefs.ModifyState(WorldState.Label.isStunned, 1);

        }

        p.anim.SetBool("isFighting", false);
        beliefs.RemoveState(WorldState.Label.attacking);
        return true;
    }

    public override bool PrePerform()
    {

        //combat resolution happens at start of the action, to ensure that everything is synced up.
        //If it happened in PostPerform, there's be a race condition between the Defender and the Attacker to do cleanup.
        // This way, the Attacker triggers all the cleanup actions.
        Debug.Log(name + " PrePerform regular Fight");
        target = inventory.FindItemWithTag("Pokemon");
        if (target == null)
        {
            Debug.Log(gameObject.name + " is trying to fight without a target");
            return false;
        }
        p.anim.SetBool("isFighting", true);
        inventory.RemoveItem(target);

        didWin = p.IsWinning(target.GetComponent<Pokemon>());
        
        if (didWin)
        {
            // stun opponent
            target.GetComponent<Pokemon>().beliefs.ModifyState(WorldState.Label.isStunned, 1);
            //loot them
            p.Loot(target.GetComponent<Pokemon>());
            //beliefs.ModifyState(WorldState.Label.isPeaceful, 1);
        }
        else
        {
            //they loot us
            target.GetComponent<Pokemon>().Loot(p);

        }



        return true;
    }

    public override void Reset()
    {
        actionName = "Fight";
        cost = 1;
        duration = 5;
        preConditions = new WorldState[1];
        preConditions[0] = new WorldState(WorldState.Label.attacking, 0);
        afterEffects = new WorldState[1];
        afterEffects[0] = new WorldState(WorldState.Label.isPeaceful, 0);

    }
}
