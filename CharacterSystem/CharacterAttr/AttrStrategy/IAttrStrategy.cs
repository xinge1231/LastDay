/*
 * 属性算法接口类
 */

using UnityEngine;
using System.Collections;

public abstract class IAttrStrategy{
    //初始化属性
    public abstract void initAttr(ICharacterAttr characterAttr);

    //计算攻击加成
    public abstract int getAtkPlusValue(ICharacterAttr characterAttr);

    //计算免伤
    public abstract int getDmgDescValue(ICharacterAttr characterAttr);


}
