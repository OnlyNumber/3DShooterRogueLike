using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ItemFinder : MonoBehaviour
{
    [SerializeField]
    private TMP_Text _helpButton;

    [SerializeField]
    private LayerMask _itemLayer;

    [SerializeField]
    private float _radiusDetect;

    private void Update()
    {
        Collider[] itemColliders;

        Vector3 upVect = transform.position;

        upVect.y += 2;

        itemColliders = Physics.OverlapCapsule(transform.position, upVect, _radiusDetect, _itemLayer);

        if(itemColliders.Length >= 1)
        {
            _helpButton.gameObject.SetActive(true);

            if (Input.GetKeyDown(KeyCode.E))
            {
                foreach (var item in itemColliders)
                {
                    item.gameObject.GetComponent<Item>().ItemActivation(gameObject);
                }
            }
        }
        else
        {
            _helpButton.gameObject.SetActive(false);

        }

    }


}
