using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using StarterAssets;

public class ThirdPersonShooterController : MonoBehaviour
{
    [SerializeField]
    private CinemachineVirtualCamera _cinemachineVirtualCamera;

    private StarterAssetsInputs _starterAssetsInputs;

    [SerializeField]
    private ThirdPersonController _thirdPersonController;

    [SerializeField]
    private float _aimSensitivity;

    public Transform Sphere;

    [SerializeField]
    private LayerMask _aimLayers;

    private Vector3 mousePosition; 

    private Vector3 worldPosition;

    [SerializeField]
    private Animator _animator;

    private void Start()
    {
        _starterAssetsInputs = GetComponent<StarterAssetsInputs>();
    }

    private void Update()
    {
        Vector2 screenPoint = new Vector2(Screen.width/2, Screen.height/2);
        Ray aimRay = Camera.main.ScreenPointToRay(screenPoint);
        if(Physics.Raycast(aimRay,out RaycastHit hit,999f, _aimLayers))
        {
            Sphere.position = hit.point;
            mousePosition = hit.point;
        }


        if(_starterAssetsInputs.aim)
        {
            _cinemachineVirtualCamera.gameObject.SetActive(true);
            _thirdPersonController.SetSensitivity(_aimSensitivity);

            worldPosition = mousePosition;
            worldPosition.y = transform.position.y;
            Vector3 aimDirection = (worldPosition - transform.position).normalized;
            transform.forward = Vector3.Lerp(transform.forward, aimDirection, Time.deltaTime * 20f);

            _thirdPersonController.SetRotating(false);

            _animator.SetLayerWeight(1, Mathf.Lerp(_animator.GetLayerWeight(1), 1, Time.deltaTime * 10f));

        }
        else
        {
            _cinemachineVirtualCamera.gameObject.SetActive(false);
            _thirdPersonController.BackToNormalSensitivity();
            _thirdPersonController.SetRotating(true);

            _animator.SetLayerWeight(1, Mathf.Lerp(_animator.GetLayerWeight(1), 0, Time.deltaTime * 10f));

        }



    }

}
