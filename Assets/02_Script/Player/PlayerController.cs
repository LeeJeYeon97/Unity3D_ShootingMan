using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    #region Private Component

    private PlayerInput _playerInput;
    private Rigidbody _rb;
    private Animator _anim;

    #endregion

    #region public Component  

    #endregion

    #region Private Field
    private InputAction moveAction;

    private Vector3 _moveDir;
    private float _moveSpeed;

    private float _maxHp;
    private float _curHp;

    #endregion

    #region Public Field

    public GameObject cameraRoot;


    private bool _isAttack;
    public bool IsAttack
    {
        get { return _isAttack; }
        set
        {
            if(value)
            {
                _isReload = false;
            }
            _anim.SetBool("Attack", value);
            _isAttack = value;
        }
    }
    private bool _isRoll;
    public bool IsRoll
    {
        get { return _isRoll; }
        set
        {
            if(value)
            {
                IsAttack = false;
            }
            IsRoll = value;
        }
    }
    private bool _isReload;
    public bool IsReload
    {
        get { return _isReload; }
        set
        {
            if(value)
            {
                IsAttack = false;
                _anim.SetTrigger("Reload");
            }
            _isReload = value;
        }
    }

    public float CurHp
    {
        get { return _curHp; }
        set { _curHp = value; }
    }
    public float MaxHp
    {
        get { return _maxHp; }
        set { _maxHp = value; }
    }
    #endregion


    private void Awake()
    {
        _playerInput = GetComponent<PlayerInput>();
        moveAction = _playerInput.actions["Move"];

        _rb = GetComponent<Rigidbody>();
        _anim = GetComponent<Animator>();

        _moveSpeed = 5.0f;
        MaxHp = 100;
        CurHp = MaxHp;

        
        GameObject hud = Resources.Load<GameObject>("UI/Scene/UI_Hud");
        Instantiate(hud, transform);
        
        // ī�޶� ����
        cameraRoot = new GameObject("CameraRoot");

        CinemachineVirtualCamera curCam = GameObject.Find("Virtual Camera").GetComponent<CinemachineVirtualCamera>();
        curCam.LookAt = cameraRoot.transform;
        curCam.Follow = cameraRoot.transform;
        
    }
    private void OnEnable()
    {
        moveAction.performed += ctx =>
        {
            Vector2 dir = ctx.ReadValue<Vector2>();
            _moveDir = new Vector3(dir.x, 0, dir.y);

            _anim.SetFloat("Move", _moveDir.magnitude);
            _anim.SetFloat("X", _moveDir.x);
            _anim.SetFloat("Y", _moveDir.z);
        };

        moveAction.canceled += ctx =>
        {
            _moveDir = Vector3.zero;

            _anim.SetFloat("Move", _moveDir.magnitude);
            _anim.SetFloat("X", _moveDir.x);
            _anim.SetFloat("Y", _moveDir.z);
        };
    }
    private void FixedUpdate()
    {
        // ī�޶� ��Ʈ ����ٴϰ� ����
        cameraRoot.transform.position = transform.position;

        Move();
        Roll();
    }

    // ������
    public void OnClickRollButton()
    {
        _anim.SetTrigger("Roll");
    }
    public void CheckRoll()
    {
        IsRoll = IsRoll ? false : true;
    }
    private void Roll()
    {
        if (!IsRoll) return;

        _rb.MovePosition(_rb.position +
                (transform.forward * Time.deltaTime * 15.0f));
    }
    
    private void Move()
    {
        if (!transform.CompareTag("Player")) return;
        if (IsRoll) return;

        if (_moveDir != Vector3.zero)
        {
            if (!IsAttack)
            {
                // ���̽�ƽ ���⿡ ���� ĳ���� ȸ��
                // ���� ������ ���� ���� �����ӿ� ���� ȸ��
                transform.rotation = Quaternion.Lerp(transform.rotation,
                    Quaternion.LookRotation(_moveDir), Time.deltaTime * 10);
                // �� �������� ĳ���� ������
                _rb.MovePosition(_rb.position +
                    (transform.forward * Time.deltaTime * _moveSpeed));
            }
            else
            {
                _rb.MovePosition(_rb.position +
                    (_moveDir.normalized * Time.deltaTime * _moveSpeed));
            }
            
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Bullet"))
        {
            CurHp--;
        }
    }

}
