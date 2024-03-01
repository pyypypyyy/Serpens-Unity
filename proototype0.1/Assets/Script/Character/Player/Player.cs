using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    [Header("MeleeAttack")]
    public float meleeAttackDamage;
    public Vector2 attackSize = new Vector2(1f,1f);
    public float offsetX = 1f;
    public float offsetY = 1f;
    public LayerMask enemyLayer;

    private SpriteRenderer spriteRenderer;
    private Vector2 AttackAreaPos;

    public void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    void MeleeAttackAnimEvent(float isAttack)
    {
        //center offset
        AttackAreaPos = transform.position;
        //if flip
        offsetX = spriteRenderer.flipX ? -Mathf.Abs(offsetX) : Mathf.Abs(offsetX);
        
        AttackAreaPos.x += offsetX;
        AttackAreaPos.y += offsetY;
        
        
        
        Collider2D[] hitColliders = Physics2D.OverlapBoxAll(AttackAreaPos, attackSize, 0f, enemyLayer);

        foreach(Collider2D hitCollider in hitColliders)
        {
            hitCollider.GetComponent<Character>().TakeDamage(meleeAttackDamage);
        }
    }

    private void OnDrawGizmosSelected()
    {
        
        
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(AttackAreaPos, attackSize);
    }
}
