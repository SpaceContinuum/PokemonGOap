using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SubGoal {

    // Dictionary to store our goals
    public Dictionary<string, int> sGoals;
    // Bool to store if goal should be removed after it has been achieved
    public bool remove;

    // Constructor
    public SubGoal(string s, int i, bool r) {

        sGoals = new Dictionary<string, int>();
        sGoals.Add(s, i);
        remove = r;
    }

    public SubGoal(WorldState.Label label, int i, bool r) : this(label.ToString(), i, r) {}
}

public class GAgent : GBase {

    // Store our list of actions
    public List<GAction> actions = new List<GAction>();
    // Dictionary of subgoals
    public Dictionary<SubGoal, int> goals = new Dictionary<SubGoal, int>();
    // Our inventory
    public GInventory inventory = new GInventory();
    // Our beliefs
    public WorldStates beliefs = new WorldStates();

    // Access the planner
    GPlanner planner;
    // Action Queue
    Queue<GAction> actionQueue;
    public GAction[] actionQueueToArray {
        get {
            if (actionQueue != null) {
                GAction[] result = new GAction[actionQueue.Count];
                actionQueue.CopyTo(result, 0);
                return result;
            }
            else return null;

        }
    }
    // Our current action
    public GAction currentAction;
    // Our subgoal
    SubGoal currentGoal;

    // Start is called before the first frame update
    public void Start() {

        GAction[] acts = this.GetComponents<GAction>();
        foreach (GAction a in acts) {

            actions.Add(a);
        }
    }

    bool invoked = false;
    //an invoked method to allow an agent to be performing a task
    //for a set location
    public void CompleteAction() {

        currentAction.running = false;
        currentAction.PostPerform();
        invoked = false;
    }

    public void Interrupt() {
        CancelInvoke("CompleteAction");
        currentAction = null;
        planner = null;
        actionQueue = null;

    }

    //bool prevHasPath= false;
    void LateUpdate() {

        //Debugging section.
/*
        if (prevHasPath != currentAction.agent.hasPath) {
            Debug.Log(name + " changed hasPath status. CurrentAction: " + currentAction.actionName + ", target: "+ currentAction.target.name);
        }
        prevHasPath = currentAction.agent.hasPath;
  */      

        //if there's a current action and it is still running
        if (currentAction != null && currentAction.running) {

            // Find the distance to the target
            //Interrupt the action if the target has been voided or destroyed.
            if (currentAction.target == null) {
                Debug.Log(name+": My target is gone! I need a new plan.");
                planner = null;
                actionQueue = null;
            }
            else {
                float distanceToTarget = Vector3.Distance(currentAction.target.transform.position, this.transform.position);
                // Check the agent has a goal and has reached that goal
                if (currentAction.agent.hasPath && distanceToTarget < 2.0f) { // currentAction.agent.remainingDistance < 1.0f) 

                    if (!invoked) {

                        //if the action movement is complete wait
                        //a certain duration for it to be completed
                        Debug.Log(name + " running CompleteAction on "+currentAction);
                        Invoke("CompleteAction", currentAction.duration);
                        invoked = true;
                    }
                }
                return;
            }
        }

        // Check we have a planner and an actionQueue
        if (planner == null || actionQueue == null) {

            // If planner is null then create a new one
            planner = new GPlanner();

            // Sort the goals in descending order and store them in sortedGoals
            var sortedGoals = from entry in goals orderby entry.Value descending select entry;

            //look through each goal to find one that has an achievable plan
            foreach (KeyValuePair<SubGoal, int> sg in sortedGoals) {

                actionQueue = planner.plan(actions, sg.Key.sGoals, beliefs);
                // If actionQueue is not = null then we must have a plan
                if (actionQueue != null) {

                    // Set the current goal
                    currentGoal = sg.Key;
                    break;
                }
            }
        }

        // Have we an actionQueue
        if (actionQueue != null && actionQueue.Count == 0) {

            // Check if currentGoal is removable
            if (currentGoal.remove) {

                // Remove it
                goals.Remove(currentGoal);
            }
            // Set planner = null so it will trigger a new one
            planner = null;
        }

        // Do we still have actions
        if (actionQueue != null && actionQueue.Count > 0) {

            // Remove the top action of the queue and put it in currentAction
            currentAction = actionQueue.Dequeue();

            if (currentAction.PrePerform()) {

                // Get our current object
                if (currentAction.target == null && currentAction.targetTag != "") {

                    currentAction.target = GameObject.FindWithTag(currentAction.targetTag);
                }

                if (currentAction.target != null) {

                    // Activate the current action
                    currentAction.running = true;
                    // Pass Unities AI the destination for the agent
                    currentAction.MoveToTarget(currentAction.target.transform,0.5f);
                }
            } else {

                // Force a new plan
                actionQueue = null;
            }
        }
    }

    public void OnMouseOver() {
        Debug.Log(name + " mouse over me.");
    }
}
