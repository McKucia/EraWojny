using FishNet.Object;
using System;
using Unity.VisualScripting;
using UnityEngine;

public class Unit : NetworkBehaviour
{
    [SerializeField] protected float moveSpeed;
    [SerializeField] protected float spawnYPosition;
    [SerializeField] protected float attackDistance;
    [SerializeField] protected float strength;
    [SerializeField] string enemyTag;
    [SerializeField] Animator animator;
    [SerializeField] bool inverted = false;
    [SerializeField] int numAttacks = 4;

    bool isMoving = true;
    bool isAttacking = false;
    int prevAttackType = 0;

    public override void OnStartClient()
    {
        base.OnStartClient();
    }

    void Start()
    {
        Init();
    }

    void Update()
    {
        if (isMoving) Move();
        CheckEnemyDistance();
    }

    [Client(RequireOwnership = true)]
    void Init()
    {
        float angle = transform.eulerAngles.y > 180 ? transform.eulerAngles.y - 360 : transform.eulerAngles.y;
        inverted = angle < 0;

        Cursor.lockState = CursorLockMode.Confined;
    }

    void Move()
    {
        transform.position += new Vector3((inverted ? -1 : 1) * moveSpeed * Time.smoothDeltaTime, 0, 0);
    }

    void CheckEnemyDistance()
    {
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out RaycastHit hit, attackDistance))
        {
            if (hit.collider.CompareTag(enemyTag))
            {
                if (isAttacking) return;
                isAttacking = true;
                isMoving = false;
                Attack();
                animator.SetBool("IsWalking", false);
            }
        }
        else
        {
            isAttacking = false;
            isMoving = true;
            animator.SetBool("IsWalking", true);
        }
    }

    // animation event
    public void Attack()
    {
        if (!isAttacking)
        {
            animator.SetBool("IsAttacking", false);
            return;
        }

        int attackType = prevAttackType;

        while (attackType == prevAttackType)
            attackType = UnityEngine.Random.Range(0, numAttacks);

        prevAttackType = attackType;

        animator.SetInteger("AttackType", attackType);
        animator.SetTrigger("Attack");
    }
}
