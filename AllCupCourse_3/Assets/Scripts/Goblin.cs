using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Goblin : MonoBehaviour, IDamagable {

    static private string hitTriggerName = "Hit";

    [SerializeField] private float hp = 0f;                   // здоровье моба
    [SerializeField] private float armor = 0f;                // броня моба
    [SerializeField] private float armorStrength = 0f;    // уменьшение брони после каждого удара
	[SerializeField] private int n = 0; // количество ударов
    private Animator selfAnimator;

    private void Awake() {
        selfAnimator = GetComponent<Animator>();
    }

    public void ApplyDamage(float physicalDamage, float damageStrength) {
        // в строке 20 напишите код с вычислениями урона по заданию вместо присвоения значения 0f
        float damage = physicalDamage - armor - (n * damageStrength);

        if (damage < 0) {
            hp += damage;
        }
        else
        {
            hp -= damage;
        }
		n += 1;
		armor -= armorStrength;
        damage -= damageStrength;
        selfAnimator.SetTrigger(hitTriggerName);
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