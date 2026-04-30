using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class PlayerHealthBar : MonoBehaviour {  

    [SerializeField]
    private PlayerHealth ph;

    [SerializeField]
    private Image _healthBarFill;
    [SerializeField]
    private float _fillSpeed;
    [SerializeField]
    private Gradient _color;

    private void Update() {
        float targetFillAmount = ph.getHealth();
        _healthBarFill.DOFillAmount(targetFillAmount, _fillSpeed);
        _healthBarFill.DOColor(_color.Evaluate(targetFillAmount), _fillSpeed);
    }
}
