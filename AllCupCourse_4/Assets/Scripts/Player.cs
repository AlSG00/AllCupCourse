using UnityEngine;

public class Player : Entity {
    [SerializeField] private GameObject bullet;
    [SerializeField] private Transform firePoint;
    [SerializeField] private float shootCooldown = 0.1f;
    [SerializeField] private float speed = 1f;
    [SerializeField] private AudioSource shootSound;

    private Transform selfTransform;
    private float timeToNextShoot = 0f;


    private void Awake() {
        selfTransform = GetComponent<Transform>();
    }

    private void FixedUpdate() {
        timeToNextShoot -= Time.fixedDeltaTime;
    }

    public void Move(Vector3 direction) {
        selfTransform.Translate(speed * direction);
    }

    public void TryToShoot() {
        if (timeToNextShoot <= 0) {
            /*if (shootSound.isPlaying) {
                shootSound.PlayScheduled(shootSound.clip.length - shootSound.time);
            }*/
            shootSound.Play();
            Instantiate(bullet, firePoint.position, firePoint.rotation);
            timeToNextShoot = shootCooldown;
        }
    }


}
