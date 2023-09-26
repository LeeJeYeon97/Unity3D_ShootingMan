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

    #region Private 

    private InputAction moveAction;

    #endregion

    #region Private Field

    private Vector3 _moveDir;
    private float _moveSpeed;

    #endregion

    #region Public Field
    public GameObject root;
    public bool _isAttack;
    public bool _isRoll;

    #endregion

    private void Awake()
    {
        _playerInput = GetComponent<PlayerInput>();
        moveAction = _playerInput.actions["Move"];

        _rb = GetComponent<Rigidbody>();
        _anim = GetComponent<Animator>();

        _moveSpeed = 5.0f;
    }
    private void OnEnable()
    {
        moveAction.performed += ctx =>
        {
            Vector2 dir = ctx.ReadValue<Vector2>();
            _moveDir = new Vector3(dir.x, 0, dir.y);

            _anim.SetFloat("Move", _moveDir.magnitude);
        };
        moveAction.canceled += ctx =>
        {
            _moveDir = Vector3.zero;

            _anim.SetFloat("Move", _moveDir.magnitude);
        };
    }
    private void FixedUpdate()
    {
        // ī�޶� ��Ʈ ����ٴϰ� ����
        root.transform.position = transform.position;

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
        _isRoll = _isRoll ? false : true;
    }
    private void Roll()
    {
        if (!_isRoll) return;

        _rb.MovePosition(_rb.position +
                (transform.forward * Time.deltaTime * 15.0f));
    }
    
    private void Move()
    {
        if (_isRoll) return;

        if (_moveDir != Vector3.zero)
        {
            if (!_isAttack)
            {
                // ���̽�ƽ ���⿡ ���� ĳ���� ȸ��
                // ���� ������ ���� ���� �����ӿ� ���� ȸ��
                transform.rotation = Quaternion.Lerp(transform.rotation,
                    Quaternion.LookRotation(_moveDir), Time.deltaTime * 10);
            }
            
            // �� �������� ĳ���� ������
            _rb.MovePosition(_rb.position +
                (transform.forward * Time.deltaTime * _moveSpeed));
        }
    }

    // ������
    public void OnClickReloadButton()
    {

    }
}
