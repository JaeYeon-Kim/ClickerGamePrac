using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.AI;

/*
게임 관련 셋팅용 스크립트 
*/
public class Settings : MonoBehaviour
{
    private BigInteger attackDMG = 1;        // 플레이어의 데미지 
    private BigInteger enemyHP = 3;          // 적의 체력 
    private BigInteger newEnemyHP;

    private BigInteger gold = 0;
    private BigInteger payGold = 1;
    private BigInteger dropGold = 1;

    public int stage = 1;
    public int enemyCount = 6;



    // Start is called before the first frame update
    void Start()
    {
        newEnemyHP = enemyHP;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public bool IsEnemyDie()
    {
        bool result = false;

        enemyHP -= attackDMG;

        if (enemyHP <= 0)
        {
            enemyHP = 0;
            result = true;
        }

        return result;

    }

    public void initEnemyHP()
    {
        BigInteger hp = (BigInteger)((float)enemyHP * 1.8f);
        enemyHP = hp;
        newEnemyHP = enemyHP;
    }

    public void GetEnemyHP()
    {
        enemyHP = newEnemyHP;
    }

    // enemy가 죽으면 소지 골드에 추가하는 스크립트 
    public BigInteger GetGold()
    {
        dropGold = BigInteger.Pow(2, stage) / 2;

        if (dropGold < 1)
        {
            dropGold = 1;
        }

        gold += dropGold;

        return gold;
    }

    // 레벨업시 수행하는 코드
    public void LvUpPayGold()
    {
        if (gold >= payGold)
        {
            gold -= payGold;
            attackDMG += 1;
            payGold += (BigInteger)((float)payGold * 1.2f);
        }
    }

    // Enemy Hp 상태 표시 
    public float GetEnemyHpVal()
    {
        float hp = (float)enemyHP / (float)newEnemyHP;

        return hp;
    }

    private string FormatNum(BigInteger num)
    {
        string[] units = { "", "K", "M", "Y", "T" };
        int unitIndex = 0;

        while (num > 1000 && unitIndex < units.Length - 1)
        {
            num /= 1000;
            unitIndex++;
        }

        string fNum = string.Format("{0}{1}", num.ToString(), units[unitIndex]);

        return fNum;
    }

    public string stringGold()
    {
        return FormatNum(gold);
    }

    public string stringPayGold()
    {
        return FormatNum(payGold);
    }
}
