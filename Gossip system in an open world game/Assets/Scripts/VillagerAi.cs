
using UnityEngine;
using UnityEngine.AI;

public class VillagerAi : MonoBehaviour
{
    public NavMeshAgent agent;

    public Transform player;

    public LayerMask whatIsGround, whatIsPlayer;

    public float health;

    //Patroling
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;

    //Attacking
    public float timeBetweenAttacks;
    bool alreadyAttacked;
    public GameObject projectile;

    //States
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;

    private void Awake()
    {
        player = GameObject.Find("MainCharacter").transform;
        agent = GetComponent<NavMeshAgent>();
        Debug.Log("Enter Awake");
    }

    private void Update()
    {
        
        Debug.Log("Updating..."+agent);
        //Check for sight and attack range
        playerInSightRange = false;//Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = false;//Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);
        //Debug.Log("playerInSightRange"+playerInSightRange);
        //Debug.Log("playerInAttackRange"+playerInAttackRange);
        if (!playerInSightRange && !playerInAttackRange) Patroling();
        if (playerInSightRange && !playerInAttackRange) ChasePlayer();
        //if (playerInAttackRange && playerInSightRange) AttackPlayer();
    }

    private void Patroling()
    {
        Vector3 dest = agent.destination;
        Debug.Log("dest:"+dest);
        Debug.Log("Enter Patroling, walkPointset="+walkPointSet);
        if (!walkPointSet) {
            SearchWalkPoint();
            Debug.Log("After SearchWalkPoint, walkPointset="+walkPointSet);
        }

        if (walkPointSet){
            agent.destination = walkPoint;
            //bool a = agent.SetDestination(walkPoint);
            //Debug.Log("is Succecced?"+a);
        }
            

        Debug.Log("walkPoint"+walkPoint);
        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        //Walkpoint reached
        if (distanceToWalkPoint.magnitude < 1f){
            Debug.Log("Reached");
            walkPointSet = false;
        }
    }
    private void SearchWalkPoint()
    {
        Debug.Log("Enter SearchWalkPoint");
        //Calculate random point in range
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);
        walkPointSet = true;
        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
            walkPointSet = true;
    }

    private void ChasePlayer()
    {
        agent.SetDestination(player.position);
    }

    private void AttackPlayer()
    {
        //Make sure enemy doesn't move
        agent.SetDestination(transform.position);

        transform.LookAt(player);

        if (!alreadyAttacked)
        {
            ///Attack code here
            Rigidbody rb = Instantiate(projectile, transform.position, Quaternion.identity).GetComponent<Rigidbody>();
            rb.AddForce(transform.forward * 32f, ForceMode.Impulse);
            rb.AddForce(transform.up * 8f, ForceMode.Impulse);
            ///End of attack code

            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }
    private void ResetAttack()
    {
        alreadyAttacked = false;
    }

    public void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0) Invoke(nameof(DestroyEnemy), 0.5f);
    }
    private void DestroyEnemy()
    {
        Destroy(gameObject);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }
}
