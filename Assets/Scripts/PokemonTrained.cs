using UnityEngine;
public class PokemonTrained : Pokemon
{

    public override void SetViolence()
    {
        // Set up the subgoal "isPeaceful"
        SubGoal s2 = new SubGoal(WorldState.Label.isPeaceful, 5, true);
        // Add it to the goals
        goals.Add(s2, 5);

        beliefs.ModifyState("isViolent", 0);
        Debug.Log(gameObject.name + " is violent");
        
        

    }

}