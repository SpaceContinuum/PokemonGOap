using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Navmesh
using UnityEngine.AI;



// THIS IS JUST A NAVMESH TEST


public class PokemonMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private Rigidbody2D rb;

    //Navmesh
    [SerializeField] private Transform target;
    private NavMeshAgent agent;

    // Start is called before the first frame update
    void Start()
    {
        //Navmesh
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        MoveToTarget(target, 0.4f);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate()
    {
        //agent.SetDestination(target.position);
    }

    public void MoveToTarget(Transform trgt, float distance)
    {
        Vector3 buffer = new Vector3(distance, distance, 0);
        agent.SetDestination(trgt.position + buffer);
    }


   
}
