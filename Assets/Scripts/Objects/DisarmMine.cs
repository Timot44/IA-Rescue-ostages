using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisarmMine : MonoBehaviour
{

    public GameObject player;

    public float detectionRange = 2f;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(gameObject.transform.position, player.transform.position) <= detectionRange)
        {
            if (Input.GetKey(KeyCode.E))
            {
                Disarm(gameObject);
            }
        }
    }
    
    
    
    
    private void Disarm(GameObject mine)
    {
        mine.GetComponent<Mine>().aIPlacer.minePlaced = false;
        Destroy(mine);
    }
}
