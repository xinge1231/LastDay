/*
 * 士兵属性算法类
 */

using UnityEngine;
using System.Collections;

public class SoldierAttrStrategy : IAttrStrategy
{
    //攻击增加算法
    public override int getAtkPlusValue(ICharacterAttr characterAttr)
    {
        return 0;

    }

    //免伤算法
    public override int getDmgDescValue(ICharacterAttr characterAttr)
    {
        return 0;
    }

    //属性初始化
    public override void initAttr(ICharacterAttr characterAttr)
    {
        SoldierAttr soldierAttr = characterAttr as SoldierAttr;
        if (soldierAttr == null) //需要判断参数是否是士兵属性
            return;

        //等级加HP上限
        if (soldierAttr.Lv > 0)
            soldierAttr.addMaxHp((soldierAttr.Lv-1) * 10);

    }
}
