﻿using UnityEngine;
using System.Collections;

public class EnemyAttrStrategy : IAttrStrategy
{
    public override int getAtkPlusValue(ICharacterAttr characterAttr)
    {
        return 0;
    }

    public override int getDmgDescValue(ICharacterAttr characterAttr)
    {
        return 0;
    }

    public override void initAttr(ICharacterAttr characterAttr)
    {
        
    }
}
