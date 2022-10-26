using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.UI;
using static UnityEditor.PlayerSettings;

public class RangeEnemy : MonoBehaviour
{
    /* * * * * * * * 큰 범위 변수 선언 * * * * * * * */
    Animator anim;
    Rigidbody2D rigid;
    ParticleSystem hitEffect;
    ParticleSystem DeadEffect;
    BoxCollider2D CScollider;


    /* * * * * * * * 기본 체력 * * * * * * * */
    float enemyHP = 10f;
    float maxHP = 10f;

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
    

    //공격 트리거 오브젝트
    int attackPower = 2;

    public float distance;
    public LayerMask isLayer;
    public float atkDistance;
    public GameObject bullet;
    public Transform poss;
    public float cooltime;
    float currenttime = 0;

    void Awake()
    {
        anim = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody2D>();
        hitEffect = transform.GetChild(1).GetComponent<ParticleSystem>();
        DeadEffect = transform.GetChild(2).GetComponent<ParticleSystem>();
        CScollider = GetComponent<BoxCollider2D>();
    }

    void Start()
    {
        Invoke("Think", 3f);
    }


    void Update()
    {
        
    }

    void FixedUpdate()
    {
        RaycastHit2D raycastLeft = Physics2D.Raycast(transform.position, transform.right * -1, distance, isLayer);
        RaycastHit2D raycastRight = Physics2D.Raycast(transform.position, transform.right, distance, isLayer);

        if (raycastLeft.collider != null || raycastRight.collider != null)
        {
            if (raycastLeft.collider != null)
            {
                transform.localScale = new Vector3(1, 1, 1);
            }
            else if (raycastRight.collider != null)
            {
                transform.localScale = new Vector3(-1, 1, 1);
            }

            if (currenttime <= 0)
            {
                GameObject bulletcopy = Instantiate(bullet, poss.position, transform.rotation);
                StartCoroutine(fire());
                currenttime = cooltime;
            }
            Attack();
        }
        else
        {
            Move();
        }
        currenttime -= Time.deltaTime;
    }

    IEnumerator fire()
    {
        yield return new WaitForSeconds(3f);
        anim.SetTrigger("attack");
    }

    /* * * * * * * * 이동 * * * * * * * */
    public int nextMove;

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

        if (nextMove == -1)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if (nextMove == 1)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }

        CancelInvoke();
        Invoke("Think", 2);
    }

    public Transform pos;
    public Vector2 boxSize;

    void Attack()
    {
        Collider2D[] collider2Ds = Physics2D.OverlapBoxAll(pos.position, boxSize, 0);
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

        EnemyHP -= damage;
        rigid.AddForce(transform.right * KnockBack, ForceMode2D.Impulse);
        hitEffect.Play();
    }

    //죽음
    void Dead()
    {
        DeadEffect.Play();
        CScollider.enabled = false;
        ItemDrop();
        Destroy(gameObject, 1f);
    }

    void ItemDrop()
    {
        //float randomSelect = Random.Range(0.0f, 1.0f);

        //if (randomSelect < 0.1f)
        //{
        //    ItemFactory.MakeItem(ItemIDCode.Coin_Gold, transform.position, true);
        //}
        //else if (randomSelect < 0.2f)
        //{
        //    ItemFactory.MakeItem(ItemIDCode.Coin_Silver, transform.position, true);
        //}
        //else if (randomSelect < 0.5f)
        //{
        //    ItemFactory.MakeItem(ItemIDCode.HealingPotion, transform.position, true);
        //}
        //else
        //{
        //    ItemFactory.MakeItem(ItemIDCode.Coin_Copper, transform.position, true);
        //}
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(pos.position, boxSize);
    }
}
