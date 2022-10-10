using System;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Player : MonoBehaviour {

    static private string attackTriggerName = "Attack";

    [SerializeField] private float attackCooldown = 0.1f;
    [SerializeField] private Goblin aim;
    
    private float timeToNextAttack = 0f;
    private Animator selfAnimator;

    private void Awake() {
        selfAnimator = GetComponent<Animator>();
    }

    [Serializable] private class Staff {
        [SerializeField] private float physicalDamage = 0f;   // физический урон посоха
        [SerializeField] private float damageStrength = 0f;       // уменьшение урона посоха после каждого удара

        public void Use(IDamagable aim) {
            if (aim != null) {
                aim.ApplyDamage(physicalDamage, damageStrength);
            }
        }
    }

    [SerializeField] private Staff weapon;


    private void Attack() {
        selfAnimator.SetTrigger(attackTriggerName);
        weapon.Use(aim);
        timeToNextAttack = attackCooldown;
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Space) && timeToNextAttack <= 0) {
            Attack();
        }

        timeToNextAttack -= Time.deltaTime;
    }

}
