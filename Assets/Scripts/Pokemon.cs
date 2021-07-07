using UnityEngine;

public class Pokemon : GAgent
{

    public enum PokemonType{ NULL, Fire, Water, Grass};
    protected ConfigData gameConfig;
    
    public GameController gameController;
    [SerializeField] private Pokemon.PokemonType myType = PokemonType.Grass;
    private Pokemon.PokemonType weaknessType;
    private Pokemon.PokemonType strengthType;


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
        
        SubGoal s3 = new SubGoal(WorldState.Label.recovered, 10, false);
        goals.Add(s3,10);
        
        
        SubGoal s4 = new SubGoal(WorldState.Label.recovered, 10, false);
        goals.Add(s4,10);
        

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
        SetViolence();
    }

    public void SetViolence()
    {
        beliefs.ModifyState("isViolent", 0);
        Debug.Log(gameObject.name + " is violent");

    }

    public void SetStun(bool state) {
        if (state && !beliefs.HasState(WorldState.Label.stunned))
        {
            beliefs.ModifyState(WorldState.Label.stunned, 0);
            transform.Rotate(new Vector3(0,0,90));
        }
        else {
            beliefs.RemoveState(WorldState.Label.stunned);
            transform.Rotate(new Vector3(0,0,-90));
        }
    }


}