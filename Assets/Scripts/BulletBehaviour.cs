using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{
    public float speed;

    public int damage;
    
    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward*Time.deltaTime*speed);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.GetComponent<PlayerLife>().TakeDamage(damage);
            gameObject.SetActive(false);
        }
    }
}
