using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Search;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float lifeTime;
    [SerializeField] private float damage;

    private IEnumerator Start()
    {
        yield return new WaitForSeconds(lifeTime);
        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        collision.gameObject.TryGetComponent(out Health health);
        if (!health)
            return;
        health.DealDamage(damage);
    }
}
