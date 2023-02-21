using UnityEngine;
using TMPro;
using System;

public class DamageableObstacle : Obstacle, IDamageable
{
    [SerializeField]
    private int health = 5;

    private TextMeshPro healthText;
    private Action OnObstacleDestroyed = delegate () { };
    private Animation anim;

    void Start()
    {
        hitDamage = health;

        healthText = GetComponentInChildren<TextMeshPro>();
        healthText.SetText(health.ToString());

        anim = GetComponent<Animation>();
        destroyOnPlayerCollide = true;
    }

    public virtual void DestroyObject()
    {
        OnObstacleDestroyed();

        // Hide game object before "Destruction Effect" played by "Particle System"
        // "Particle System" will destroy this game object after effect ends
        healthText.enabled = false;
        GetComponent<MeshRenderer>().enabled = false;

        GetComponent<ParticleSystem>().Play();
        GetComponent<ParticleSystem>().Emit(1);

        Destroy(this);
    }

    public override void Interact(object obj)
    {
        if (obj is PlayerCollider && destroyOnPlayerCollide)
        {
            DestroyObject();
        }
    }

    public void TakeDamage(int damage)
    {
        if (health > damage)
        {
            health -= damage;
            healthText.SetText(health.ToString());

            anim.Stop();
            anim.Play();
        }
        else
        {
            health = 0;
            DestroyObject();
        }
    }

    public void SetCallback(Action func)
    {
        OnObstacleDestroyed += func;
    }
}