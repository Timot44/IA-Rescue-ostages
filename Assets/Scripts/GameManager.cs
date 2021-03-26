using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public List<GameObject> itemSpawnable;

    public List<Transform> placeToSpawnItems;
    private List<GameObject> _objectSpawned = new List<GameObject>(2);
    public Transform currentRespawnPoint;
    private float _timerToSpawnItem;
    public float initialTimer;
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
         _timerToSpawnItem = initialTimer;
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
        IAParent[] enemies = FindObjectsOfType<IAParent>();
        foreach (var IA in enemies)
        {
            IA.SwitchToState();
        }
    }

    private void SpawnItem()
    {
        foreach (var obj in _objectSpawned)
        {
            foreach (var place in placeToSpawnItems)
            {
                if (obj !=null && obj.transform.position == place.position)
                {
                    DestroyImmediate(obj,true);
                }
            }
        }
        _objectSpawned.Clear();
        _timerToSpawnItem = initialTimer;
        foreach (var placeToSpawnItem in placeToSpawnItems)
        {
            GameObject itemToSpawn = itemSpawnable[Random.Range(0, itemSpawnable.Count - 1)];
            GameObject itemInstantiated = Instantiate(itemToSpawn, placeToSpawnItem.position, Quaternion.identity);
            _objectSpawned.Add(itemInstantiated);
        }
    }
}