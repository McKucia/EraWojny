using UnityEngine;

public class EnemyAnimationEvents : MonoBehaviour
{
    [SerializeField] Unit unit;

    // animation event
    public void AttackEnd()
    {
        unit.Attack();
    }
}
