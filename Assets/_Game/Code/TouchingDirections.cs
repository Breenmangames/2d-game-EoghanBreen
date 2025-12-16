using UnityEngine;

public class TouchingDirections : MonoBehaviour
{

    public ContactFilter2D contactFilter;
    public float groundDistance = 0.05f;
    public float wallCheckDistance = 0.2f;
    public float ceilingDistance = 0.05f;
    CapsuleCollider2D touchingCol;
    Animator animator;
    RaycastHit2D[] groundHits = new RaycastHit2D[2];
    RaycastHit2D[] wallHits = new RaycastHit2D[2];
    RaycastHit2D[] ceilingHits = new RaycastHit2D[2];

    [SerializeField]
    private bool _isGrounded;
    [SerializeField]
    private bool _isOnWall;
    [SerializeField]
    private bool _isOnCeiling;


    private Vector2 wallCheckDirection => gameObject.transform.localScale.x > 0 ? Vector2.right : Vector2.left;
    public bool isGrounded
    {
        get
        {
            return _isGrounded;
        }
        private set
        {
            _isGrounded = value;
            animator.SetBool(AnimationStrings.isGrounded, value);
        }
    }
    public bool isOnWall
    {
        get
        {
            return _isOnWall;
        }
        private set
        {
            _isOnWall = value;
            animator.SetBool(AnimationStrings.isOnWall, value);
        }
    }

    public bool isOnCeiling
    {
        get
        {
            return _isOnCeiling;
        }
        private set
        {
            _isOnCeiling = value;
            animator.SetBool(AnimationStrings.isOnCeiling, value);
        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private void Awake()
    {
        touchingCol = GetComponent<CapsuleCollider2D>();
        animator = GetComponent<Animator>();
    }

   

    // Update is called once per frame
    void FixedUpdate()
    {
       isGrounded = touchingCol.Cast(Vector2.down, contactFilter, groundHits, groundDistance) >0;
       isOnWall = touchingCol.Cast(wallCheckDirection, contactFilter, wallHits, wallCheckDistance) > 0;
       isOnCeiling = touchingCol.Cast(Vector2.up, contactFilter, ceilingHits, ceilingDistance) > 0;
    }
}
