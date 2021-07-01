public class GoToFood : GAction
{
    public override bool PrePerform()
    {

        // Find a free or occupied food
        target = inventory.FindItemWithTag("Food");
        // Check that we did indeed find food
        if (target == null)
            // No food
            return false;
        // There is food
        return true;
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