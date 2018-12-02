/*
 * 角色类接口
 */
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class ICharacter{
    protected GameObject m_gameObject = null; //游戏对象
    protected UnityEngine.AI.NavMeshAgent m_navAgent = null; //导航组件
    protected AudioSource m_audio = null; //音频组件
    protected Animator m_animator = null; //动画组件
    protected Collider m_collider = null; //碰撞体组件
    protected Rigidbody m_rigidbody = null; //刚体组件

    protected string m_assetName = ""; //模型名称
    public string AssetName
    {
        get{
            return m_assetName;
        }
    }
    protected bool m_bIsKilled = false;//是否已经死亡
    public bool IsKilled
    {
        get { return m_bIsKilled; }
        set { m_bIsKilled = value; }
    }
    protected bool m_bCanRemove = false; //对象是否可移除
    public bool CanRemove
    {
        get { return m_bCanRemove; }
        set { m_bCanRemove = value; }
    }
    protected float m_removeTimer = 1.5f; //对象移除延迟时间
    public float RemoveTimer
    {
        get { return m_removeTimer; }
        set { m_removeTimer = value; }
    }
    protected Vector3 m_spawnPosition = Vector3.zero; //重生地点
    public UnityEngine.Vector3 SpawnPosition
    {
        get { return m_spawnPosition; }
        set { m_spawnPosition = value; }
    }
    protected ICharacterAttr m_characterAttr = null;//角色属性
    public ICharacterAttr CharacterAttr
    {
        get { return m_characterAttr; }
        set { m_characterAttr = value; }
    }
    protected IWeapon m_weapon = null; //武器
    public IWeapon Weapon
    {
        get { return m_weapon; }
        set { m_weapon = value; }
    }
    protected ICharacterAI m_AI = null; //AI
    public ICharacterAI AI
    {
        get { return m_AI; }
        set { m_AI = value; }
    }
    protected ICharacterSteerings m_steerings = null; //操控行为
    public ICharacterSteerings Steerings
    {
        get { return m_steerings; }
        set { m_steerings = value; }
    }
    protected ISensor m_sensors = null; //感知器
    public ISensor Sensors
    {
        get { return m_sensors; }
        set { m_sensors = value; }
    }
    protected GameObject m_target = null; //目标
    public UnityEngine.GameObject Target
    {
        get { return m_target; }
        set {
            if (value.layer == LayerMask.NameToLayer("Character"))
                m_targetCharacter = GameManager.Instance.findCharacterByGameObject(value);
            m_target = value;
        }
    }
    protected ICharacter m_targetCharacter = null; //目标角色
    public ICharacter TargetCharacter
    {
        get { return m_targetCharacter; }
        set {
            Target = TargetCharacter.getGameObject();
            m_targetCharacter = value;
        }
    }
    protected Vector3 m_targetPos = Vector3.zero; //目标位置
    public UnityEngine.Vector3 TargetPos
    {
        get {
            if (Target != null)//如果有目标对象，取目标对象位置为目标位置
                return Target.transform.position;
            return m_targetPos;
        }
        set {
            Target = null;//说明目标是一个地址，取消当前目标
            TargetCharacter = null;
            m_targetPos = value;
        }
    }
    public ICharacter() { }

    #region 模型相关
    //设置模型，获取相关组件
    public virtual void setGameObject(GameObject theGameObject) {
        m_gameObject = theGameObject;
        m_navAgent = m_gameObject.GetComponent<UnityEngine.AI.NavMeshAgent>();
        m_animator = m_gameObject.GetComponent<Animator>();
        m_audio = m_gameObject.GetComponent<AudioSource>();
        m_collider = m_gameObject.GetComponent<Collider>();
        m_rigidbody = m_gameObject.GetComponent<Rigidbody>();

    }
    //获取模型
    public GameObject getGameObject() {
        return m_gameObject;
    }
    //销毁模型
    public void release()
    {

        if (m_gameObject != null)
            GameObject.Destroy(m_gameObject);
    }

    //获取动画组件
    public Animator getAnimator() {
        return m_animator;
    }
    //获取碰撞体
    public Collider getCollider() {
        return m_collider;
    }

    //获取刚体组件
    public Rigidbody getRigidbody() {
        return m_rigidbody;
    }
    #endregion

    #region 获取
    //获取当前速率(标量)
    public virtual float getCurSpeed() {
        return Steerings.m_velocity.magnitude;
    }

    //获取当前生命值
    public int getCurHP() {
        return m_characterAttr.CurHP;
    }
    #endregion

    #region 更新
    //物理更新
    public virtual void fixedUpdate() {
        AI.fixedUpdate();
        Steerings.fixedUpdate();
    }
    //逻辑更新
    public virtual void update() {

        //死亡后达到指定时间，则该角色可以被移除
        if (IsKilled) {
            RemoveTimer -= Time.deltaTime;
            if (RemoveTimer <= 0)
                CanRemove = true;
        }

        //武器更新
        if(Weapon!=null)
            Weapon.update();
        //AI更新
        if(AI!=null)
            AI.update();
        //操控行为更新
        if(Steerings!=null)
            Steerings.update();
    }
    #endregion

    #region 行为
    //移动到目标位置
    public virtual void moveTo(Vector3 position) {
        /*使用自带导航
        if(m_navAgent!=null && m_navAgent.isActiveAndEnabled)
            m_navAgent.SetDestination(position);
         */
            
        m_animator.SetBool("isMove", true);
    }
    //移动一段位移
    public virtual void moveBy(Vector3 distance) {
        m_gameObject.transform.Translate(distance);
    }
    //追赶目标
    public virtual void chase(ICharacter target) {
        if(target!=null)
            moveTo(target.getPosition());
    }
    //停止移动
    public virtual void stopMove() {
        //m_navAgent.Stop();
        m_animator.SetBool("isMove", false);
    }
    //获取位置
    public Vector3 getPosition() {
        return m_gameObject.transform.position;
    }

    //攻击
    public virtual void attack()
    {
        Weapon.fire();
    }
    //被攻击
    public virtual void underAttack(ICharacter theAttacker)
    {
        CharacterAttr.calDmg(theAttacker);
        //if (m_characterAttr.getCurHP() <= 0)
            //showDeathEffect();
    }

    //死亡
    public virtual void dead() {
        m_animator.SetTrigger("isDead");
        m_navAgent.enabled = false;
        m_collider.enabled = false;
        m_rigidbody.useGravity = false;
        IsKilled = true;
       
    }

    #endregion

    #region 武器
    //设置武器
    public void setWeapon(IWeapon theWeapon) {
        Weapon = theWeapon;
    }
    //获取武器
    public IWeapon getWeapon() {
        return Weapon;
    }
    //设置武器攻击加成(通过属性等)
    protected void setWeaponAtkPlus(int value) {
        Weapon.setAtkPlusValue(value);
    }
    #endregion


    #region 角色特效
    // 播放音效
    public void playSound(string ClipName)
    {
        IAssetFactory factory = FactoryManager.getAssetFactory();
        AudioClip theClip = factory.loadAudioClip(ClipName);
        if (theClip == null)
            return;
        m_audio.clip = theClip;
        m_audio.Play();
    }

    // 播放特效
    public void playEffect(string EffectName)
    {
        //  取得特效
        IAssetFactory factory = FactoryManager.getAssetFactory();
        GameObject effectObj = factory.loadEffect(EffectName);
        if (effectObj == null)
            return;

        // 将特效附加到当前角色对象
        UnityTool.attach(m_gameObject, effectObj, Vector3.zero);
    }

    //public abstract void showDeathEffect();
    #endregion
}
