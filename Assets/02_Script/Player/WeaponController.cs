using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class WeaponController : MonoBehaviour
{

    #region Private Component

    private Animator _anim;

    // Player의 상태 체크를 위해 PlayerController 가져옴
    private PlayerController _playerController;

    // PlayerInput컴포넌트 값 저장
    private PlayerInput _playerInput;
    // AttackAction값 저장
    private InputAction attackAction;

    #endregion

    #region Public Component
    #endregion

    #region Private Field

    private WaitForSeconds reloadWait;

    // 총알 생성 방향
    private Vector3 _attackDir;

    // 총알 생성 딜레이
    private float _attackDelay;
    // 재장전 딜레이
    private float _reloadDelay;

    // 최대, 현재 탄알 수
    private int maxAmmo;
    private int curAmmo;

    private string bulletName;
    
    #endregion

    #region Public Field

    // 총에 달려있는 총구 위치
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

        // Attack Action값 가져오기
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
        // Attack 이벤트 연결
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
        // 공격 중일 때는 총 방향에 의한 회전
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
            // 총알 생성
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
    // 재장전 UI 버튼 클릭 시 호출 함수
    public void OnClickReloadButton()
    {
        if (curAmmo >= maxAmmo) return;

        StartCoroutine(CoReload());
    }
}
