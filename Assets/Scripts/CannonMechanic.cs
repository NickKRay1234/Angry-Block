using UnityEngine;
using System.Collections;
using System;

public class CannonMechanic : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _ballPrefab;
    [SerializeField] private Transform _cannonTarget;
    [SerializeField] private GameObject _ballsContainer;

    private bool _isShooting = false;
    private bool _isAiming;

    private void Aim()
    {
        if (_isShooting) return;
        if (Input.GetMouseButton(0)) // While button is pressed
        {
            if (!_isAiming) _isAiming = true;
            else
            {
                //PathCalculation();
                Rotate();
            }
            
            
        }
        else if (_isAiming && !_isShooting)
        {
            _isAiming = false;
            StartCoroutine(Shoot()); // Code review
            //HideTheDots();
        }
            
    }

    private void Update()
    {
        Aim();
    }

    private void Rotate()
    {
        var dir = _cannonTarget.position - transform.position;
        var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    IEnumerator Shoot()
    {
        for (int i = 0; i < 5; i++)
        {
            yield return new WaitForSeconds(0.07f);
            Rigidbody2D ball = Instantiate(_ballPrefab, transform.position, Quaternion.identity);
            ball.transform.SetParent(_ballsContainer.transform);
            //ball.AddForce(ShootForce(Input.mousePosition));
        }
    }

}
