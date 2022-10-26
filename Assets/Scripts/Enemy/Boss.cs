using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;
using static UnityEditor.ShaderData;

public class Boss : MonoBehaviour
{
    /* * * * * * * * 큰 범위 변수 선언 * * * * * * * */
    ParticleSystem hitEffect;
    ParticleSystem DeadEffect;
    BoxCollider2D CScollider;
    Animator anim;
    Rigidbody2D rigid;
    Player player;
    Transform target;

    /* * * * * * * * 보스 기본 체력 * * * * * * * */
    float bossHP = 100f;
    public float maxHP = 100f;

    public float BossHP
    {
        get => bossHP;
        set
        {
            bossHP = value;
            if (bossHP <= 0)
            {
                bossHP = 0;
                Dead();
            }
            bossHP = Mathf.Min(bossHP, maxHP);
        }
    }

    
    void Awake()
    {
        anim = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody2D>();
        CScollider = GetComponent<BoxCollider2D>();
        hitEffect = transform.GetChild(4).GetComponent<ParticleSystem>();
        DeadEffect = transform.GetChild(5).GetComponent<ParticleSystem>();
        player = FindObjectOfType<Player>();
    }

    /* * * * * * * * 이동 방향 정하는 코루틴 시작 * * * * * * * */
    void Start()
    {
        StartCoroutine(ChangeMoveDir());
    }

    void Update()
    {
        
    }

    public float distance = 10f;
    public LayerMask isLayer;
    float chasespeed = 0.005f;

    void FixedUpdate()
    {
        RaycastHit2D raycastLeft = Physics2D.Raycast(transform.position, transform.right * -1, distance, isLayer);
        RaycastHit2D raycastRight = Physics2D.Raycast(transform.position, transform.right, distance, isLayer);

        if (raycastLeft.collider != null || raycastRight.collider != null)
        {
            if (raycastLeft.collider != null)
            {
                target = raycastLeft.collider.transform;
                transform.localScale = new Vector3(2, 2, 1);
            }
            else if (raycastRight.collider != null)
            {
                target = raycastRight.collider.transform;
                transform.localScale = new Vector3(-2, 2, 1);
            }

            transform.position = Vector3.Lerp(transform.position, target.position, chasespeed);

        }
        else
        {
            Move();
        }
    }

    /* * * * * * * * 이동 * * * * * * * */
    public int nextMove;

    IEnumerator ChangeMoveDir()
    {
        //-1이면 왼쪽, 1이면 오른쪽, 0이면 가만히
        nextMove = Random.Range(-1, 2);

        anim.SetInteger("move", nextMove);

        yield return new WaitForSeconds(3f);

        StartCoroutine("ChangeMoveDir");
    }

    void Move()
    {
        //nextMove의 랜덤 숫자에 따라 이동함
        rigid.velocity = new Vector2(nextMove, rigid.velocity.y);

        if (nextMove == -1)
        {
            transform.localScale = new Vector3(2, 2, 1);
        }
        else if (nextMove == 1)
        {
            transform.localScale = new Vector3(-2, 2, 1);
        }

        //Vector2 frontVec = new Vector2(rigid.position.x + nextMove, rigid.position.y);
        //Debug.DrawRay(frontVec, Vector3.down, new Color(0, 1, 0));
        //RaycastHit2D raycast = Physics2D.Raycast(frontVec, Vector3.down, 1, LayerMask.GetMask("Background"));
        //if (raycast.collider == null)
        //{
        //    nextMove *= -1;
        //}
    }


    /* * * * * * * * 일반 공격 * * * * * * * */
    int attackPower = 5;

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            Invoke("AttackMotion", 1f);
        }
    }

    void AttackMotion()
    {
        anim.SetTrigger("attack");
        player.TakeDamage(attackPower, transform.position);
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

        BossHP -= damage;
        rigid.AddForce(transform.right * KnockBack, ForceMode2D.Impulse);
        hitEffect.Play();
    }

    /* * * * * * * * 다이 * * * * * * * */
    void Dead()
    {
        DeadEffect.Play();
        CScollider.enabled = false;
        ItemDrop();
        Destroy(gameObject, 0.5f);
    }

    /* * * * * * * * 아이템 드랍 * * * * * * * */
    //무조건 기본 단검을 드랍, 추후 플레이어 원거리 투척 스킬을 위해

    void ItemDrop()
    {
       GameObject obj = Resources.Load("Item/BasicSmallKnife") as GameObject;
       Instantiate(obj, transform.position, Quaternion.identity);
    }
}
