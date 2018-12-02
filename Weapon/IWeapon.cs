using UnityEngine;
using System.Collections;



public abstract class IWeapon{
    //武器模型和拥有者对象
    protected GameObject m_gameObject = null;
    protected ICharacter m_owner = null;
    protected string m_assetName = "";

    //武器属性
    protected WeaponAttr m_weaponAttr = null;
    protected int m_atkPlusValue = 0;

    //武器特效
    protected float m_effectDisplayTime = 0.1f;
    protected ParticleSystem m_particles = null;
    protected LineRenderer m_line = null;
    protected AudioSource m_audio = null;
    protected Light m_light = null;

    #region 设置武器模型和拥有者
    //设置武器模型和特效组件
    public void setGameObject(GameObject theGameObject) {
        m_gameObject = theGameObject;
    }
    //获取模型
    public GameObject getGameObject()
    {
        return m_gameObject;
    }

    //销毁模型
    public void release()
    {
        if (m_gameObject != null)
            GameObject.Destroy(m_gameObject);
    }

    //设置武器拥有者
    public void setOwner(ICharacter theCharacter) {
        m_owner = theCharacter;
    }
    #endregion

    #region 获取
    public int getAtkRange() {
        return m_weaponAttr.AtkRange;
    }

    public int getAtkPower() {
        return m_weaponAttr.AtkPower;
    }
    #endregion

    #region 武器特效
    //设置特效组件
    public abstract void setupEffect();
    //停止播放特效
    public abstract void disableEffect();
    //播放攻击特效
    public abstract void showAtkEffect(Vector3 hitPosition);
    //播放击中特效
    public abstract void showHitEffect(Vector3 hitPosition);

    // 更新(用于特效播放时间计算)
    public void update()
    {
        
        if (m_effectDisplayTime > 0)
        {
            m_effectDisplayTime -= Time.deltaTime;
            if (m_effectDisplayTime <= 0)
            {
                disableEffect();
            }
        }
        
    }
    #endregion

    //获取资源名称
    public string getAssetName()
    {
        return m_assetName;
    }

    //设置武器属性
    public void setWeaponAttr(WeaponAttr weaponAttr) {
        m_weaponAttr = weaponAttr;
    }

    public WeaponAttr getWeaponAttr() {
        return m_weaponAttr;
    }

    //设置额外武器攻击力加成
    public void setAtkPlusValue(int value) {
        m_atkPlusValue = value;
    }

    //获取武器最终攻击力(经过加成)
    public int getFinalAtk() {
        return m_weaponAttr.AtkPower + m_atkPlusValue;
    }

    //武器开火
    public virtual void fire() {
    }
    
}
