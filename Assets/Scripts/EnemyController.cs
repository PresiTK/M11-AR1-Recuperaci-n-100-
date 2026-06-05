using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class EnemyController : MonoBehaviour
{
    public float speedRotation;
    public float speed = 1f;
    public float stoppingDistance;
    public Transform target;
    private Animator animator;
    private Rigidbody[] skeletonRigidbodies;

    private void Start()
    {

        animator = GetComponent<Animator>();
        skeletonRigidbodies = GetComponentsInChildren<Rigidbody>();
        if (target == null)
        {
            if (GameObject.FindWithTag("Player") != null)
            {
                target = GameObject.FindWithTag("Player").GetComponent<Transform>();
            }
        }
    }
    void Update()
    {
        if (target == null)
            return;

        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(target.position - transform.position), speedRotation * Time.deltaTime);

        float distance = Vector3.Distance(transform.position, target.position);
        animator.SetFloat("Velocity", speed);
        if (distance < stoppingDistance)
        {
            animator.SetFloat("Velocity", speed / (stoppingDistance - distance));
        }

    }

    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
    }

    public void Kill()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.isKinematic = true;
        GetComponent<Animator>().enabled = false;
        GetComponent<Collider>().enabled = false;
        foreach (Rigidbody rb1 in skeletonRigidbodies)
        {
            rb1.isKinematic = false;
        }
        GameManager.instance.EnemiesList.Remove(gameObject);

        Destroy(gameObject, 5f);

    }
}
