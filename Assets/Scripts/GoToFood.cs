using UnityEngine;

public class GoToFood : GAction
{
    GameObject resource;
    public override bool PrePerform()
    {

        // Grab available food and remove it from the list
        //resource = GWorld.Instance.GetClosestFood(gameObject);
        // Test did we get one?
        if (resource != null) {

            // Yes we have food
            inventory.AddItem(resource);
            target = resource;
            return true;
        }
        else return false;
    }

    public override bool PostPerform()
    {

        // Inject eating state to world states
        GWorld.Instance.GetWorld().ModifyState("Eating", 1);
        // Patient adds himself to the queue
        GWorld.Instance.AddPatient(this.gameObject);
        // Inject a state into the agents beliefs
        beliefs.ModifyState("lessHungry", 1);

        return true;
    }
}