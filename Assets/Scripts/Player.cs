using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlTypes;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using System.ComponentModel;
using Unity.VisualScripting;
using static UnityEditor.ShaderData;
using UnityEngine.UIElements;


public class Player : MonoBehaviour
{
    /* * * * * * * * 큰 범위 변수 선언 * * * * * * * */
    Rigidbody2D rigid;
    Animator playerAnim;
    GameObject attackCollison;
    BoxCollider2D boxCollider;

    /* * * * * * * * 씬 이동 * * * * * * * */
    public int currentMap;

    /* * * * * * * * 플레이어 기본 체력 및 스테미나 * * * * * * * */
    float playerHP = 20000f;
    float maxHP = 20000f;

    public float PlayerHP
    {
        get => playerHP;
        set
        {
            playerHP = value;
            if(playerHP <= 0)
            {
                playerHP = 0;
                Dead();
            }
            else if(playerHP >= maxHP)
            {
                playerHP = maxHP;
            }
            playerHP = Mathf.Min(playerHP, maxHP);
        }
    }

    public float MaxHP
    {
        get => maxHP;
    }

    float playerSM = 100f;
    float maxSM = 100f;
    public float PlayerSM
    {
        get => playerSM;
        set
        {
            playerSM = value;
            if (playerSM <= 0f)
            {
                playerSM = 0f;
            }
            else if (playerSM >= maxSM)
            {
                playerSM = maxSM;
            }
            playerSM = Mathf.Min(playerSM, maxSM);
        }
    }

    public float MaxSM
    {
        get => maxSM;
    }

    
    /* * * * * * * * 화폐 변수, 프로퍼티, 델리게이트 * * * * * * * */
    int money = 0;
    public int Money
    {
        get => money;
        private set
        {
            if(money != value)
            {
                OnMoneyChange?.Invoke(money);
                money = value;
            }
        }
    }
    public System.Action<int> OnMoneyChange;


    /* * * * * * * * 인벤토리 생성 및 초기화 * * * * * * * */
    Inventory inventory;

