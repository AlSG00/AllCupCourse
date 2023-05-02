using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Player : MonoBehaviour {

    static private string attackTriggerName = "Attack";

    static private string axisHorizontal = "Horizontal";

    static private LayerMask mobMask {
        get {
            return LayerMask.GetMask("Mobs");
        }
    }

    static private LayerMask defaultMask {
        get {
            return LayerMask.GetMask("Default");
        }
    }

    static private float minActionCooldown = 1.2f;

    [SerializeField] private float actionCooldown = 1.2f;
    [SerializeField] private Transform aimTransform;


    private Collider2D selfCollider;
    private Transform selfTransform;

    private Animator selfAnimator;
    private float timeToAction = 0f;
    private List<Collider2D> groundColliders = new List<Collider2D>();

    private float rightRotationValue = 0f;
    private float leftRotationValue = 180f;
    private bool readyToSelect = false;
    private bool teleportTargetSelected = false;
    public Camera playerCamera;
    public LayerMask layerToCheck;

    private void Awake() {
        selfAnimator = GetComponent<Animator>();
        selfCollider = GetComponent<Collider2D>();
        selfTransform = GetComponent<Transform>();
    }

    [Serializable] private class Staff {
        [SerializeField] private float physicalDamage = 200;
        [SerializeField] private float fireDamage = 715;
        private const float voidDamage = 0.015f;

        public void Use(IDamagable aim) {
            if (aim != null) {
                aim.ApplyDamage(physicalDamage, fireDamage, voidDamage);
            }
        }
    }

    [SerializeField] private Staff weapon;

    private bool isOnGround {
        get {
            return groundColliders.Count > 0;
        }
    }

    private void OnCollisionStay2D(Collision2D coll) {
        if (!groundColliders.Contains(coll.collider)) {
            foreach (var p in coll.contacts) {
                if (p.point.y < selfCollider.bounds.min.y) {
                    groundColliders.Add(coll.collider);
                    break;
                }
            }
                
        } 
    }

    private void OnCollisionExit2D(Collision2D coll) {
        if (groundColliders.Contains(coll.collider)) {
            groundColliders.Remove(coll.collider);
        }
    }

    private void OnMouseEnter()
    {
        readyToSelect = true;
    }

    private void OnMouseExit()
    {
        readyToSelect = false;
    }

    private void TryTeleport(Vector3 position) {
        Vector2 positionToCheck = new Vector2(
            position.x,
            position.y + (selfCollider.bounds.size.y / 2)
            );

        if (teleportTargetSelected)
        {
            teleportTargetSelected = false;
            RaycastHit2D hitInfo = Physics2D.BoxCast(positionToCheck, selfCollider.bounds.size, 0f, Vector2.zero, 0f, layerToCheck);

            if (hitInfo)
            {
                Debug.LogWarning(hitInfo.collider.name);
                return;
            }

            transform.position = new Vector2(
                position.x,
                position.y + (selfCollider.bounds.size.y / 2)
                );
        }
    }

    private void PrepareTeleport()
    {
        if (readyToSelect)
        {
            teleportTargetSelected = true;
            Debug.Log(teleportTargetSelected);
        }
    }

    private void Attack() {
        selfAnimator.SetTrigger(attackTriggerName);
        weapon.Use(Physics2D.BoxCast(new Vector2(aimTransform.position.x, aimTransform.position.y), new Vector2(3, 2), 0, Vector3.zero, Mathf.Infinity, mobMask).collider?.gameObject?.GetComponent<IDamagable>());
        timeToAction = Mathf.Max(minActionCooldown, actionCooldown);
    }

    private void TurnRight() {
        transform.rotation = Quaternion.Euler(
            transform.rotation.x,
            rightRotationValue,
            transform.rotation.z
            );
    }

    private void TurnLeft() {
        transform.rotation = Quaternion.Euler(
            transform.rotation.x,
            leftRotationValue,
            transform.rotation.z
            );
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.A))
        {
            TurnLeft();
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            TurnRight();
        }

        if (Input.GetMouseButtonUp(0))
        {
            if (teleportTargetSelected)
            {
                TryTeleport(playerCamera.ScreenToWorldPoint(Input.mousePosition));
            }
            else
            {
                PrepareTeleport();
            }
        }

        if ((Input.GetKeyDown(KeyCode.Space)))
        {
            if (timeToAction <= 0)
            {
                Attack();
            }
        }

        timeToAction -= Time.deltaTime;
    }

}
