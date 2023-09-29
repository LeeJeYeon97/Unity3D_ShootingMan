using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class WeaponController : MonoBehaviour
{

    #region Private Component

    private Animator _anim;

    // Player�� ���� üũ�� ���� PlayerController ������
    private PlayerController _playerController;

    // PlayerInput������Ʈ �� ����
    private PlayerInput _playerInput;
    // AttackAction�� ����
    private InputAction attackAction;

    #endregion

    #region Public Component
    #endregion

    #region Private Field

    private WaitForSeconds reloadWait;

    // �Ѿ� ���� ����
    private Vector3 _attackDir;

    // �Ѿ� ���� ������
    private float _attackDelay;
    // ������ ������
    private float _reloadDelay;

    // �ִ�, ���� ź�� ��
    private int maxAmmo;
    private int curAmmo;

    private string bulletName;
    
    #endregion

    #region Public Field

    // �ѿ� �޷��ִ� �ѱ� ��ġ
    public Transform firePos;

    public GameObject bullet;

    public int MaxAmmo { get { return maxAmmo; } }
    public int CurAmmo { get { return curAmmo; } }

    #endregion

    void Awake()
    {
        _anim = GetComponent<Animator>();
        _playerController = GetComponent<PlayerController>();
        _playerInput = GetComponent<PlayerInput>();

        // Attack Action�� ��������
        attackAction = _playerInput.actions["Attack"];

        maxAmmo = 30;
        curAmmo = maxAmmo;

        _attackDelay = 0.0f;
        _reloadDelay = 1.5f;

        reloadWait = new WaitForSeconds(_reloadDelay);

        bulletName = System.Enum.GetName(typeof(Define.PoolList), 
            Define.PoolList.Bullet);
    }
    private void OnEnable()
    {
        attackAction.started += ctx =>
        {
            if (_playerController.IsReload) return;

            _playerController.IsAttack = true;
        };
        // Attack �̺�Ʈ ����
        attackAction.performed += ctx =>
        {
            Vector2 dir = ctx.ReadValue<Vector2>();
            _attackDir = new Vector3(dir.x, 0, dir.y);
        };
        attackAction.canceled += ctx =>
        {
            _playerController.IsAttack = false;
        };
    }
    private void Update()
    {
        if(_playerController.IsAttack)
        {
            Fire();
        }
    }
    private void Fire()
    {
        // ���� ���� ���� �� ���⿡ ���� ȸ��
        transform.rotation = Quaternion.Lerp(transform.rotation,
                    Quaternion.LookRotation(_attackDir), Time.deltaTime * 10);

        _attackDelay -= Time.deltaTime;

        if (_attackDelay <= 0)
        {
            if (curAmmo <= 0)
            {
                StartCoroutine(CoReload());
                return;
            }
            // �Ѿ� ����
            GameObject bullet = Managers.Pool.GetPool(bulletName);
            bullet.GetComponent<BulletController>().Init(firePos, _attackDir);
            curAmmo--;
            _attackDelay = 0.1f;
        }
    }

    IEnumerator CoReload()
    {
        _playerController.IsReload = true;
        
        yield return reloadWait;

        curAmmo = maxAmmo;

        _playerController.IsReload = false;

    }
    // ������ UI ��ư Ŭ�� �� ȣ�� �Լ�
    public void OnClickReloadButton()
    {
        if (curAmmo >= maxAmmo) return;

        StartCoroutine(CoReload());
    }
}
