using UnityEngine;
using DG.Tweening;
using TMPro;

public class PlayerHealth : MonoBehaviour {

    private float maxHealth = 100f;
    private float currentHealth;
    public GameObject deathText;

    // private float amount = currentHealth / maxHealth;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {
        currentHealth = maxHealth;
    }

    public void TakeDamage(float damage) {
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0f, maxHealth);

        if(currentHealth <= 0) {
            print("Dead");
            Destroy(gameObject);
            OnPlayerDeath();
        }
    }

    public float getHealth() {
        return currentHealth / maxHealth;
    }

    public void OnPlayerDeath() {
        deathText.SetActive(true);

        
    }


}
