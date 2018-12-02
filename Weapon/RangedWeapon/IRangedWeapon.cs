using UnityEngine;
using System.Collections;


public abstract class IRangedWeapon : IWeapon {
    protected Ray ray;
    protected RaycastHit hitInfo;
    protected GameObject m_gunEnd = null;
 
    public IRangedWeapon() {
        
    }

    //设置特效组件
    public override void setupEffect()
    {
        m_gunEnd = UnityTool.findChildGameObject(m_gameObject, "GunBarrelEnd");
        GameObject shootEffect = FactoryManager.getAssetFactory().loadEffect("ShootEffect");
        UnityTool.attach(m_gunEnd, shootEffect, Vector3.zero);
        m_particles = shootEffect.GetComponent<ParticleSystem>();
        m_audio = m_gunEnd.GetComponent<AudioSource>();
        m_line = m_gunEnd.GetComponent<LineRenderer>();
        //m_light = m_gunEnd.GetComponent<Light>();
        m_light = UnityTool.findChildGameObject(m_gunEnd, "FireLight").GetComponent<Light>();

        m_line.SetColors(Color.red, Color.black);
        m_line.SetWidth(0.1f, 0.1f);
    }
    //停止播放特效
    public override void disableEffect() {
        if (m_line != null)
            m_line.enabled = false;
        if (m_light != null)
            m_light.enabled = false;
    }

    //播放攻击特效
    public override void showAtkEffect(Vector3 targetPosition)
    {
        m_effectDisplayTime = 0.1f;
        if (m_particles != null)
        {
            m_particles.Stop();
            m_particles.Play();
        }
        if (m_light != null)
        {
            m_light.enabled = true;
        }

        if (m_audio != null)
            m_audio.Play();

        if (m_line != null)
        {
            m_line.enabled = true;
            m_line.SetPosition(0, m_gunEnd.transform.position);
            m_line.SetPosition(1, targetPosition);
            
        }
        

    }
    //播放击中特效
    public override void showHitEffect(Vector3 targetPosition) {

    }

    public override void fire()
    {
        //ray =Camera.main.ScreenPointToRay(Input.mousePosition);
        ray.origin = m_gunEnd.transform.position;
        ray.direction = m_gunEnd.transform.forward;
        
        if (Physics.Raycast(ray, out hitInfo, m_weaponAttr.AtkRange))
        {
            showAtkEffect(hitInfo.point);
            showHitEffect(hitInfo.point);
            if (hitInfo.transform.gameObject.tag.Equals("Enemy"))
            {
                GameManager.Instance.findEnemyByGameObjectID(hitInfo.transform.gameObject.GetInstanceID()).underAttack(m_owner);
            }
        }
        else {
            showAtkEffect(ray.origin+ray.direction*m_weaponAttr.AtkRange);
        }
        
    }
}
