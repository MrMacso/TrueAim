using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    private PlayerController player;
    private Animator animator;
    public Image healthBar;
    private NavMeshAgent agent;

    public int currentHealth = 100;
    private int maxHealth = 100;
    private int damageDeal = 10;

    private void Start()
    {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        player = FindObjectOfType<PlayerController>();
    }
    private void Update()
    {
        agent.destination = player.transform.position;
    }
    public void TakeDamage(int damage)
    {
        //deals damage
        currentHealth -= damage;

        float percentOfHealth = (float)currentHealth / (float)maxHealth;
        healthBar.fillAmount = percentOfHealth;

        if (currentHealth <= 0f)
             Death(); 
    }
    void Death()
    {
        ScoreManager.instance.AddPoint();
        Destroy(gameObject);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("MainCamera"))
        {
            player.RecieveDamage(damageDeal);
        }
    }
}
