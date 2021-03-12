using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public List<GameObject> itemSpawnable;

    public List<Transform> placeToSpawnItems;

    public Transform currentRespawnPoint;
    private float _timerToSpawnItem;
    private float _initialTimer;
    public bool isPhaseTwo;

    #region singleton

    public static GameManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    #endregion

    // Start is called before the first frame update
    void Start()
    {
        _initialTimer = _timerToSpawnItem;
    }

    // Update is called once per frame
    void Update()
    {
        _timerToSpawnItem -= Time.deltaTime;
        if (_timerToSpawnItem <= 0)
        {
            SpawnItem();
        }
    }

    public void SwitchPhaseForAll()
    {
        //TODO faire le changement de phase
    }

    private void SpawnItem()
    {
        _timerToSpawnItem = _initialTimer;
        foreach (var placeToSpawnItem in placeToSpawnItems)
        {
            GameObject itemToSpawn = itemSpawnable[Random.Range(0, itemSpawnable.Count - 1)];
            Instantiate(itemToSpawn, placeToSpawnItem.position, Quaternion.identity);
        }
    }
}