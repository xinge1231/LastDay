using UnityEngine;
using System.Collections;

public class SoldierAttr : ICharacterAttr {
    private int m_lv = 0;

    public int Lv
    {
        get
        {
            return m_lv;
        }

        set
        {
            m_lv = value;
        }
    }
}
