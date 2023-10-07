using UnityEngine;

public class Blaster : MonoBehaviour, IAttack
{
    [SerializeField] private GameObject bullet;
    [SerializeField] private float bulletSpeed;
    
    public void Attack()
    {
        var bulletObject = Instantiate(bullet, transform.position, Quaternion.identity);
        bulletObject.TryGetComponent(out Rigidbody body);
        body.velocity = transform.forward * bulletSpeed;
    }
}
