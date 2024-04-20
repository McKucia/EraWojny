using FishNet.CodeGenerating;
using FishNet.Component.Animating;
using FishNet.Object;
using FishNet.Object.Synchronizing;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class Unit : NetworkBehaviour
{
    public Camera PlayerCamera;

    [SerializeField] float moveSpeed;
    [SerializeField] float spawnYPosition;
    [SerializeField] float attackDistance;
    [SerializeField] string enemyTag;
    [SerializeField] int strength;
    [SerializeField] int numAttacks = 4;
    [SerializeField] Animator animator;
    [SerializeField] NetworkAnimator networkAnimator;
    [SerializeField] HealthBar healthBar;

    readonly SyncVar<bool> isMoving = new SyncVar<bool>(true);
    readonly SyncVar<bool> isAttacking = new SyncVar<bool>(false);
    readonly SyncVar<Unit> currentEnemyUnit = new SyncVar<Unit>();

    // [AllowMutableSyncType]
    // [SerializeField]
    // public SyncVar<int> hp = new SyncVar<int>(10);

    bool inverted = false;
    //bool isAttacking = false;
    bool initialized = false;
    int prevAttackType = 0;
    [SerializeField]
    int hp = 15;

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
        if (IsServerInitialized)
        {
            CheckCollideDistanceServer(transform.position, transform.forward);
            MoveServer();
        }

        if (!base.IsOwner) return;
    }


    void Init()
    {
        isMoving.OnChange += OnIsMoving;
        isAttacking.OnChange += OnIsAttacking;

        float angle = transform.eulerAngles.y > 180 ? transform.eulerAngles.y - 360 : transform.eulerAngles.y;
        inverted = angle < 0;
        animator.SetBool("IsWalking", true);

        Cursor.lockState = CursorLockMode.Confined;

        healthBar.Init(PlayerCamera, inverted, hp);

        initialized = true;
    }

    void MoveServer()
    {
        if (isMoving.Value)
            transform.position += new Vector3(inverted ? -1 : 1, 0f, 0f) * moveSpeed / 20f;
    }

    void CheckCollideDistanceServer(Vector3 position, Vector3 direction)
    {
        if (isAttacking.Value) return;

        RaycastHit hit;
        if (gameObject.scene.GetPhysicsScene().Raycast(position, direction, out hit, attackDistance))
        {
            var hittedObject = hit.collider.gameObject;
            if (!hittedObject.CompareTag(enemyTag) || hittedObject == this.gameObject) return;

            var unit = hittedObject.GetComponent<Unit>();
            if (!unit) return;

            if (unit.inverted == this.inverted)
            {
                isMoving.Value = false;
            }
            else
            {
                isMoving.Value = false;
                isAttacking.Value = true;
                currentEnemyUnit.Value = unit;
            }
        }
        else
        {
            isMoving.Value = true;
        }
    }

    void OnIsMoving(bool prev, bool next, bool asServer)
    {
        if(next == false)
            animator.SetBool("IsWalking", false);
        else
            animator.SetBool("IsWalking", true);
    }

    void OnIsAttacking(bool prev, bool next, bool asServer)
    {
        if (next == true)
            Attack();
    }

    public void TakeDamage(int damage)
    {
        hp -= damage;
        if (hp <= 0)
        {
            Despawn(gameObject);
            return;
        }

        healthBar.UpdateHP(hp);
    }

    #region Animation Events

    public void Attack()
    {
        if (!isAttacking.Value)
            return;

        int attackType = prevAttackType;

        while (attackType == prevAttackType)
            attackType = Random.Range(0, numAttacks);

        prevAttackType = attackType;

        animator.SetInteger("AttackType", attackType);
        networkAnimator.SetTrigger("Attack");
    }

    public void Hit()
    {
        if (!base.IsServerInitialized) return;

        if (currentEnemyUnit.Value.hp - strength <= 0)
        {
            isMoving.Value = true;
            isAttacking.Value = false;
        }
        currentEnemyUnit.Value.TakeDamage(strength);
    }

    #endregion
}
