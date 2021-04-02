using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DisarmMine : MonoBehaviour
{

    public GameObject player;

    public float detectionRange = 2f;

    public bool isTextActive = false;
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
           UI_MenuButtons._instance.disarm_text.SetActive(true);
           isTextActive = true;
            if (Input.GetKey(KeyCode.E))
            {
                Disarm(gameObject);
                UI_MenuButtons._instance.disarm_text.SetActive(false);
                
            }
        }
        else
        {
            if (isTextActive)
            {
                UI_MenuButtons._instance.disarm_text.SetActive(false);
                isTextActive = false;
            }
        }
        
    }


    public void DesactiveText()
    {
        if (isTextActive)
        {
            UI_MenuButtons._instance.disarm_text.SetActive(false);
        }
        
    }
    
    private void Disarm(GameObject mine)
    {
        
        mine.GetComponent<Mine>().aIPlacer.minePlaced = false;
        Destroy(mine);
    }
}
