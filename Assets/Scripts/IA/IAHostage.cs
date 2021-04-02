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

    [Header("Hide parameters")] 
    public float distanceToCheck = 10f;
    public float minDistanceFromEnemies = 3f;
    public int numberOfCirclesToCheck = 3;
    public int numberOfPointPerCircles = 10;
    public float percentDecreasePerCircle = .33f;
    public LayerMask obstaclesLayers;
    public float atSaveSpotDistance = 1f;
    public float checkIfIsAttackedTime = 1f;

    private bool _fleeingEnemies;
    private bool _phaseTwo;
    private float _saveSpotTimer = 1f;
        
    private Vector3 _saveSpotPos;
    private Transform _player;
    private IAParent[] _enemies;
    
    

    public void TakeDamage(int damage)
    {
        // Handle damage and death
        health -= damage;
        if (health <= 0)
        {
            Dead();
        }
    }

    public void SetStateToPhaseTwo()
    {
        // If the state is not already in phase two, send the information to the game manager to start phase two and
        // start invoke repeating to check if the AI is attacked
        if (!_phaseTwo)
        {
            _phaseTwo = true;
            GameManager.Instance.SwitchPhaseForAll();
            InvokeRepeating("IsAttacked", 0, checkIfIsAttackedTime);
        }
    }

    public void Dead()
    {
        // If the AI is dead, start Respawn process
        GameManager.Instance.RespawnPlayer(_player.gameObject, gameObject);
    }

    public void Start()
    {
        // Initialize variables
        health = maxHealth;
        _player = FindObjectOfType<Player>().transform;
        _enemies = FindObjectsOfType<IAParent>();
    }

    public void Update()
    {
        // Set slider for life to show health
        healthBarSlider.value = health;
        
        // If phase two as started the AI as a behavior, if not he do nothing
        if (_phaseTwo)
        {
            if (!_fleeingEnemies)
                // If he does not flee the ennemie he simply follow the player
                //Stop following if it's to close. 
                if (Vector3.Distance(_player.position, transform.position) > correctDistanceFromPlayer)
                {
                    agent.destination = _player.transform.position;
                }
                else
                {
                    agent.destination = transform.position;
                }
            else
            {
                // Check is the savespot has been initialize or have a value of zero to start searching
                if (_saveSpotPos == null || _saveSpotPos == Vector3.zero)
                {
                    _saveSpotPos = SearchSaveSpot();
                }

                // Recheck if the AI is at save point and that it need to check for another one (not 100% of the time for optimisation purpose)
                if (_saveSpotPos != Vector3.zero && AtSaveSpot() && _saveSpotTimer <= 0)
                {
                    // Recheck if enemies are no longer attacking
                    List<Transform> enemyTransforms = new List<Transform>();
                    for (int i = 0; i < _enemies.Length; i++)
                    {
                        if (_enemies[i] != null)
                            if (_enemies[i].isAttack && _enemies[i])
                                enemyTransforms.Add(_enemies[i].transform);
                    }

                    // Check if the point is not still safe, search for another point
                    if (!CheckPointSafe(transform.position, enemyTransforms))
                    {
                        SearchSaveSpot();
                    }

                    // Restart the timer for the next search
                    _saveSpotTimer = 1f;
                }
                else
                {
                    // Decrease the timer before the next search
                    _saveSpotTimer -= Time.deltaTime;
                }

                // If the save point is init, if it is, go to savepoint
                if (_saveSpotPos != Vector3.zero)
                {
                    agent.destination = _saveSpotPos;
                }
            }
        }
    }

    private void IsAttacked()
    {
        // Check every enemy to check if one of them is attacking
        foreach (var ias in _enemies)
        {
            if (ias != null)
                if (ias.isAttack)
                {
                    _fleeingEnemies = true;
                    return;
                }
        }

        _fleeingEnemies = false;
    }

    private bool AtSaveSpot()
    {
        // Check if the AI is at the save position or if it need to continue
        if (Vector3.Distance(_saveSpotPos, transform.position) <= atSaveSpotDistance)
            return true;
        else
            return false;
    }

    private Vector3 SearchSaveSpot()
    {
        // Reset save spot to a non correct value
        Vector3 saveSpot = Vector3.zero;

        // Get all enemies transform
        List<Transform> enemyTransforms = new List<Transform>();
        for (int i = 0; i < _enemies.Length; i++)
        {
            if (_enemies[i] != null)
                if (_enemies[i].isAttack)
                    enemyTransforms.Add(_enemies[i].transform);
        }

        // For each circle
        for (int i = 0; i < numberOfCirclesToCheck; i++)
        {
            // Calculate the radius of the circle
            float circleDistance = (i + 1) * distanceToCheck / numberOfCirclesToCheck;
            // Re-calculate the number of point with percentage optimisation
            int trueNumberOfPoint = Mathf.RoundToInt(numberOfPointPerCircles *
                                                     Mathf.Pow(1 - percentDecreasePerCircle,
                                                         numberOfCirclesToCheck - i - 1));
            // Calculate how much degree we need to add per points
            float degreePerPoint = 360 / trueNumberOfPoint;

            // For each point on this circle
            for (int y = 0; y < trueNumberOfPoint; y++)
            {
                // Calculate point position
                Vector3 pointPosition = new Vector3(circleDistance * Mathf.Sin(Mathf.Deg2Rad * degreePerPoint * y), 0,
                                            circleDistance * Mathf.Cos(Mathf.Deg2Rad * degreePerPoint * y)) + transform.position;

                // Check if the point is safe
                if (CheckPointSafe(pointPosition, enemyTransforms))
                {
                    // If its safe, the position is validate as a save spot
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
        // Check for every hostile ennemies if the distance of the point is correct
        for(int i = 0; i < enemyTransforms.Count; i++)
        {
            if (Vector3.Distance(enemyTransforms[i].position, pointPos) < minDistanceFromEnemies)
            {
                return false;
            }
                
        }

        // Check if the point is not in a wall
        RaycastHit hitSphere;
        if (!Physics.SphereCast(pointPos, .2f, Vector3.down, out hitSphere, .2f, obstaclesLayers))
        {
            // Check foreach ennemy if he has an obstacle between the point and them, if their is one, the point is safe.
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

        Debug.LogError("Fail in CheckPointSafe");
        return false;
    }

    private void OnDrawGizmosSelected()
    {
        // Draw the circles for detections
        for (int i = 0; i < numberOfCirclesToCheck; i++)
        {
            float circleDistance = (i + 1) * distanceToCheck / numberOfCirclesToCheck;
            int trueNumberOfPoint = Mathf.RoundToInt(numberOfPointPerCircles * Mathf.Pow(1-percentDecreasePerCircle, numberOfCirclesToCheck - i - 1));
            float degreePerPoint = 360 / trueNumberOfPoint;

            for (int y = 0; y < trueNumberOfPoint; y++)
            {
                Vector3 pointPosition = new Vector3(circleDistance * Mathf.Sin(Mathf.Deg2Rad * degreePerPoint * y), 0, circleDistance * Mathf.Cos(Mathf.Deg2Rad * degreePerPoint * y));
                
                Gizmos.color = Color.green;
                Gizmos.DrawSphere(transform.position + pointPosition, .5f);
            }
        }

        if (_saveSpotPos != null)
        {
            Gizmos.color = Color.magenta;
            Gizmos.DrawSphere(_saveSpotPos, .3f);
        }
    }
}
