using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HornChanger : MonoBehaviour
{
    public SpriteRenderer sprrend;
    public Sprite lvl1crack;
    public Sprite lvl2;
    public Sprite lv2crack;
    public Sprite lvl3;
    public Sprite lvl3crack;
    public BossManager boss;

    private void Update()
    {
        if (boss.BossHealth == 6)
        {
            sprrend.sprite = lvl1crack;
        }

        if (boss.BossHealth <= 6 && boss.bossvul == false)
        {
            sprrend.sprite = lvl2;
        }

        if (boss.BossHealth == 3)
        {
            sprrend.sprite = lv2crack;
        }

        if (boss.BossHealth <= 3 && boss.bossvul == false)
        {
            sprrend.sprite = lvl3;
        }


        if (boss.BossHealth == 0)
        {
            sprrend.sprite = lvl3crack;
        }
;
    }
}
