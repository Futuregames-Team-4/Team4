using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public float attackRange = 2.0f;
    public float attackDamage = 1.0f;
    public LayerMask playerLayer;
    public int rayCount = 8; // Number of raycasts to shoot in all directions
    public float spreadAngle = 360.0f; // Angle within which raycasts are spread

    private bool hasAttacked = false; // Flag to track whether an attack has already occurred

    void Update()
    {
        bool playerCollisionDetected = false;

        for (int i = 0; i < rayCount; i++)
        {
            int playerLayerMask = 1 << LayerMask.NameToLayer("Box Collider");

            // Calculate the direction for each raycast based on spreadAngle
            float angle = (i / (float)rayCount) * spreadAngle - (spreadAngle / 2.0f);
            Vector3 rayDirection = Quaternion.Euler(0, angle, 0) * transform.forward;

            // Cast a ray in the calculated direction
            RaycastHit hit;
            if (Physics.Raycast(transform.position, rayDirection, out hit, attackRange, playerLayerMask))
            {
                // Check if the hit object has a "Player" tag (you can use layers instead)
                if (hit.collider.CompareTag("Player"))
                {
                    // Perform the attack action (you can modify this part)
                    AttackPlayer(hit.collider);
                    hasAttacked = true; // Set the flag to true after attacking
                    playerCollisionDetected = true; // Player collision detected
                    break; // Exit the loop, as we only want to attack once
                }
            }

            // Draw the raycast for visualization in the Scene view
            Debug.DrawRay(transform.position, rayDirection * attackRange, Color.red);
        }

        // Reset hasAttacked only if no player collision was detected
        if (!playerCollisionDetected)
        {
            hasAttacked = false;
        }
    }

    void AttackPlayer(Collider player)
    {
        if (!hasAttacked) // Check if an attack has not already occurred
        {
            Debug.Log("You are hit!");
            // Apply damage to the player (you can modify this part)
            FuelConsumption fuel = player.transform.parent.GetComponent<FuelConsumption>();
            fuel.HitByEnemy();
            hasAttacked = true; // Set the flag to true after attacking
        }
    }
}
