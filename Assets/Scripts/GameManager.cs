using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public List<GameObject> itemSpawnable;

    private List<GameObject> _objectSpawned = new List<GameObject>(2);
    
    public List<Transform> placeToSpawnItems;

    public GameObject panelWin;
    public GameObject deathPlayerTxt;

    public Transform playerRespawnPoint;
    public Transform hostageRespawnPoint;
    
    private float _timerToSpawnItem;
    public float initialTimerToSpawnItem;
    private float _countdownToDisableTextDeath = 3;
    
    public bool isPhaseTwo;
    private bool textDeathCountdown;

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
         _timerToSpawnItem = initialTimerToSpawnItem;
         Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        _timerToSpawnItem -= Time.deltaTime;
        if (_timerToSpawnItem <= 0)
        {
            SpawnItem();
        }

        if (textDeathCountdown)
        {
            _countdownToDisableTextDeath -= Time.deltaTime;
        }

        if (_countdownToDisableTextDeath <= 0)
        {
            textDeathCountdown = false;
            _countdownToDisableTextDeath = 3;
            deathPlayerTxt.SetActive(false);
        }
    }

    public void RespawnPlayer(GameObject player,GameObject hostage)
    {
        
        if (isPhaseTwo)
        {
            deathPlayerTxt.SetActive(true);
            textDeathCountdown = true;
            player.GetComponent<CharacterController>().enabled = false;
            player.transform.position = playerRespawnPoint.position;
            player.GetComponent<PlayerLife>().Start();
            player.GetComponent<CharacterController>().enabled = true;
            
            hostage.transform.position = hostageRespawnPoint.position;
            hostage.GetComponent<IAHostage>().health = hostage.GetComponent<IAHostage>().maxHealth;
        }
        else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex, LoadSceneMode.Single);
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
        _timerToSpawnItem = initialTimerToSpawnItem;
        foreach (var placeToSpawnItem in placeToSpawnItems)
        {
            GameObject itemToSpawn = itemSpawnable[Random.Range(0, itemSpawnable.Count)];
            GameObject itemInstantiated = Instantiate(itemToSpawn, placeToSpawnItem.position, Quaternion.identity);
            _objectSpawned.Add(itemInstantiated);
        }
    }

    public void Win()
    {
        panelWin.SetActive(true);
        Cursor.visible = true;
        Time.timeScale = 0f;
        UI_MenuButtons._instance.isGameIsPause = true;
    }
    
    
    
}