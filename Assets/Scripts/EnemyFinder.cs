using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFinder : MonoBehaviour
{
    public Transform player;
    private GameObject closestEnemy;

    public float speedRotation = 5f;
    private float closestDistance = 100f;

    private void Start()
    {
        closestEnemy = null;
    }

    private void Update()
    {
        List<GameObject> enemies = GameManager.instance.EnemiesList;
        closestDistance = Mathf.Infinity;
        for (int i = 0; i < enemies.Count; i++)
        {
            GameObject enemy = enemies[i];
            if (enemy == null) continue;

            float distance = Vector3.Distance(player.position, enemy.transform.position);
            if (distance < closestDistance)
            {
                closestEnemy = enemy;
                closestDistance = distance;
            }
        }
        if (closestEnemy == null)
            return;
        transform.position = closestEnemy.transform.position;
        player.rotation = Quaternion.Lerp(player.rotation, Quaternion.LookRotation(closestEnemy.transform.position - player.position), speedRotation * Time.deltaTime);
    }
}
