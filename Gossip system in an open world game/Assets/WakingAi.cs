using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WakingAi : MonoBehaviour
{
    [SerializeField] private Transform movingPositionTransform;
    private NavMeshAgent agent;
    // Start is called before the first frame update
    private void Awake(){
        agent = GetComponent<NavMeshAgent>();
    }
    private void Update(){
        agent.SetDestination(movingPositionTransform.position);
        Debug.Log(agent.destination);
    }
    
}
