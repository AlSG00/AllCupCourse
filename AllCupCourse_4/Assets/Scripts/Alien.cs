using UnityEngine;

public class Alien : Entity {
    [SerializeField] private AudioSource deathSound;

    protected override void ApplyDamage() {
        deathSound.Play();
        base.ApplyDamage();
    }

    public void SetDeathSound(AudioSource deathSound) {
        this.deathSound = deathSound;
    }
}