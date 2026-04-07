using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class EnemyHealthBar : MonoBehaviour {

    private float maxHealth = 100f;
    private float currentHealth;

    [SerializeField]
    private Image healthBarFill;

    [SerializeField]
    private float fillSpeed;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start() {
        currentHealth = maxHealth;
    }

    public void TakeDamage(float damage) {
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0f, maxHealth);
        UpdateHealthBar();
        Debug.Log(currentHealth);

        if(currentHealth <= 0f) {
            healthBarFill.DOKill();
            Destroy(transform.parent.gameObject);
        }
    }

    private void UpdateHealthBar() {
        float targetFillAmount = currentHealth / maxHealth;
        healthBarFill.DOFillAmount(targetFillAmount, fillSpeed);
    }
}
