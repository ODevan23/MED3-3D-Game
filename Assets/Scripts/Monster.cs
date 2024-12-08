using UnityEngine;
using UnityEngine.AI;

public class MonsterAI : MonoBehaviour
{
    public float detectionRange = 20f; 
    public float moveSpeed = 3.5f;     
    public float idleTime = 2f;        
    public float moveTime = 3f;       

    private Transform player;          
    private NavMeshAgent navMeshAgent; 
    private bool isMoving = false;     
    private float timer = 0f;          


    private void Start()
    {
      
        player = GameObject.FindGameObjectWithTag("Player").transform; // here we find where the player's position is so we can use it later after game has started

       
        navMeshAgent = GetComponent<NavMeshAgent>(); //stuff that is required for the ai to move

       
        navMeshAgent.speed = moveSpeed;
    }

    private void Update()
    {
        // Check if player is within detection range
        if (Vector3.Distance(transform.position, player.position) < detectionRange)
        {
            // Start moving towards the player if it's within detection range
            MoveTowardsPlayer();
        }
        else
        {
            // When the player is out of range, follow idle/move pattern
            HandleIdleAndMove();
        }
    }

    private void MoveTowardsPlayer() //Moves monster toward player using navmesh navigation
    {
       
        navMeshAgent.SetDestination(player.position);

       
        isMoving = true;
    }

    private void HandleIdleAndMove()
    {
        if (isMoving)
        {
            // Move the monster for 'moveTime' seconds
            timer += Time.deltaTime;
            if (timer >= moveTime)
            {
                // After moving, switch to idle
                isMoving = false;
                timer = 0f;  // Reset the timer
                navMeshAgent.isStopped = true; // Stop the movement
            }
        }
        else
        {
            // Idle for 'idleTime' seconds
            timer += Time.deltaTime;
            if (timer >= idleTime)
            {
                // After idle, switch to moving
                isMoving = true;
                timer = 0f;  // Reset the timer
                navMeshAgent.isStopped = false; // Resume movement
            }
        }
    }
}
