using UnityEngine;

public class Pokemon : GAgent
{

    public enum PokemonType{ NULL, Fire, Water, Grass};
    protected ConfigData gameConfig;
    
    [SerializeField] private Pokemon.PokemonType myType = PokemonType.Grass;
    private Pokemon.PokemonType weaknessType;
    private Pokemon.PokemonType strengthType;

    private Pokemon opponent=null;

    public Animator anim;

    public void Awake() {
        CalculateTypes();

    }

    new void Start()
    {

        
        // Call the base start
        base.Start();
        // Set up the subgoal "isFull"
        //SubGoal s1 = new SubGoal(WorldState.Label.isFull, 3, false);
        // Add it to the goals
        //goals.Add(s1, 3);

        // Set up the subgoal "isFighting"
        //SubGoal s2 = new SubGoal(WorldState.Label.isPeaceful, 5, false);
        // Add it to the goals
        //goals.Add(s2, 5);

        //get hungry in a random time frame
        //Invoke("GetHungry", Random.Range(gameConfig.HungerFrequency-gameConfig.HungerVariance, gameConfig.HungerFrequency+gameConfig.HungerVariance));
        //Invoke("GetHungry", 10f);
        
        //SubGoal s3 = new SubGoal(WorldState.Label.isRecovered, 10, false);
        //goals.Add(s3,10);
        
        
     
        anim = GetComponent<Animator>();
    }

    public void Update() {
        bool animState =  false;
        if (currentAction != null) {
            Vector3 v = currentAction.agent.velocity;
            anim.SetFloat("movementSpeed", v.magnitude);
            animState = (v.magnitude > 0);
        }
        
    }

    private void CalculateTypes()
    {
        switch(myType)
        {
            case PokemonType.Grass:
                GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/spr_bw_001");
                weaknessType = PokemonType.Fire;
                strengthType = PokemonType.Water;
                break;
            case PokemonType.Fire:
                GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/spr_bw_498");
                weaknessType = PokemonType.Water;
                strengthType = PokemonType.Grass;
                break;
            case PokemonType.Water:
                GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/spr_bw_393");
                weaknessType = PokemonType.Grass;
                strengthType = PokemonType.Fire;
                break;
        }
    }

    
    public PokemonType GetMyPokemonType()
    {
        return myType;
    }

    public PokemonType GetMyWeaknessType()
    {
        return weaknessType;
    }

    public PokemonType GetMyStrengthType()
    {
        return strengthType;
    }

    public Pokemon GetOpponent()
    {
        return opponent;
    }

    public bool IsWinning(Pokemon other)
    {
        PokemonType otherType = other.GetMyPokemonType();
        if (otherType == strengthType)
        {
            return true;
        }
        if (otherType == weaknessType)
        {
            return false;
        }
        return Random.value >= 0.5f;
    }

    public void Loot(Pokemon other) {
        GameObject f = other.inventory.FindItemWithTag("Food");
        if (f != null && f.GetComponent<Food>() != null) { //this means our opponent has food !
            other.inventory.RemoveItem(f); //take it from them
            inventory.AddItem(f); //give it to us
            other.beliefs.ModifyState(WorldState.Label.hasFood, -1);
            beliefs.ModifyState(WorldState.Label.hasFood, 1);
        }
    }
    public virtual void SetViolence()
    {
    }

    public void SetDefence()
    {
        SubGoal s1 = new SubGoal(WorldState.Label.isPeaceful, 5, true);
        // Add it to the goals
        AddGoal(s1, 5);
        /*if (!goals.ContainsKey(s1))
        {
            goals.Add(s1, 5);
        }*/

        SubGoal s2 = new SubGoal(WorldState.Label.isRecovered, 10, true);
        // Add it to the goals
        AddGoal(s2, 10);
        /*if (!goals.ContainsKey(s2))
        {
            goals.Add(s2, 10);
        }*/
    }

    public void GetHungry() {
        // Set up the subgoal "isFull"

        SubGoal s1 = new SubGoal(WorldState.Label.isFull, 3, true);
        // Add it to the goals
        AddGoal(s1, 3);
        /*if (!goals.ContainsKey(s1))
        {
            goals.Add(s1, 3);
        }*/

        beliefs.ModifyState(WorldState.Label.isHungry, 1);
        //call the get hungry method over and over at random times to make the Pokemon
        //get hungry again
        Debug.Log(gameObject.name+" is hungry");

        //TODO: move this to the "Eat" function.
        //Invoke("GetHungry", Random.Range(gameConfig.HungerFrequency-gameConfig.HungerVariance, gameConfig.HungerFrequency+gameConfig.HungerVariance));
    }


    public void SetStun(bool state) {
        if (state && !beliefs.HasState(WorldState.Label.isStunned))
        {
            SubGoal s3 = new SubGoal(WorldState.Label.isRecovered, 10, true);
            // Add it to the goals
            AddGoal(s3, 10);
            /*if (!goals.ContainsKey(s3))
            {
                goals.Add(s3, 10);
            }*/

            beliefs.ModifyState(WorldState.Label.isStunned, 0);
            anim.SetBool("isStunned", true);
            //transform.Rotate(new Vector3(0,-90,0));

            
        }
        else {
            beliefs.RemoveState(WorldState.Label.isStunned);
            anim.SetBool("isStunned", false);
            //transform.Rotate(new Vector3(0,-90,0));
        }
    }

    public void SetOpponent(Pokemon p)
    {
        opponent = p;
    }

    public override void Interrupt() {
        Debug.Log(gameObject.name + " interrupted action: "+ currentAction );
        base.Interrupt();
        
        string[] anims = {"isStunned", "isEating", "isWalking"};
        foreach (string s in anims) {
            anim.SetBool(s, false); //interrupt animations
        }
        //move the pokemon back to Free group from wherever it is.
        GWorld.Instance.PokemonEating2Free(gameObject);
        GWorld.Instance.PokemonStunned2Free(gameObject);
        GWorld.Instance.PokemonFighting2Free(gameObject);
        //clear problem states
        

    }


}