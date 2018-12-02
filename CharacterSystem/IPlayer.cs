using UnityEngine;
using System.Collections;

public enum ENUM_Player {
    Null = 0,
    PlayerAssault = 1,
}

public abstract class IPlayer : ICharacter {
    private Camera m_camera = null;
    protected ENUM_Player m_playerType = ENUM_Player.Null;

    private float m_moveSpeed = 5.0f;
    private float m_rotateSpeed = 5.0f;
    private float m_jumpVelocity = 1.0f;

    private bool isGrounded;        //玩家是否在地面上
    private float groundedRaycastDistance = 5.0f;   //表示向地面发射射线的射线长度
    private float m_mouseRotateX; //摄像机俯仰角
    private float m_minMouseRotateX = -60.0f;
    private float m_maxMouseRotateX = 75.0f;
    private float m_cameraHeight;//摄像机高度
    private float m_minCameraHeight = 0.0f;
    private float m_maxCameraHeight = 5.0f;


    public void addMoveSpeed(float speed) {
        m_moveSpeed += speed;
    }

    public void reduceMoveSpeed(float speed)
    {
        m_moveSpeed -= speed;
    }

    public override float getCurSpeed() {
        return m_moveSpeed;
    }
    public override void setGameObject(GameObject theGameObject)
    {
        base.setGameObject(theGameObject);
        m_camera = UnityTool.findChildGameObject(theGameObject, "Camera").GetComponent<Camera>();
    }

    public void inputProcess()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        float rv = Input.GetAxisRaw("Mouse X");
        float rh = Input.GetAxisRaw("Mouse Y");
        move(h, v);
        rotate(rh, rv);
        attack();

    }

    public override void fixedUpdate()
    {
        //m_steerings.m_velocity =m_rigidbody.velocity;
        //Debug.Log(m_steerings.m_velocity.magnitude);

        isGrounded = Physics.Raycast(m_gameObject.transform.position, -Vector3.up, groundedRaycastDistance);
        jump();
    }

    public override void update()
    {
        inputProcess();
        //武器更新
        if (Weapon != null)
            Weapon.update();
    }

    public override void attack()
    {

        Ray ray = new Ray();
        RaycastHit hitInfo;
        if (Input.GetButtonDown("Fire1"))
        {
            ray.origin = m_camera.transform.position;//设置射线发射的原点：摄像机所在的位置
            ray.direction = m_camera.transform.forward;	//设置射线发射方向：摄像机的正方向
            if (Physics.Raycast(ray, out hitInfo, Weapon.getWeaponAttr().AtkRange))
            {
                Debug.Log("RayHit");
                GameObject targetObj = hitInfo.transform.gameObject;
                Weapon.showAtkEffect(hitInfo.point);
                Weapon.showHitEffect(hitInfo.point);
                if (targetObj.tag.Equals("Enemy"))
                {
                    ICharacter targetCharacter = GameManager.Instance.findEnemyByGameObjectID(targetObj.GetInstanceID());
                    targetCharacter.underAttack(this);
                }
            }
            else {
                Vector3 targetPos = ray.origin + ray.direction * Weapon.getWeaponAttr().AtkRange;
                Weapon.showAtkEffect(targetPos);
                Weapon.showHitEffect(targetPos);
            }
        }
        
    }

    public void jump()
    {
        if (Input.GetKey(KeyCode.Space) && isGrounded)
        {
            m_gameObject.transform.GetComponent<Rigidbody>().AddForce(Vector3.up * m_jumpVelocity, ForceMode.VelocityChange);
        }
    }

    public void move(float h, float v)
    {
        m_gameObject.transform.Translate((Vector3.forward * v + Vector3.right * h) * m_moveSpeed * Time.deltaTime);

        /*
        if (v != 0.0f)
            m_animator.SetBool("isMove", true);
        else
            m_animator.SetBool("isMove", false);
        */
    }

    public void rotate(float rh, float rv)
    {
        //视角左右旋转
        m_gameObject.transform.Rotate(0, rv * m_rotateSpeed, 0);
        //视角上下旋转
        m_mouseRotateX -= rh * m_rotateSpeed;
        m_mouseRotateX = Mathf.Clamp(m_mouseRotateX, m_minMouseRotateX, m_maxMouseRotateX);
        m_camera.transform.localEulerAngles = new Vector3(m_mouseRotateX, 0.0f, 0.0f);
        //旋转上下视角的同时，调整摄像机位置
        m_cameraHeight = (m_mouseRotateX - m_minMouseRotateX) / (m_maxMouseRotateX - m_minMouseRotateX) * 5;
        m_camera.transform.position = new Vector3(m_camera.transform.position.x, m_cameraHeight + m_gameObject.transform.position.y, m_camera.transform.position.z);

    }

}
