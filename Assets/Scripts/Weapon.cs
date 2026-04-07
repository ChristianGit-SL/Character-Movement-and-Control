using UnityEngine;

public class Weapon : MonoBehaviour {

    [SerializeField]
    private float damage;

    private void OnTriggerEnter(Collider other) {
        if(other.CompareTag("Enemy")) {
            EnemyHealthBar enemy = other.GetComponentInChildren<EnemyHealthBar>();
            enemy.TakeDamage(damage);
        }
    }
    
}
