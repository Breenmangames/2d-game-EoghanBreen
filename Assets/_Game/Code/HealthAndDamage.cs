using Unity.VisualScripting;
using UnityEngine;

public class Damage : MonoBehaviour
{

    [SerializeField] 
    private float _MaxHealth = 100;
    [SerializeField]
    private bool _isAlive = true;



    Animator Animator;


 

    private bool isInvincible = false;
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
    private float timeSinceHit= 0f;
    public float invincibilityTimer = 0.25f;

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

    public void hit(int damage)
    {
        if (isAlive)
        {
            Health -= damage;
            isInvincible = true;
        }
    }
    private void Awake()
    {
        Animator = GetComponent<Animator>();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
   
    public void Hit(int damage)
    {
        if (isAlive)
        {
            Health -= damage;
            isInvincible = true;
        }
    }


    private void Update()
    {

        if (isInvincible)
        {
            if (timeSinceHit > invincibilityTimer)
            {
                isInvincible = false;
                timeSinceHit = 0;

            }//invincibility timer logic here
        }
        timeSinceHit += Time.deltaTime;
        Hit(10);
    }
   
  
}

