using UnityEngine;

public class Pokemon : GAgent
{

    new void Start()
    {

        // Call the base start
        base.Start();
        // Set up the subgoal "isFull"
        SubGoal s1 = new SubGoal(WorldState.Label.isFull, 5, false);
        // Add it to the goals
        goals.Add(s1, 3);

        // Set up the subgoal "isFighting"
        SubGoal s2 = new SubGoal("isFighting", 1, false);
        // Add it to the goals
        goals.Add(s2, 5);

    // Set up the subgoal "isHiding"
        SubGoal s3 = new SubGoal("isHiding", 3, false);
        // Add it to the goals
        goals.Add(s3, 1);

        SubGoal s4 = new SubGoal(WorldState.Label.safeFromAttack, 10, false);
        goals.Add(s4,10);

        
        SubGoal s5 = new SubGoal(WorldState.Label.recovered, 10, false);
        goals.Add(s5,10);
        

    }


   
    public void GetHungry() {
        beliefs.ModifyState("isHungry", 0);
        //call the get hungry method over and over at random times to make the Pokemon
        //get hungry again
        Debug.Log(gameObject.name+" is hungry");

        //TODO: move this to the "Eat" function.
        //Invoke("GetHungry", Random.Range(gameConfig.HungerFrequency-gameConfig.HungerVariance, gameConfig.HungerFrequency+gameConfig.HungerVariance));

    }


}