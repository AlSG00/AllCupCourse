using System.Collections;
using UnityEngine;

public class SpawnArea : MonoBehaviour {
    [SerializeField] private Transform leftTop;
    [SerializeField] private Transform rightBottom;
    [SerializeField] private GameObject spawn;
    [SerializeField] private float spawnCooldown;
    [SerializeField] private float spawnChance;
    [SerializeField] private AudioSource alienDeathSound;

    private float left;
    private float up;
    private float right;
    private float down;

    private Coroutine spawnCoroutine;

    private void Awake() {
        left = leftTop.position.x;
        up = leftTop.position.y;
        right = rightBottom.position.x;
        down = rightBottom.position.y;
        spawnCoroutine = StartCoroutine(TryToSpawn());
    }
    private IEnumerator TryToSpawn() {
        while (true) {
            if (Random.Range(0, 1) < spawnChance) {
                float x = Random.Range(left, right);
                float y = Random.Range(down, up);

                GameObject currentSpawn = Instantiate(spawn, new Vector3(x, y, 0), Quaternion.identity);
                Alien alien;
                if (currentSpawn.TryGetComponent<Alien>(out alien)) {
                    alien.SetDeathSound(alienDeathSound);
                }
            }
            yield return new WaitForSeconds(spawnCooldown);
        }
    }

    private void OnDestroy() {
        StopCoroutine(spawnCoroutine);
    }
}
