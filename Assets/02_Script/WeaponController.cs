using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class WeaponController : MonoBehaviour
{

    private Vector3 _attackDir;

    private PlayerController _playerController;
    private PlayerInput _playerInput;
    private InputAction attackAction;

    public Transform firePos;
    public GameObject bullet;

    private float _attackDelay;
    private int maxAmmo;
    private int curAmmo;

    private bool isReload;
    void Awake()
    {
        _playerController = GetComponent<PlayerController>();
        _playerInput = GetComponent<PlayerInput>();
        attackAction = _playerInput.actions["Attack"];

        maxAmmo = 30;
        curAmmo = maxAmmo;

        _attackDelay = 0.0f;

        // �ڽ� ������Ʈ���� FirePos ã��
        //foreach (Transform child in this.transform)
        //{
        //    if (child.name == "FirePos")
        //        firePos = child;
        //}
    }
    private void OnEnable()
    {
        attackAction.performed += ctx =>
        {
            Vector2 dir = ctx.ReadValue<Vector2>();
            _attackDir = new Vector3(dir.x, 0, dir.y);

            _playerController._isAttack = true;
        };
        attackAction.canceled += ctx =>
        {
            _playerController._isAttack = false;
        };
    }
    private void Update()
    {
        if (!_playerController._isAttack || _playerController._isRoll) return;
        Fire();
    }
    private void Fire()
    {
        _attackDelay -= Time.deltaTime;

        // ���� ���� ���� �� ���⿡ ���� ȸ��
        transform.rotation = Quaternion.Lerp(transform.rotation,
                    Quaternion.LookRotation(_attackDir), Time.deltaTime * 10);

        if (_attackDelay <= 0)
        {
            if(curAmmo <= 0)
            {
                if (isReload) return;

                Reload();
                return;
            }

            // �Ѿ� ����
            Instantiate(bullet, firePos.position, Quaternion.LookRotation(_attackDir));
            //_anim.SetTrigger("Fire");
            _attackDelay = 0.1f;
        }
    }
    private void Reload()
    {
        isReload = true;

        // TODO �ִϸ��̼� Ʈ���� 
        curAmmo = maxAmmo;
        _attackDelay = 0.1f;

        isReload = false;

    }
}
