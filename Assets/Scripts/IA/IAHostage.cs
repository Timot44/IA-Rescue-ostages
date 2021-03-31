using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class IAHostage : MonoBehaviour
{
    [Header("Main parameters")]
    public int health;
    public int maxHealth = 100;
    public float correctDistanceFromPlayer = 2f;
    public NavMeshAgent agent;
    public Slider healthBarSlider;
    public bool testFlee;

    [Header("Hide parameters")] 
    public float distanceToCheck = 10f;
    public float minDistanceFromEnemies = 3f;
    public int numberOfCirclesToCheck = 3;
    public int numberOfPointPerCircles = 10;
    public float percentDecreasePerCircle = .33f;
    public LayerMask obstaclesLayers;
    public float atSaveSpotDistance = 1f;
    public float checkIfIsAttackedTime = 1f;

    private bool fleeingEnemies;
    private bool phaseTwo;
    private float saveSpotTimer = 1f;
        
    private Vector3 saveSpotPos;
    private Transform player;
    private IAParent[] enemies;
    
    

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
            InvokeRepeating("IsAttacked", 0, checkIfIsAttackedTime);
        }
    }

    public void Dead()
    {
        GameManager.Instance.RespawnPlayer(player.gameObject, gameObject);
    }

    public void Start()
    {
        health = maxHealth;
        player = FindObjectOfType<Player>().transform;
        enemies = FindObjectsOfType<IAParent>();
    }

    public void Update()
    {
        healthBarSlider.value = health;
        
        if (phaseTwo)
        {
            if (!fleeingEnemies && !testFlee)
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
                
                if (saveSpotPos == null || saveSpotPos == Vector3.zero)
                {
                    saveSpotPos = SearchSaveSpot();
                }

                if (saveSpotPos != Vector3.zero && AtSaveSpot() && saveSpotTimer <= 0)
                {
                    List<Transform> enemyTransforms = new List<Transform>();
                    for (int i = 0; i < enemies.Length; i++)
                    {
                        if (enemies[i].isAttack && enemies[i])
                            enemyTransforms.Add(enemies[i].transform);
                    }

                    if (!CheckPointSafe(transform.position, enemyTransforms))
                    {
                        SearchSaveSpot();
                    }

                    saveSpotTimer = 1f;
                }
                else
                {
                    saveSpotTimer -= Time.deltaTime;
                }

                if (saveSpotPos != Vector3.zero)
                {
                    Debug.Log("GotoSavespot : " + saveSpotPos);
                    agent.destination = saveSpotPos;
                }
            }
        }
    }

    private void IsAttacked()
    {
        foreach (var ias in enemies)
        {
            if (ias.isAttack)
            {
                fleeingEnemies = true;
                return;
            }
        }

        fleeingEnemies = false;
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
        Vector3 saveSpot = Vector3.zero;

        // Get all enemies transform
        List<Transform> enemyTransforms = new List<Transform>();
        for (int i = 0; i < enemies.Length; i++)
        {
            if (enemies[i].isAttack)
                enemyTransforms.Add(enemies[i].transform);
        }

        for (int i = 0; i < numberOfCirclesToCheck; i++)
        {
            float circleDistance = (i + 1) * distanceToCheck / numberOfCirclesToCheck;
            int trueNumberOfPoint = Mathf.RoundToInt(numberOfPointPerCircles *
                                                     Mathf.Pow(1 - percentDecreasePerCircle,
                                                         numberOfCirclesToCheck - i - 1));
            float degreePerPoint = 360 / trueNumberOfPoint;

            for (int y = 0; y < trueNumberOfPoint; y++)
            {
                Vector3 pointPosition = new Vector3(circleDistance * Mathf.Sin(Mathf.Deg2Rad * degreePerPoint * y), 0,
                    circleDistance * Mathf.Cos(Mathf.Deg2Rad * degreePerPoint * y)) + transform.position;

                if (CheckPointSafe(pointPosition, enemyTransforms))
                {
                    saveSpot = pointPosition;
                    
                    return saveSpot;
                }
            }
        }
        
        Debug.Log("No save spot found");
        
        return saveSpot;
    }

    private bool CheckPointSafe(Vector3 pointPos, List<Transform> enemyTransforms)
    {
        // Check is
        for(int i = 0; i < enemyTransforms.Count; i++)
        {
            if (Vector3.Distance(enemyTransforms[i].position, pointPos) < minDistanceFromEnemies)
            {
                return false;
            }
                
        }

        RaycastHit hitSphere;
        if (!Physics.SphereCast(pointPos, .2f, Vector3.down, out hitSphere, .2f, obstaclesLayers))
        {
            
            foreach (var enemy in enemyTransforms)
            {
                RaycastHit hit;
                if (Physics.Linecast(pointPos, enemy.position, obstaclesLayers))
                {
                    return true;
                }
                else
                {
                    return false;
                }
                    
            }
        }
        else
        {
            return false;
        }

        Debug.Log("PAS NORMAL");
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
