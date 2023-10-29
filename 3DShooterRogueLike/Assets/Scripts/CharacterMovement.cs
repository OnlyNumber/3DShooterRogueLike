using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    [Header("Player")]

    [SerializeField]
    private CharacterController _characterController;

    [SerializeField]
    private float normalSpeed;

    [SerializeField]
    private float sprintSpeed;

    private Vector3 _direction;

    [Header("Camera rotation")]

    [SerializeField]
    private GameObject _cameraTarget;

    [SerializeField]
    private float _sensitivity;

    [Tooltip("How far in degrees can you move the camera up"), SerializeField]
    private float _topClamp = 70.0f;

    [Tooltip("How far in degrees can you move the camera down"), SerializeField]
    private float _bottomClamp = -30.0f;

    private float _cameraX;

    private float _cameraY;

    [Space(10)]
    [Header("Gravitation")]

    [SerializeField]
    private float _jumpHight;

    [SerializeField]
    private float _gravity;

    private float velocityY;


    [Header("Ground")]
    private bool _isGrounded;

    [SerializeField]
    private float _checkGroundOffset;
    [SerializeField]
    private float _checkGroundRadius;

    [SerializeField]
    private LayerMask _groundLayers;

    [SerializeField]
    private Animator _animator;

    private void Update()
    {

        Move();
        CheckGround();
        Gravitation();
        RotateCamera();
    }

    private void RotateCamera()
    {
        if(Mathf.Abs(Input.GetAxis("Mouse X")) > 0.01f || Mathf.Abs(Input.GetAxis("Mouse Y")) > 0.01f)
        {
            _cameraX += Input.GetAxis("Mouse X") * 1 * _sensitivity;
            _cameraY -= Input.GetAxis("Mouse Y") * 1 * _sensitivity;
        }

        _cameraX = ClampAngle(_cameraX, float.MinValue, float.MaxValue);
        _cameraY = ClampAngle(_cameraY, _bottomClamp, _topClamp);

        // Cinemachine will follow this target
        _cameraTarget.transform.rotation = Quaternion.Euler(_cameraY,
            _cameraX, 0.0f);
    }

    private static float ClampAngle(float lfAngle, float lfMin, float lfMax)
    {
        if (lfAngle < -360f) lfAngle += 360f;
        if (lfAngle > 360f) lfAngle -= 360f;
        return Mathf.Clamp(lfAngle, lfMin, lfMax);
    }

    private void Move()
    {
        float targetSpeed = normalSpeed;

        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");

        Vector3 right = _cameraTarget.transform.right;

        Vector3 forward = _cameraTarget.transform.forward;

        right.y = 0;

        forward.y = 0;

        _direction = (right * horizontalInput + forward * verticalInput).normalized;

        _characterController.Move(_direction.normalized * Time.deltaTime * targetSpeed + new Vector3(0, velocityY, 0) * Time.deltaTime);

        if(_direction != Vector3.zero)
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(_direction), Time.deltaTime * 10f);

    }

    private void Gravitation()
    {
        if (_isGrounded)
        {
            if (velocityY < 0)
            {
                velocityY = -2f;
            }

            if(Input.GetKeyDown(KeyCode.Space))
            {
                velocityY = Mathf.Sqrt(_jumpHight  * - 2 * _gravity);
            }

        }
        else
        {

        }



        velocityY += Time.deltaTime * _gravity;

        velocityY = Mathf.Clamp(velocityY, -10, 10);

    }

    private void CheckGround()
    {
        Vector3 checkSphere = new Vector3(transform.position.x, transform.position.y + _checkGroundOffset, transform.position.z);

        _isGrounded = Physics.CheckSphere(checkSphere, _checkGroundRadius, _groundLayers, QueryTriggerInteraction.Ignore);



    }



}
