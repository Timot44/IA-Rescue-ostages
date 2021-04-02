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

    private PatrollerState currentState;
    
    public NavMeshAgent agent;

    public Transform player;

    void Flee()
    {
        fleeing = true;
        StartCoroutine(WaitToStopFleeing());
        CallEnemies();
    }

    void CallEnemies()
    {
        GameObject[] allEnemies = GameObject.FindGameObjectsWithTag("Enemy");

        foreach (var enemy in allEnemies)
        {
            if (Vector3.Distance(enemy.transform.position, transform.position) < callEnemyMaxDistance)
            {
                enemy.GetComponent<IAParent>().HelpPatroller(player);
            }
        }
    }

    public override void SwitchToState()
    {
        currentState = new PatrollerStateP2();
    }
    
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        currentState = new PatrollerStateP1();
        InvokeRepeating("AttemptToPlaceMine", 0, placeMineTime);
    }
    
    void Update()
    {
        currentState.Move(this);

        if (Input.GetKeyDown(KeyCode.J))
        {
            currentState = new PatrollerStateP2();
        }

        if (DetectPlayer())
        {
            Flee();
        }
    }

    void AttemptToPlaceMine()
    {
        currentState.PlaceMine(this);
    }

    public void PlaceMine()
    {
        minePlaced = true;
        GameObject mine = Instantiate(prefabMine, spawnMineTransform.position, Quaternion.identity);
        mine.GetComponent<Mine>().aIPlacer = this;
        player.GetComponent<PlayerShoot>().canDisarm = true;
        player.GetComponent<PlayerShoot>().mine = mine;
    }

    public bool DetectPlayer()
    {
        float anglePerRay = angle / rayCount;
        
        for (int i = 0; i < rayCount; i++)
        {
            Vector3 rayForward = new Vector3(detectionRange * Mathf.Sin((Mathf.Deg2Rad * anglePerRay * i) - (angle/2) * Mathf.Deg2Rad + transform.localEulerAngles.y * Mathf.Deg2Rad), 
                0, 
                detectionRange * Mathf.Cos((Mathf.Deg2Rad * anglePerRay * i) - (angle/2) * Mathf.Deg2Rad + transform.localEulerAngles.y * Mathf.Deg2Rad));
            
            Ray ray = new Ray(transform.position, rayForward);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, detectionRange))
            {
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
        yield return new WaitForSeconds(timeToStopFleeing);
        fleeing = false;
    }

    public override void HelpPatroller(Transform playerTransform)
    {
    }

    private void OnDrawGizmosSelected()
    {
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
