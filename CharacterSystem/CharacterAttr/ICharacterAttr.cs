using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public abstract class ICharacterAttr{
    private string m_name = "";
    private int strenth = 0;
    private int agility = 0;
    private int stamina = 0;
    private int intelligence = 0;
    private int m_maxHP = 10000;
    private int m_curHP = 1000;
    private float m_maxSpeed = 10.0f;
    private float m_maxRotateSpeed = 1.5f;



    protected IAttrStrategy m_attrStrategy = null;


    public int Strenth
    {
        get
        {
            return strenth;
        }

        set
        {
            strenth = value;
        }
    }

    public int Agility
    {
        get
        {
            return agility;
        }

        set
        {
            agility = value;
        }
    }

    public int Stamina
    {
        get
        {
            return stamina;
        }

        set
        {
            stamina = value;
        }
    }

    public int Intelligence
    {
        get
        {
            return intelligence;
        }

        set
        {
            intelligence = value;
        }
    }


    public int MaxHP
    {
        get
        {
            return m_maxHP;
        }

        set
        {
            m_maxHP = value;
        }
    }

    public int CurHP
    {
        get
        {
            return m_curHP;
        }

        set
        {
            m_curHP = value;
        }
    }

    public string Name
    {
        get
        {
            return m_name;
        }

        set
        {
            m_name = value;
        }
    }

    public float MaxSpeed
    {
        get
        {
            return m_maxSpeed;
        }

        set
        {
            m_maxSpeed = value;
        }
    }

    public float MaxRotateSpeed
    {
        get
        {
            return m_maxRotateSpeed;
        }

        set
        {
            m_maxRotateSpeed = value;
        }
    }

    public ICharacterAttr() {

    }



    public void setAttrStragegy(IAttrStrategy attrStrategy) {
        m_attrStrategy = attrStrategy;
    }

    public IAttrStrategy getAttrstrategy() {
        return m_attrStrategy;
    }
    //初始化初始
    public void initAttr() {
        m_attrStrategy.initAttr(this);
    }
    //计算属性对攻击的加成
    public int getAtkPlusValue() {
        return m_attrStrategy.getAtkPlusValue(this);
    }

    //计算免伤
    public int getDmgDescValue() {
        return m_attrStrategy.getDmgDescValue(this);
    }

    //计算最终受到的伤害
    public void calDmg(ICharacter theAttacker) {
        CurHP -= theAttacker.getWeapon().getFinalAtk() - getDmgDescValue();
    }

    //增加最大HP
    public void addMaxHp(int addNum) {
        MaxHP += addNum;
    }

}
