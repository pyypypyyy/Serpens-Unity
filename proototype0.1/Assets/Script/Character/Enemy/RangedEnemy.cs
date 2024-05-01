using UnityEngine;

public class RangedEnemy : Enemy
{
    [Header("Ranged Attack")]
    public GameObject projectilePrefab; 
    public Transform firePoint; 
    public float projectileSpeed = 5f; 
    public float attackCooldown = 1f; 
    private float attackTimer = 0f; 

    protected override void Update()
    {
        base.Update();
        if (player != null)
        {
            // Check attack cooldowns and fire long range attacks at the right distance.
            if (attackTimer <= 0f && Vector2.Distance(transform.position, player.position) <= attackDistance)
            {
                Attack(); 
                attackTimer = attackCooldown; // Reset attack cooldowns
            }
            else
            {
                attackTimer -= Time.deltaTime; // Update attack cooldowns
            }
        }
    }

    private void Attack()
    {
        // Create and launch remote attacks
        GameObject projectile = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);

        // Calculate the direction vector towards the player
        Vector3 direction = (player.position - firePoint.position).normalized;


        // Calculate the initial rotation angle of the bullet (towards the player)
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + 180f;

        // Set the rotation angle and speed of the bullet
        projectile.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        // Set the speed of the bullet
        projectile.GetComponent<Rigidbody2D>().velocity = direction * projectileSpeed;
    }

    // Animation event method that triggers the firing of a bullet at a specific frame of the attack animation
    public void FireProjectile()
    {
        Attack(); 
    }
}
