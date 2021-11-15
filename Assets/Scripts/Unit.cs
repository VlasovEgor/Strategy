using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Unit : SecectableObject
{
    public NavMeshAgent NavMeshAgent;
    public int Price;
    public int Health;
    public int _maxHealth = 5;

    public GameObject HealthBarPrefab;
    private HealthBar _healthBar;


    public override void Start()
    {
        base.Start();
        Health = _maxHealth;
        GameObject healthBar = Instantiate(HealthBarPrefab);
        _healthBar = healthBar.GetComponent<HealthBar>();
        _healthBar.Setup(transform);
    }
    public override void WhenClickOnGround(Vector3 point)
    {
        base.WhenClickOnGround(point);

        NavMeshAgent.SetDestination(point);
    }
    public void TakeDamage(int DamageValue)
    {
        Health -= DamageValue;
        _healthBar.SetHealth(Health, _maxHealth);
        if (Health <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        FindObjectOfType<Managment>().UnSelect(this);
        if (_healthBar)
        {
            Destroy(_healthBar.gameObject);
        }

    }
}

