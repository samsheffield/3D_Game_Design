using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public Image healthBar;
    public float maxHealth = 3f;
    public float health;

    void Start()
    {
        health = maxHealth;
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Projectile"))
        {

        }
    }

    public void UpdateHealth(float update)
    {

    }


}
