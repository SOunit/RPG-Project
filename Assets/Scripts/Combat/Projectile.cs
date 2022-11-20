using RPG.Core;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField]
    float speed = 1f;

    [SerializeField]
    bool isHoming = false;

    Health target = null;

    float damage = 0f;

    private void Start()
    {
        transform.LookAt(GetAimLocation());
    }

    void Update()
    {
        if (target == null)
        {
            return;
        }

        if (isHoming && !target.IsDead())
        {
            transform.LookAt(GetAimLocation());
        }
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Health>() != target)
        {
            return;
        }

        // to fix arrow disappear when collide with dead enemy
        if (target.IsDead())
        {
            return;
        }

        target.TakeDamage (damage);
        Destroy(this.gameObject);
    }

    public void SetTarget(Health target, float damage)
    {
        this.target = target;
        this.damage = damage;
    }

    private Vector3 GetAimLocation()
    {
        CapsuleCollider targetCapsule = target.GetComponent<CapsuleCollider>();
        if (targetCapsule == null)
        {
            return target.transform.position;
        }

        return target.transform.position +
        Vector3.up * targetCapsule.height / 2;
    }
}
