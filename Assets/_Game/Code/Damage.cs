using Unity.VisualScripting;
using UnityEngine;

public class HealthAndDamage : MonoBehaviour
{

    [SerializeField] 
    private float _MaxHealth = 100;
    [SerializeField]
    private bool _isAlive = true;

    Animator Animator;

    public bool isAlive
        { 
        get
        { 
            return _isAlive; 
        }
        set 
        {
            _isAlive = value; 
            Animator.SetBool(AnimationStrings.IsAlive, value);
        }
    }
    public float MaxHealth
    {
        get
        {
            return _MaxHealth;
        }
        set 
        { 
            _MaxHealth = value; 
        }
    }

    [SerializeField]
    private float _Health = 100;
    [SerializeField]
    private bool isInvincible = false;
    private float timeSinceHit = 0;
    private float InvincibilityTimer = 0.5f;

    public float Health

    {
        get
        {
            return _Health;
        }
        set
        {
            _Health = value;

            if (value < 0)
            {
                isAlive = false;
            }

        }
    }


    private void Awake()
    {
        Animator = GetComponent<Animator>();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private void Update()
    {
        if (isInvincible) 
        {
            if (timeSinceHit > InvincibilityTimer) 
            {
                isInvincible = false ;
                
            }
        }
        Hit(10);
    }


    public void Hit(int damage)
    {
        if (isAlive && !isInvincible)
        {
            Health -= damage;
            isInvincible = true;
        }
    }

}
