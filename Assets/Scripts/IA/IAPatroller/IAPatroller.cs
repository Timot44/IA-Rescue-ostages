using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class IAPatroller : IAParent
{
    public bool minePlaced;
    
    [Header("Call parameters")]
    public float callEnemyMaxDistance = 15;
    
    [Header("Behavior")]
    public bool fleeing;
    public float timeToStopFleeing = 20;
    public float placeMineTime = 10f;
    public GameObject prefabMine;
    public Transform spawnMineTransform;

    private PatrollerState _currentState;
    
    public NavMeshAgent agent;

    public Transform player;

    void Flee()
    {
        // Script used when the AI Flee the player after detection
        fleeing = true;
        StartCoroutine(WaitToStopFleeing());
        CallEnemies();
    }

    void CallEnemies()
    {
        // Find all enemies
        GameObject[] allEnemies = GameObject.FindGameObjectsWithTag("Enemy");

        // Only call enemies in a restricted distance
        foreach (var enemy in allEnemies)
        {
            if (Vector3.Distance(enemy.transform.position, transform.position) < callEnemyMaxDistance)
            {
                // Call the function in IAParent to other enemies
                enemy.GetComponent<IAParent>().HelpPatroller(player);
            }
        }
    }

    public override void SwitchToState()
    {
        // Change state to change behavior
        _currentState = new PatrollerStateP2();
    }
    
    void Start()
    {
        // Initialize variables
        player = GameObject.FindGameObjectWithTag("Player").transform;
        health = maxHealth;
        SetBarMax(maxHealth);
        _currentState = new PatrollerStateP1();
        // Start a loop to place mine
        InvokeRepeating("AttemptToPlaceMine", 0, placeMineTime);
    }
    
    void Update()
    {
        // Move the patroller
        _currentState.Move(this);
        
        // If the AI Detect the player she start fleeing
        if (DetectPlayer())
        {
            Flee();
        }
    }

    void AttemptToPlaceMine()
    {
        // Try to place mine
        _currentState.PlaceMine(this);
    }

    public void PlaceMine()
    {
        // Place the mine in the world and set a bool to true to detect if it's already place
        minePlaced = true;
        GameObject mine = Instantiate(prefabMine, spawnMineTransform.position, Quaternion.identity);
        
        // Init a variable in the mine so that it can callback when destroyed
        mine.GetComponent<Mine>().aIPlacer = this;
    }

    public bool DetectPlayer()
    {
        // Function to detect player
        
        // Calc the angle to add per ray placed
        float anglePerRay = angle / rayCount;
        
        for (int i = 0; i < rayCount; i++)
        {
            // Foreach ray, this part calculate positions for point to raycast toward
            Vector3 rayForward = new Vector3(detectionRange * Mathf.Sin((Mathf.Deg2Rad * anglePerRay * i) - (angle/2) * Mathf.Deg2Rad + transform.localEulerAngles.y * Mathf.Deg2Rad), 
                0, 
                detectionRange * Mathf.Cos((Mathf.Deg2Rad * anglePerRay * i) - (angle/2) * Mathf.Deg2Rad + transform.localEulerAngles.y * Mathf.Deg2Rad));
            
            Ray ray = new Ray(transform.position, rayForward);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, detectionRange))
            {
                // If the ray touch the player or the hostage, the function return true
                if (hit.collider.tag == "Player")
                {
                    return true;
                }
            }
        }
        return false;
    }

    public IEnumerator WaitToStopFleeing()
    {
        // Little function to wait a certain amount of time before stop fleeing
        yield return new WaitForSeconds(timeToStopFleeing);
        fleeing = false;
    }

    // Help patroller is empty because a patroller is useless to call
    public override void HelpPatroller(Transform playerTransform)
    {
    }

    private void OnDrawGizmosSelected()
    {
        // Simply draw gizmo to see the detection rays when the object is selected in the editor
        float anglePerRay = (angle/2) / (rayCount/2);
        
        for (int i = 0; i < rayCount; i++)
        {
            Vector3 rayForward = transform.position + new Vector3(
                                     detectionRange *
                                     Mathf.Sin((Mathf.Deg2Rad * anglePerRay * i) - (angle / 2) * Mathf.Deg2Rad +
                                               transform.localEulerAngles.y * Mathf.Deg2Rad), 0,
                                     detectionRange *
                                     Mathf.Cos((Mathf.Deg2Rad * anglePerRay * i) - (angle / 2) * Mathf.Deg2Rad +
                                               transform.localEulerAngles.y * Mathf.Deg2Rad));
            Gizmos.color = Color.red;
            Gizmos.DrawRay(transform.position, rayForward - transform.position);
        }
    }
}
