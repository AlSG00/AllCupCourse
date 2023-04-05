using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Player : MonoBehaviour 
{

    static private string attackTriggerName = "Attack";
    static private string isMovingBoleanName = "isMoving";

    static private string axisHorizontal = "Horizontal";

    static private LayerMask mobMask 
    {
        get 
        {
            return LayerMask.GetMask("Mobs");
        }
    }

    static private float minHorizontalSpeed = 0.1f;
    static private float minActionCooldown = 1.2f;

    [SerializeField] private float actionCooldown = 1.2f;
    [SerializeField] private Transform aimTransform;
    [SerializeField] private float jumpPower = 350f;
    [SerializeField] private float jumpRaycastDensity = 2f;
    [SerializeField] private LayerMask layerToHit;
    [SerializeField] private float speed = 2f;


    private Collider2D selfCollider;
    private Rigidbody2D selfRigidbody;
    private Transform selfTransform;

    private Animator selfAnimator;
    private float timeToAction = 0f;
    private List<Collider2D> groundColliders = new List<Collider2D>();


    private void Awake() 
    {
        selfAnimator = GetComponent<Animator>();
        selfRigidbody = GetComponent<Rigidbody2D>();
        selfCollider = GetComponent<Collider2D>();
        selfTransform = GetComponent<Transform>();
    }

    [Serializable] private class Staff 
    {
        [SerializeField] private float physicalDamage = 200;
        [SerializeField] private float fireDamage = 715;
        private const float voidDamage = 0.015f;

        public void Use(IDamagable aim) 
        {
            if (aim != null) 
            {
                aim.ApplyDamage(physicalDamage, fireDamage, voidDamage);
            }
        }
    }

    [SerializeField] private Staff weapon;

    private bool isOnGround 
    {
        get 
        {
            return groundColliders.Count > 0;
        }
    }

    private void OnCollisionStay2D(Collision2D coll) 
    {
        if (!groundColliders.Contains(coll.collider)) 
        {
            foreach (var p in coll.contacts) 
            {
                if (p.point.y < selfCollider.bounds.min.y) 
                {
                    groundColliders.Add(coll.collider);
                    break;
                }
            }
                
        } 
    }

    void OnCollisionExit2D(Collision2D coll) 
    {
        if (groundColliders.Contains(coll.collider)) {
            groundColliders.Remove(coll.collider);
        }
    }



    private void Attack() 
    {
        selfAnimator.SetTrigger(attackTriggerName);
        weapon.Use(Physics2D.BoxCast(new Vector2(aimTransform.position.x, aimTransform.position.y), new Vector2(3, 2), 0, Vector3.zero, Mathf.Infinity, mobMask).collider?.gameObject?.GetComponent<IDamagable>());
        timeToAction = Mathf.Max(minActionCooldown, actionCooldown);
    }

    private void Jump() 
    {
        if (GroundCheck())
        {
            selfRigidbody.AddForce(Vector2.up * jumpPower);
        }
    }

    private void Move(float horizontalSpeed) 
    {        
        Debug.Log(Input.GetAxisRaw(axisHorizontal));
        if (Input.GetAxisRaw(axisHorizontal) != 0)
        {
            selfAnimator.SetBool(isMovingBoleanName, true);
            if (Input.GetAxisRaw(axisHorizontal) > 0)
            {
                transform.rotation = Quaternion.Euler(0, 0, 0);
                transform.Translate(Input.GetAxisRaw(axisHorizontal) * horizontalSpeed * Time.deltaTime, 0, 0);
            }
            else
            {
                transform.rotation = Quaternion.Euler(0, 180, 0);
                transform.Translate(Input.GetAxisRaw(axisHorizontal) * -horizontalSpeed * Time.deltaTime, 0, 0);
            }
        }
        else
        {
            selfAnimator.SetBool(isMovingBoleanName, false);
        }
    }

    private bool GroundCheck()
    {
        Ray2D selfRay;
        RaycastHit2D selfHit;
        
        Debug.DrawRay(transform.position, Vector2.down, Color.red, jumpRaycastDensity);
        if (Physics2D.Raycast(transform.position, Vector2.down, jumpRaycastDensity, layerToHit))
        {
            return true;
        }

        return false;
    }

    private void Update() 
    {
        // дл€ настройки анимации используй параметры анимации типа boolean дл€ движени€ и типа trigger дл€ атаки в методе Attack
        Move(speed);

        if (Input.GetKeyDown(KeyCode.W))
        { 
            Jump();
        }

        if (timeToAction <= 0)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Attack();
            }
        }

        timeToAction -= Time.deltaTime;
    }
}
