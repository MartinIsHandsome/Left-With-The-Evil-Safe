using UnityEngine;
using System.Collections.Generic;

public class TurretScript : MonoBehaviour
{
    public float detectionRange = 10f;
    public GameObject bulletPrefab;
    public Transform bulletSpawnPoint;
    public float bulletForce = 10f;
    private Transform target; 
    private List<Transform> targetsInRange = new List<Transform>();

    private float lastFireTime;
    public float fireInterval = 5f;

    private void Start()
    {
        lastFireTime = Time.time;
    }

    private void Update()
    {
        // Find all gameObjects with the "ScriptForEnemy" script
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        // Clear the list of targets in range
        targetsInRange.Clear();

        foreach (var enemy in enemies)
        {
            float distanceToEnemy = Vector2.Distance(transform.position, enemy.transform.position);

            // Check if the enemy is within the detection range
            if (distanceToEnemy <= detectionRange)
            {
                targetsInRange.Add(enemy.transform);
            }
        }

        // Check if there are targets in range
        if (targetsInRange.Count > 0)
        {
            // Find the closest target among the targets in range
            target = FindClosestTarget(targetsInRange);

            // Rotate the turret to look at the closest target
            Vector3 targetDirection = target.position - transform.position;
            float angle = Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, angle));

            // Check if it's time to fire
            if (Time.time - lastFireTime >= fireInterval)
            {
                Fire();
                lastFireTime = Time.time;
            }
        }
    }

    private Transform FindClosestTarget(List<Transform> targets)
    {
        Transform closestTarget = null;
        float closestDistance = Mathf.Infinity;

        foreach (var target in targets)
        {
            float distance = Vector2.Distance(transform.position, target.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestTarget = target;
            }
        }

        return closestTarget;
    }

    private void Fire()
    {


        // Instantiate the bullet
        GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, Quaternion.identity);

        // Apply force to the bullet
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            Vector2 fireDirection = (target.position - bulletSpawnPoint.position).normalized;
            rb.velocity = fireDirection * bulletForce;
        }


        // Apply bullet damage
        BulletScriptDissapear bulletScript = bullet.GetComponent<BulletScriptDissapear>();
        if (bulletScript != null)
        {
            bulletScript.damage = 10;
        }
    }
}
