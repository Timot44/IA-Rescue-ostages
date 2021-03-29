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
            if (other.GetComponent<PlayerLife>())
            {
                other.GetComponent<PlayerLife>().TakeDamage(damage);
            }
            else if (other.GetComponent<IAHostage>())
            {
                other.GetComponent<IAHostage>().TakeDamage(damage);
            }
            gameObject.SetActive(false);
        }
    }
}
