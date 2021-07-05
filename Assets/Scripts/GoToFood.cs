using UnityEngine;

public class GoToFood : GAction
{
    public override bool PrePerform()
    {

        // Test did we acquire a target to go to?
        if (target != null) {

            // Yes we have food
            return true;
        }
        else return false;
    }

    public override bool PostPerform()
    {

        // Inject eating state to world states
        GWorld.Instance.GetWorld().ModifyState("isEating", 1);
        //bool canEat = GWorld.Instance.GetWorld().ClaimFood(target);

        // Inject a state into the agents beliefs
        beliefs.ModifyState("isEating", 1);

        return true;
    }

    public override void Reset()
    {
        //preconditions.Add("ChosenFood", 1);
        preConditions[0] = new WorldState("chosenFood", 1);
        afterEffects[0] = new WorldState("nearFood", 1);
    }
}