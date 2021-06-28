public class Pokemon : GAgent
{
    private int hungerMeter = 100;

    new void Start()
    {

        // Call the base start
        base.Start();
        // Set up the subgoal "isEating"
        SubGoal s1 = new SubGoal("isEating", 1, true);
        // Add it to the goals
        goals.Add(s1, 3);

        // Set up the subgoal "isFighting"
        SubGoal s2 = new SubGoal("isFighting", 1, true);
        // Add it to the goals
        goals.Add(s2, 5);

        // Set up the subgoal "isHiding"
        SubGoal s3 = new SubGoal("isHiding", 1, true);
        // Add it to the goals
        goals.Add(s3, 1);
    }

}