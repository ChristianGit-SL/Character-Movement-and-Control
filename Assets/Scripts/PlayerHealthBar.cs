using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class HealthBar : MonoBehaviour {

    private float _maxHealth = 100f;
    private float _currentHealth;
    [SerializeField]
    private Image _healthBarFill;
    [SerializeField]
    private float _fillSpeed;
    [SerializeField]
    private Gradient _color;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start() {
        _currentHealth = _maxHealth;
    }

    public void UpdateHealth(float amount) {
        _currentHealth += amount;
        _currentHealth = Mathf.Clamp(_currentHealth, 0f, _maxHealth);
        UpdateHealthBar();
    }

    private void UpdateHealthBar() {
        float targetFillAmount = _currentHealth / _maxHealth;
        // _healthBarFill.fillAmount = targetFillAmount;
        _healthBarFill.DOFillAmount(targetFillAmount, _fillSpeed);
        // _healthBarFill.color = _color.Evaluate(targetFillAmount);
        _healthBarFill.DOColor(_color.Evaluate(targetFillAmount), _fillSpeed);
    }

    
}
