using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public List<GameObject> itemSpawnable;

    public List<Transform> placeToSpawnItems;

    public Transform currentRespawnPoint;
    private float _timerToSpawnItem;

    public bool isPhaseTwo;

    #region singleton

    public static GameManager Instance;

    private void Awake()
    {
        if (Instance == null)
        { Instance = this;
        }
    }

    #endregion

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void SwitchPhaseForAll()
    {
    }

    private void SpawnItem()
    {
    }
}