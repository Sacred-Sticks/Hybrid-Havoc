using System.Collections;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float lifeTime;
    [SerializeField] private float damage;
    
    public GameObject Owner { private get; set; }
    
    private IEnumerator Start()
    {
        yield return new WaitForSeconds(lifeTime);
        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject == Owner)
            return;
        collision.gameObject.TryGetComponent(out Health health);
        if (health) 
            health.DealDamage(damage);
        Destroy(gameObject);
    }
}
