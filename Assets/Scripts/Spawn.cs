using UnityEngine;

public class Spawn : MonoBehaviour {

    // Grab our prefab
    public GameObject foodPrefab;
    // Number of patients to spawn
    public int numPokemon = 5;
    //private int foodCounter = 1;

    Spawn Spawner = null;

    public static Spawn Instance { get; private set; } //singleton
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
        DontDestroyOnLoad(gameObject);
    }


    void Start() {

        Invoke("SpawnFood", 5.0f);
    }

   private void SpawnFood() {
        Vector3 spawnPosition = new Vector3(Random.Range(-10.0f, 10.0f), Random.Range(-4.0f, 4.0f),0);

        if (numPokemon - 1 > foodCounter)
        {
            // Instantiate new food
            GameObject newFood = Instantiate(foodPrefab, spawnPosition, Quaternion.identity);
            //foodCounter++;
            GWorld.Instance.AddFood(newFood);
        }

        // Invoke this method at random intervals
        Invoke("SpawnFood", Random.Range(2.0f, 10.0f));
    }

    // Update is called once per frame
    void Update()
    {

    }

   
}
