using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Goblin : MonoBehaviour, IDamagable {

    static private string hitTriggerName = "Hit";

    [SerializeField] private float hp = 792;
    [SerializeField] private float armor = 239;
    [SerializeField] private float fireResistProcent = 50f;

    private Animator selfAnimator;

    private void Awake() {
        selfAnimator = GetComponent<Animator>();
    }

    public void ApplyDamage(float physicalDamage, float fireDamage, float voidDamage) {
        // damage calculations
        float damage = hp * voidDamage + fireDamage * (1 - fireResistProcent/100) + physicalDamage - armor;

        if (damage > 0) {
            hp -= damage;
        }
        if (selfAnimator != null) {
            selfAnimator.SetTrigger(hitTriggerName);
        }
    }

    public void onHitAnimationEnd() {
        if (hp <= 0) {
            Kill();
        }
    }

    private void Kill() {
        Destroy(gameObject);
    }
}