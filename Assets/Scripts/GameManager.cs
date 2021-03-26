using System.Collections.Generic;
using TreeEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public List<GameObject> itemSpawnable;

    private List<GameObject> _objectSpawned = new List<GameObject>(2);
    
    public List<Transform> placeToSpawnItems;

    public GameObject panelWin;
    
    public Transform playerRespawnPoint;
    public Transform hostageRespawnPoint;
    
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

    public void RespawnPlayer(GameObject player,GameObject hostage)
    {
        if (isPhaseTwo)
        {
            player.transform.position = playerRespawnPoint.position;
            player.GetComponent<PlayerLife>().playerLife = player.GetComponent<PlayerLife>().playerMaxLife;
            hostage.transform.position = hostageRespawnPoint.position;
            hostage.GetComponent<IAHostage>().health = hostage.GetComponent<IAHostage>().maxHealth;
        }
        else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
    
    public void SwitchPhaseForAll()
    {
        isPhaseTwo = true;
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

    public void Win()
    {
        panelWin.SetActive(true);
    }
}