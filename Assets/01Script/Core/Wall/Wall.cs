using UnityEngine;

public class Wall : MonoBehaviour
{
    [SerializeField] int maxHP = 500;

    int hp;
    public bool IsAlive { get; private set; } = true;

    void Awake()
    {
        hp = maxHP;
    }

    public void TakeDamage(int dmg)
    {
        if (!IsAlive) return;

        hp -= dmg;
        if (hp <= 0)
            Die();
    }

    void Die()
    {
        IsAlive = false;
        Debug.Log("Wall destroyed!");
        // TODO: trigger lose condition
    }
}
