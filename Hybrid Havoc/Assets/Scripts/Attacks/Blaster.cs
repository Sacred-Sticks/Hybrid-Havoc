using UnityEngine;

public class Blaster : MonoBehaviour, IAttack
{
    [SerializeField] private GameObject bullet;
    [SerializeField] private float bulletSpeed;
    
    public void Attack()
    {
        var bulletObject = Instantiate(this.bullet, transform.position + transform.forward, Quaternion.identity);
        bulletObject.TryGetComponent(out Rigidbody body);
        bulletObject.TryGetComponent(out Bullet bullet);
        if (!body || !bullet)
            return;
        body.velocity = transform.forward * bulletSpeed;
        bullet.Owner = gameObject;
    }
}
