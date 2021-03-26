using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class IAHostage : MonoBehaviour
{
    [Header("Main parameters")]
    public int health;
    public int maxHealth = 100;
    public float correctDistanceFromPlayer = 2f;
    public NavMeshAgent agent;

    [Header("Hide parameters")] 
    public float distanceToCheck = 10f;
    public float minDistanceFromEnemies = 3f;
    public int numberOfCirclesToCheck = 3;
    public int numberOfPointPerCircles = 10;
    public float percentDecreasePerCircle = .33f;
    public LayerMask obstaclesLayers;
    public float atSaveSpotDistance = 1f;

    private bool fleeingEnemies;
    private bool phaseTwo;
    private Vector3 saveSpotPos;
    
    private Transform player;

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Dead();
        }
    }

    public void SetStateToPhaseTwo()
    {
        if (!phaseTwo)
        {
            phaseTwo = true;
            GameManager.Instance.SwitchPhaseForAll();
        }
    }

    public void Dead()
    {
    }

    public void Start()
    {
        health = maxHealth;
        player = FindObjectOfType<Player>().transform;
    }

    public void Update()
    {
        if (phaseTwo)
        {
            if (!fleeingEnemies)
                if (Vector3.Distance(player.position, transform.position) > correctDistanceFromPlayer)
                {
                    agent.destination = player.transform.position;
                }
                else
                {
                    agent.destination = transform.position;
                }
            else
            {
                if (saveSpotPos == null)
                {
                    saveSpotPos = SearchSaveSpot();
                }
                if (AtSaveSpot())
                {
                    
                }
            }
        }
    }

    private bool AtSaveSpot()
    {
        if (Vector3.Distance(saveSpotPos, transform.position) <= atSaveSpotDistance)
            return true;
        else
            return false;
    }

    private Vector3 SearchSaveSpot()
    {
        Vector3 saveSpot = new Vector3();

        // Get all enemies transform
        IAParent[] ias = FindObjectsOfType<IAParent>();
        Transform[] enemyTransforms = new Transform[ias.Length];
        for (int i = 0; i < ias.Length; i++)
        {
            enemyTransforms[i] = ias[i].transform;
        }

        return saveSpot;
    }

    private bool CheckPointSafe(Vector3 pointPos, Transform[] enemyTransforms, Transform[] hostileEnemies)
    {
        // Check is
        for(int i = 0; i < enemyTransforms.Length; i++)
        {
            if (Vector3.Distance(enemyTransforms[i].position, transform.position) < minDistanceFromEnemies)
                return false;
        }

        RaycastHit hitSphere;
        if (!Physics.SphereCast(pointPos, .2f, Vector3.down, out hitSphere, .2f, obstaclesLayers))
        {
            foreach (var enemy in hostileEnemies)
            {
                RaycastHit hit;
                if (Physics.Linecast(pointPos, enemy.position, obstaclesLayers))
                    return true;
                else
                    return false;
            }
        }
        else
        {
            Debug.Log("This point is in wall : " + pointPos);
            return false;
        }

        return true;
    }

    private void OnDrawGizmosSelected()
    {
        for (int i = 0; i < numberOfCirclesToCheck; i++)
        {
            float circleDistance = (i + 1) * distanceToCheck / numberOfCirclesToCheck;
            int trueNumberOfPoint = Mathf.RoundToInt(numberOfPointPerCircles * Mathf.Pow(1-percentDecreasePerCircle, numberOfCirclesToCheck - i - 1));
            float degreePerPoint = 360 / trueNumberOfPoint;

            for (int y = 0; y < trueNumberOfPoint; y++)
            {
                Vector3 pointPosition = new Vector3(circleDistance * Mathf.Sin(Mathf.Deg2Rad * degreePerPoint * y), 0, circleDistance * Mathf.Cos(Mathf.Deg2Rad * degreePerPoint * y));
                
                Gizmos.color = Color.green;
                Gizmos.DrawSphere(transform.position + pointPosition, .2f);
            }
        }

        if (saveSpotPos != null)
        {
            Gizmos.color = Color.magenta;
            Gizmos.DrawSphere(saveSpotPos, .3f);
        }
    }
}
