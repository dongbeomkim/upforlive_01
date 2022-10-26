using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;
using static UnityEditor.ShaderData;

public class Enemy : MonoBehaviour
{
    /* * * * * * * * 큰 범위 변수 선언 * * * * * * * */
    Animator anim;
    Rigidbody2D rigid;
    ParticleSystem hitEffect;
    ParticleSystem DeadEffect;
    BoxCollider2D CScollider;
    Collider2D[] collider2Ds;
    Transform target;

    /* * * * * * * * 기본 체력 * * * * * * * */
    float enemyHP = 15f;
    float maxHP = 15f;

    public float EnemyHP
    {
        get => enemyHP;
        set
        {
            enemyHP = value;
            if (enemyHP <= 0)
            {
                enemyHP = 0;
                Dead();
            }
            enemyHP = Mathf.Min(enemyHP, maxHP);
        }
    }


    void Awake()
    {
        anim = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody2D>();
        hitEffect = transform.GetChild(3).GetComponent<ParticleSystem>();
        DeadEffect = transform.GetChild(4).GetComponent<ParticleSystem>();
        CScollider = GetComponent<BoxCollider2D>();
    }

    void Start()
    {
        Invoke("Think", 3f);
    }

    
    void Update()
    {
    }


    /* * * * * * * * 이동 * * * * * * * */
    int nextMove;
    public float distance = 3f;
    public LayerMask isLayer;
    float chasespeed = 0.01f;

    void FixedUpdate()
    {
        RaycastHit2D raycastLeft = Physics2D.Raycast(transform.position, transform.right * -1, distance, isLayer);
        RaycastHit2D raycastRight = Physics2D.Raycast(transform.position, transform.right, distance, isLayer);

        if (raycastLeft.collider != null || raycastRight.collider != null)
        {
            if (raycastLeft.collider != null)
            {
                target = raycastLeft.collider.transform;
                transform.localScale = new Vector3(1, 1, 1);
            }
            else if (raycastRight.collider != null)
            {
                target = raycastRight.collider.transform;
                transform.localScale = new Vector3(-1, 1, 1);
            }

            transform.position = Vector3.Lerp(transform.position, target.position, chasespeed);
            Attack();
        }
        else
        {
            Move();
        }
    }

    void Move()
    {
        rigid.velocity = new Vector2(nextMove, rigid.velocity.y);
        Vector2 frontVec = new Vector2(rigid.position.x + nextMove, rigid.position.y);
        Debug.DrawRay(frontVec, Vector3.down, new Color(0, 1, 0));
        RaycastHit2D raycast = Physics2D.Raycast(frontVec, Vector3.down, 1, LayerMask.GetMask("Background"));
        if (raycast.collider == null)
        {
            Turn();
        }
    }

    void Think()
    {
        nextMove = Random.Range(-1, 2);
        anim.SetInteger("move", nextMove);

        if (nextMove == -1)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if (nextMove == 1)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }

        Invoke("Think", 3f);
    }

    void Turn()
    {
        nextMove *= -1;

        if(nextMove == -1)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if(nextMove == 1)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }

        CancelInvoke();
        Invoke("Think", 2);
    }


    /* * * * * * * * 공격 * * * * * * * */
    public Transform pos;
    public Vector2 boxSize;
    int attackPower = 2;

    void Attack()
    {
        collider2Ds = Physics2D.OverlapBoxAll(pos.position, boxSize, 0);
        foreach (Collider2D collider in collider2Ds)
        {
            if (collider.CompareTag("Player"))
            {
                collider.GetComponent<Player>().TakeDamage(attackPower, transform.position);
            }
        }
    }

    /* * * * * * * * 피격 * * * * * * * */
    float KnockBack = 0;

    public void TakeDamage(float damage, Vector3 back)
    {
        Vector2 dir = back - transform.position;

        if (dir.x < 0)
        {
            KnockBack = 2;
        }
        else if (dir.x > 0)
        {
            KnockBack = -2;
        }
        rigid.AddForce(transform.right * KnockBack, ForceMode2D.Impulse);
        
        EnemyHP -= damage;
        hitEffect.Play();
    }


    /* * * * * * * * 다이 * * * * * * * */
    void Dead()
    {
        DeadEffect.Play();
        CScollider.enabled = false;
        ItemDrop();
        Destroy(gameObject, 1f);
    }

    /* * * * * * * * 아이템 드롭 확률 * * * * * * * */
    void ItemDrop()
    {
        //float randomSelect = Random.Range(0.0f, 1.0f);

        //if(randomSelect < 0.1f)
        //{
        //    ItemFactory.MakeItem(ItemIDCode.Coin_Gold, transform.position, true);
        //}
        //else if(randomSelect < 0.2f)
        //{
        //    ItemFactory.MakeItem(ItemIDCode.Coin_Silver, transform.position, true);
        //}
        //else if(randomSelect < 0.5f)
        //{
        //    ItemFactory.MakeItem(ItemIDCode.HealingPotion, transform.position, true);
        //}
        //else
        //{
        //    ItemFactory.MakeItem(ItemIDCode.Coin_Copper, transform.position, true);
        //}

        GameObject obj = Resources.Load("Item/HealingPotion") as GameObject;
        Instantiate(obj, transform.position, Quaternion.identity);
    }


    /* * * * * * * * 공격범위 기즈모 * * * * * * * */
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(pos.position, boxSize);
    }
}
