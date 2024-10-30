using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [Header("Player")]
    public GameObject player;
    private Animator playerAnim;
    private bool isAttack = false;      // 공격 여부 
    private float attackTime = 0f;      // 공격이 진행되는 시간 
    [SerializeField] private float hitTime = 0.4f;        // 공격 타이밍 지정 

    [Header("Audio")]
    public AudioClip attackClip;
    public AudioClip hitClip;
    private AudioSource audioSource;

    [Header("Enemy")]
    public GameObject enemyObj; // 가져와야 하는 프리팹 
    public GameObject enemySpawn;
    public bool isSpawn = true;

    [Header("UI")]
    public Image imageEnemyHP;
    public TextMeshProUGUI textGold;
    public TextMeshProUGUI textPayGold;
    public TextMeshProUGUI textStageGold;
    public TextMeshProUGUI textEnemyCount;






    private Settings setting;




    // Start is called before the first frame update
    void Start()
    {
        playerAnim = player.GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        setting = GetComponent<Settings>();
    }


    void Update()
    {
        EnemySpawn();
        MouseOnClick();
    }

    private bool IsAttackChk()
    {
        if (isAttack)
        {
            attackTime += Time.deltaTime;

            if (attackTime > hitTime)
            {
                attackTime = 0;
                isAttack = false;
            }
        }

        return isAttack;
    }

    private void EnemyAttack(RaycastHit2D hit)
    {
        Enemy enemy = hit.collider.GetComponent<Enemy>();
        AudioSource enemyAudio = hit.collider.GetComponent<AudioSource>();

        if (enemy != null)
        {
            enemy.EnemyOnClick();
            enemyAudio.clip = hitClip;
            enemyAudio.volume = 0.3f;
            enemyAudio.Play();

            playerAnim.SetTrigger("Attack");
            audioSource.clip = attackClip;
            audioSource.Play();

            isAttack = true;

            // 적 개체가 죽었는지 체크 
            if (setting.IsEnemyDie())
            {
                enemy.EnemyDie();
                setting.GetEnemyHP();
                isSpawn = true;
            }
        }
    }

    private void MouseOnClick()
    {

        if (IsAttackChk())
        {
            return;
        }


        if (Input.GetMouseButtonDown(0))
        {
            // 카메라에서 월드로 광선을 쏴서 마우스 포지션을 받아옴 
            Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            // Ray를 쐈을때 정보를 저장 
            RaycastHit2D hit = Physics2D.Raycast(pos, Vector2.zero);

            if (hit.collider != null && hit.collider.tag == "Enemy")
            {
                EnemyAttack(hit);
            }

        }
    }

    private void EnemySpawn()
    {
        if (isSpawn)
        {
            StartCoroutine(EnemySpawnTime());
            isSpawn = false;
        }

    }

    IEnumerator EnemySpawnTime()
    {
        yield return new WaitForSeconds(1f);

        setting.initEnemyHP();
        Instantiate(enemyObj, enemySpawn.transform.position, Quaternion.identity);

    }


}
