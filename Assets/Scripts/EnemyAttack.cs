using UnityEngine;

public class EnemyAttack : MonoBehaviour {

    public PlayerHealth pH;
    
    // public void Attack() {
    //     playerHealth.TakeDamage(10);
    // }

    private void OnTriggerEnter(Collider other) {
        if(other.CompareTag("Player")) {
            // playerHealth = other.GetComponent<PlayerHealthBar>();
            
            if(pH != null) {
                pH.TakeDamage(15);
            }
        }
    }
}
