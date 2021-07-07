using UnityEngine;

public class Pokemon : GAgent
{

    public enum PokemonType{ NULL, Fire, Water, Grass};
    protected ConfigData gameConfig;
    
    //public GameController gameController;
    [SerializeField] private Pokemon.PokemonType myType = PokemonType.Grass;
    private Pokemon.PokemonType weaknessType;
    private Pokemon.PokemonType strengthType;
    public Animator anim;

    new void Start()
    {

        CalculateTypes();
        // Call the base start
        base.Start();
        // Set up the subgoal "isFull"
        SubGoal s1 = new SubGoal(WorldState.Label.isFull, 5, false);
        // Add it to the goals
        goals.Add(s1, 3);

        // Set up the subgoal "isFighting"
        SubGoal s2 = new SubGoal("isPeaceful", 1, false);
        // Add it to the goals
        goals.Add(s2, 5);

        //get hungry in a random time frame
        //Invoke("GetHungry", Random.Range(gameConfig.HungerFrequency-gameConfig.HungerVariance, gameConfig.HungerFrequency+gameConfig.HungerVariance));
        //Invoke("GetHungry", 10f);
        
        SubGoal s3 = new SubGoal(WorldState.Label.isRecovered, 10, false);
        goals.Add(s3,10);
        
     
        anim = GetComponent<Animator>();
    }

    public void Update() {
        bool animState =  false;
        if (currentAction != null) {
            Vector3 v = currentAction.agent.velocity;
            animState = (v.magnitude > 0);
        }
        anim.SetBool("isWalking", animState);
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

    
    public PokemonType GetPokemonType()
    {
        return myType;
    }

    public PokemonType GetWeaknessType()
    {
        return weaknessType;
    }

    public PokemonType GetStrengthType()
    {
        return strengthType;
    }

    public void GetHungry() {
        beliefs.ModifyState("isHungry", 0);
        //call the get hungry method over and over at random times to make the Pokemon
        //get hungry again
        Debug.Log(gameObject.name+" is hungry");

        //TODO: move this to the "Eat" function.
        //Invoke("GetHungry", Random.Range(gameConfig.HungerFrequency-gameConfig.HungerVariance, gameConfig.HungerFrequency+gameConfig.HungerVariance));
    }


    public void SetStun(bool state) {
        if (state && !beliefs.HasState(WorldState.Label.isStunned))
        {
            beliefs.ModifyState(WorldState.Label.isStunned, 0);
            anim.SetBool("isStunned", true);
            //transform.Rotate(new Vector3(0,0,90));
        }
        else {
            beliefs.RemoveState(WorldState.Label.isStunned);
            anim.SetBool("isStunned", false);
            //transform.Rotate(new Vector3(0,0,-90));
        }
    }


}