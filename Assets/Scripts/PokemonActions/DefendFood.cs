//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

////TODO: current implemnentation means defending pokemon will always lose.

//public class DefendFood : GAction
//{
//    Pokemon p;
//    GameObject attacker;
//    public override bool PostPerform()
//    {
//        //GameObject f = inventory.FindItemWithTag("Food");

//        beliefs.RemoveState(WorldState.Label.isDefensive);
//        inventory.RemoveItem(attacker);
//        if (p.IsWinning(target.GetComponent<Pokemon>()))
//        {
//            // stun opponent
//            //gameObject.GetComponent<GAgent>().inventory.AddItem(f);
//            Debug.Log(p + " won the fight");
//            Debug.Log(target.GetComponent<Pokemon>() + " is stunned");
//            GWorld.Instance.PokemonFighting2Free(gameObject);
//            target.GetComponent<Pokemon>().SetStun(true);
//            GWorld.Instance.PokemonFighting2Stunned(target);
//        }
//        else
//        {
//            //gameObject.GetComponent<GAgent>().inventory.RemoveItem(f);
//            Debug.Log(target.GetComponent<Pokemon>() + " won the fight");
//            Debug.Log(p + " is stunned");

//            gameObject.GetComponent<Pokemon>().SetStun(true);
//            GWorld.Instance.PokemonFighting2Stunned(gameObject);

//            //GWorld.Instance.PokemonFighting2Free(target);
//        }
//        target = null;
//        return true;
//    }

//    public override bool PrePerform()
//    {
//        p = gameObject.GetComponent<Pokemon>();
//        attacker = inventory.FindItemWithTag("Pokemon");
//        /*GameObject f = inventory.FindItemWithTag("Food");
//        if (f == null)
//        {
//            //where's the food we were supposed to have?
//            Debug.Log(name + " can't find food in inventory");
//            beliefs.RemoveState(WorldState.Label.isDefensive);
//            return false;
//        }*/
//        if (attacker == null || p.GetOpponent() == null)
//        {
//            //where's our attacker? that's odd.
//            Debug.Log(name + " can't find attacker object");
//            beliefs.RemoveState(WorldState.Label.isDefensive);
//            GWorld.Instance.PokemonFighting2Free(gameObject);
//            return false;
//        }
//        target = attacker;
//        return true;
        
//    }

//    public override void Reset()
//    {
//        actionName = "DefendFood";
//        duration = 3;
//        cost = 1; //this should always be a priority since there's no alternative.
//        preConditions = new WorldState[1];
//        preConditions[0] = new WorldState(WorldState.Label.isDefensive, 0);
//        //preConditions[1] = new WorldState(WorldState.Label.hasFood, 0);
//        afterEffects = new WorldState[1];
//        afterEffects[0] = new WorldState(WorldState.Label.isPeaceful, 0);

//    }

//}
