using UnityEngine;
using UnityEngine.AI;

public class MonsterAI : MonoBehaviour
{
    public float detectionRange = 20f; // How far the monster can detect the player
    public float moveSpeed = 3.5f;     // Speed when moving towards the player
    public float idleTime = 2f;        // Time the monster stands still before moving again
    public float moveTime = 3f;        // Time the monster moves before stopping again

    private Transform player;          // Reference to the player's Transform
    private NavMeshAgent navMeshAgent; // NavMeshAgent component
    private bool isMoving = false;     // Flag to check if the monster is currently moving
    private float timer = 0f;          // Timer to track time for idle and move phases

    private void Start()
    {
        // Find the player by tag
        player = GameObject.FindGameObjectWithTag("Player").transform;

        // Get the NavMeshAgent component
        navMeshAgent = GetComponent<NavMeshAgent>();

        // Set the agent's speed for movement towards the player
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

    private void MoveTowardsPlayer()
    {
        // Directly set the destination to the player's position
        navMeshAgent.SetDestination(player.position);

        // Ensure the monster is moving towards the player
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
