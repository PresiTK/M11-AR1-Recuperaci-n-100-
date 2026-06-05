using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class GrenadeController : MonoBehaviour
{
    Rigidbody rb;
    public LayerMask mask;
    public float launchForce = 2f;
    public float timer = 3;
    public float radius = 5f;
    public float explosionForce = 3f;
    public GameObject particles;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * launchForce, ForceMode.Impulse);
    }

    private void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0f)
        {
            Explode();
        }
    }
    void Explode()
    {
        if (particles != null)
            Instantiate(particles, transform.position, Quaternion.identity);

        Collider[] hits = Physics.OverlapSphere(transform.position, radius, mask);

        foreach (Collider hit in hits)
        {
            Rigidbody hitRb = hit.attachedRigidbody;
            if (hitRb != null)
                hitRb.AddExplosionForce(explosionForce, transform.position, radius);

            if (hit.CompareTag("Enemy"))
            {
                EnemyController enemy = hit.GetComponent<EnemyController>();
                enemy.Kill();
            }

        }
        Destroy(gameObject);
    }
}
