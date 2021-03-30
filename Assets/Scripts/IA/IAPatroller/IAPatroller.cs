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
    public int callEnemyCount = 5;
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
        int helpCounter = callEnemyCount;

        foreach (var enemy in allEnemies)
        {
            if (Vector3.Distance(enemy.transform.position, transform.position) < callEnemyMaxDistance)
            {
                enemy.GetComponent<IAParent>().HelpPatroller(player);
                helpCounter--;
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
        Vector3 iAPos = transform.position;
        Vector3 iAForward = transform.forward * detectionRange;
        Vector3 iARight = transform.right * 3f;
        Ray ray1 = new Ray(iAPos, iAForward);
        Ray ray2 = new Ray(iAPos,iAForward - iARight);
        Ray ray3 = new Ray(iAPos,iAForward + iARight);
        Ray[] rays = new[] {ray1, ray2, ray3};
        RaycastHit hit;
        foreach (var ray in rays)
        {
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
        Debug.DrawRay(transform.position,detectionRange * transform.forward, Color.red);
        Debug.DrawRay(transform.position,detectionRange * transform.forward - transform.right * 3, Color.red);
        Debug.DrawRay(transform.position,detectionRange * transform.forward + transform.right * 3, Color.red);
    }
}
