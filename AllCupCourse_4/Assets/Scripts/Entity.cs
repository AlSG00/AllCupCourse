using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
public class Entity : MonoBehaviour {
    private enum Team {
        Aliens,
        Player,
        Border
    }


    [SerializeField] private Team team;
    [SerializeField] private bool isImmortal;
    private void OnCollisionEnter2D(Collision2D collision) {
        Entity entity = collision.gameObject.GetComponent<Entity>();
        if (entity != null) {
            if (entity.team != team && entity.isImmortal == false) {
                entity.ApplyDamage();
            }
        }

    }

    virtual protected void ApplyDamage() {
        Destroy(gameObject);
    }
}