    static Player instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            Init();
        }
        else if (instance != this)
        {
            Destroy(instance.gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    private void Init()
    {
        playerInput = new PlayerInput();
        playerAnim = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody2D>();
        attackCollison = transform.GetChild(0).gameObject;
        boxCollider = GetComponent<BoxCollider2D>();
        inventory = FindObjectOfType<Inventory>();
    }

    

    void Start()
    {
        
    }

    private void OnEnable()
    {
        playerInput.Player.Enable();
        playerInput.Player.Move.performed += InputDir;
        playerInput.Player.Move.canceled += InputDir;
        playerInput.Player.Jump.performed += PlayerJump;
        playerInput.Player.NormalAttack.performed += NormalAttack;
        playerInput.Player.Talk.performed += SearchNpc;
        playerInput.Player.Dash.performed += Dash;
    }

    private void OnDisable()
    {
        playerInput.Player.Dash.performed -= Dash;
        playerInput.Player.Talk.performed -= SearchNpc;
        playerInput.Player.NormalAttack.performed -= NormalAttack;
        playerInput.Player.Jump.performed -= PlayerJump;
        playerInput.Player.Move.canceled -= InputDir;
        playerInput.Player.Move.performed -= InputDir;
        playerInput.Player.Disable();
    }
    
    void Update()
    {

    }


    /* * * * * * * * 결정된 방향으로 이동 * * * * * * * */
    private void FixedUpdate()
    {
        transform.position = transform.position + (Vector3)inputdir * playerSpeed * Time.fixedDeltaTime;
    }


    /* * * * * * * * 양옆 이동 방향 결정 * * * * * * * */
    PlayerInput playerInput;

    Vector2 inputdir;
    public Vector2 Inputdir => inputdir;

    bool ismove = false;
    public bool Ismove => ismove;

    float playerSpeed = 5f;

    private void InputDir(InputAction.CallbackContext obj)
    {

        //왼쪽 이동 -1, 오른쪽 이동 1, 멈춤 0
        inputdir = obj.ReadValue<Vector2>();
        playerAnim.SetInteger("run", (int)inputdir.x);

        if (inputdir.x < 0f)
        {
            ismove = false;
            transform.localScale = new Vector3(-5, 5, 1f);
        }
        else if(inputdir.x > 0f)
        {
            ismove = true;
            transform.localScale = new Vector3(5, 5, 1f);
        }
    }

    /* * * * * * * * 점프 * * * * * * * */
    float playerJumpPower = 7f;
    int jumplimited = 0;

    private void PlayerJump(InputAction.CallbackContext obj)
    {
        jumplimited++;

        //둘 다 1로 하면 1단 점프, 2로하면 2단 점프
        if (jumplimited <= 1)
        {
            rigid.AddForce(transform.up * playerJumpPower, ForceMode2D.Impulse);
            playerAnim.SetBool("isJump", true);
        }
        
    }


    /* * * * * * * * 점프 횟수 및 닿으면 죽는 부분 * * * * * * * */
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            playerAnim.SetBool("isJump", false);
            if(jumplimited >= 1)
            {
                jumplimited = 0;
            }
        }

        if(collision.gameObject.CompareTag("Deathground"))
        {
            Dead();
        }
    }

    /* * * * * * * * 아이템 획득 * * * * * * * */
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Item"))
        {
            Item item = collision.GetComponent<ItemData>().item;
            inventory.AddItem(item, collision);
        }
    }


    /* * * * * * * * 일반 공격 * * * * * * * */
    float attackdelay = 0f;

    float strength = 3;
    public float Strength
    {
        get => strength;
        set
        {
            strength = value;
        }
    }

    float defense = 0;
    public float Defense => defense;

    

    private void NormalAttack(InputAction.CallbackContext _)
    {
        if(attackdelay <= 0f)
        {
            attackCollison.SetActive(true);
            attackdelay = 1f;
            playerAnim.SetTrigger("attack");
            playerInput.Player.Disable();
            StartCoroutine(Attack1());
        }
    }

    IEnumerator Attack1()
    {
        yield return new WaitForSeconds(0.5f);
        attackCollison.SetActive(false);
        attackdelay = 0f;
        playerInput.Player.Enable();
    }

    /* * * * * * * * 원거리 공격 스킬(미구현) * * * * * * * */


    /* * * * * * * * 대쉬 스킬 관련 * * * * * * * */
    float dashPower = 5f;
    public bool isDash = false;

    private void Dash(InputAction.CallbackContext obj)
    {
        if (isDash && PlayerSM >= 10f)
        {
            rigid.AddForce(transform.right * inputdir * dashPower, ForceMode2D.Impulse);
            playerAnim.SetTrigger("dash");
            PlayerSM -= 10f;
        }
    }

    /* * * * * * * * 피격 * * * * * * * */
    float knockBack = 0f;

    public void TakeDamage(int damage, Vector3 back)
    {
        playerAnim.SetTrigger("hurt");

        Vector2 dir = back - transform.position;


        if (dir.x < 0)
        {
            knockBack = 1f;
        }
        else if (dir.x > 0)
        {
            knockBack = -1f;
        }

        PlayerHP -= (damage - Defense);

        rigid.AddForce(transform.right * knockBack, ForceMode2D.Impulse);

        OnDamaged();
    }

    //1초 무적 및 물리 미적용 위해 레이어 변경
    void OnDamaged()
    {
        gameObject.layer = 11;
        Invoke("OffDamaged", 1);
    }

    void OffDamaged()
    {
        gameObject.layer = 10;
    }


    /* * * * * * * * 대화 매서드 * * * * * * * */
    bool isAction = false;

    private void SearchNpc(InputAction.CallbackContext _)
    {
        Collider2D col = Physics2D.OverlapCircle(transform.position, 3f, LayerMask.GetMask("Npc"));
        GameObject go = col.gameObject;
        ObjectData objectData = go.GetComponent<ObjectData>();
        GameManager.Instance.TalkPanel.SetActive(isAction);

        if (objectData.isNpc)
        {
            SearchData(objectData.id);
        }
        else
        {
            Debug.Log("대화할 대상이 없습니다....");
        }
    }

    //대화(넘겨받은 id로 대화 정보 찾기
    private void SearchData(int index)
    {
        GameManager.Instance.TalkPanel.SetActive(true);

        int questTalkIndex = QuestManager.Instance.GetQuestTalkIndex(index);
        string talkData = GameManager.Instance.talkManager.GetTalk(index + questTalkIndex, QuestManager.Instance.TalkIndex);
        
        GameManager.Instance.TalkText.text = talkData;

        if (talkData == null)
        {
            GameManager.Instance.TalkPanel.SetActive(false);
            QuestManager.Instance.TalkIndex = 0;
            Debug.Log(QuestManager.Instance.CheckQuest(index));
            return;
        }
        else
        {
            Debug.Log("대화할 대상이 없습니다....");
        }

        QuestManager.Instance.TalkIndex++;
    }

    /* * * * * * * * 다이 * * * * * * * */
    void Dead()
    {
        playerAnim.SetTrigger("die");
        boxCollider.enabled = false;
        rigid.bodyType = RigidbodyType2D.Kinematic;
        Destroy(this.gameObject.gameObject);
        SceneLoad.LoadScene(0);
    }
}
