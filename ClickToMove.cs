using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

// This script is used to check to see if navAgents are able to move around correctly on areas marked as walkable.
// Add this script to an npc with a Nav Agent for testing.
public class ClickToMove : MonoBehaviour
{
    private NavMeshAgent navAgent;

    private void Start()
    {
        navAgent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // Create a ray from the camera to the mouse position. 
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            // Check to see if the ray hits the ground.
            if (Physics.Raycast(ray, out hit, Mathf.Infinity)) 
            {
                // Move the agent to the clicked position. 
                navAgent.SetDestination(hit.point);
            }
        }
    }
}
