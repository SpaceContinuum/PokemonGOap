using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class GAction : GBase {

    // Name of the action
    public string actionName = "Action";

    // Cost of the action
    private float _cost = 1.0f;

    public virtual float cost {
        get { return _cost;}
        set { _cost = value;}
    }

    // Target where the action is going to take place
    public GameObject target;
    // Store the tag
    public string targetTag;
    // Duration the action should take
    public float duration = 0.0f;
    // An array of WorldStates of preconditions
    public WorldState[] preConditions;
    
    // An array of WorldStates of afterEffects
    public WorldState[] afterEffects;
    // The NavMEshAgent attached to the agent
    public NavMeshAgent agent;
    // Dictionary of preconditions
    public Dictionary<string, int> preconditions;
    // Dictionary of effects
    public Dictionary<string, int> effects;
    // State of the agent
    public WorldStates agentBeliefs;
    // Access our inventory
    public GInventory inventory;
    public WorldStates beliefs;
    // Are we currently performing an action?
    public bool running = false;

    // Constructor
    public GAction() {

        // Set up the preconditions and effects
        preconditions = new Dictionary<string, int>();
        effects = new Dictionary<string, int>();
    }

    protected void Awake() {

        // Get hold of the agents NavMeshAgent
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;

        // Check if there are any preConditions in the Inspector
        // and add to the dictionary
        if (preConditions != null) {

            foreach (WorldState w in preConditions) {

                // Add each item to our Dictionary
                preconditions.Add(w.key, w.value);
            }
        }

        // Check if there are any afterEffects in the Inspector
        // and add to the dictionary
        if (afterEffects != null) {

            foreach (WorldState w in afterEffects) {
                try {
                // Add each item to our Dictionary
                effects.Add(w.key, w.value);
                }
                catch (ArgumentException e) {
                    Debug.Log(gameObject.name + " exception trying to add " + w.key + " in action " + actionName);
                }
            }
        }
        // Populate our inventory
        inventory = this.GetComponent<GAgent>().inventory;
        // Get our agents beliefs
        beliefs = this.GetComponent<GAgent>().beliefs;
    }

    public virtual bool IsAchievable() {

        return true;
    }

    //check if the action is achievable given the condition of the
    //world and trying to match with the actions preconditions
    public bool IsAchievableGiven(Dictionary<string, int> conditions) {

        foreach (KeyValuePair<string, int> p in preconditions) {

            if (!conditions.ContainsKey(p.Key)) {

                return false;
            }
        }
        return true;
    }

    public void MoveToTarget(Transform trgt, float distance)
    {
        Vector3 buffer = new Vector3(distance, distance, 0);
        agent.SetDestination(trgt.position + buffer);
    }

    public void TriggerAnimation(string type, float time)
    {
        //TODO:
    }

    public abstract bool PrePerform();
    public abstract bool PostPerform();
    public abstract void Reset();

    protected float PathLength(NavMeshPath path) {
        if (path.corners.Length < 2)
            return 0;
        
        Vector3 previousCorner = path.corners[0];
        float lengthSoFar = 0.0F;
        int i = 1;
        while (i < path.corners.Length) {
            Vector3 currentCorner = path.corners[i];
            lengthSoFar += Vector3.Distance(previousCorner, currentCorner);
            previousCorner = currentCorner;
            i++;
        }
        return lengthSoFar;

    }
       
}
