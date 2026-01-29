using UnityEngine;

public class ActorHealth : MonoBehaviour
{
    [SerializeField] int maxHealth = 100;
    [SerializeField] int hp;

    Actor actor;

    private void Awake()
    {
        actor = GetComponent<Actor>();
    }

    void OnEnable()
    {
        hp = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        if (!actor.IsAlive) return;

        hp -= damage;
        
        if(hp <= 0 )
        {
            actor.Kill();
        }
    }
}
