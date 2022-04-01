using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CannonMechanic : MonoBehaviour
{
    [SerializeField] private float _power = 2f;
    [SerializeField] private int _amountOfDots = 15;

    private Vector2 _startPos;
    private bool _isShooting;
    private bool _isAiming;

    private GameObject Dots;
    private List<GameObject> _projectilesPath;

    private Rigidbody2D _ballBody;
    public GameObject ballPrefab;
    public GameObject ballsContainer;
    
    private void Start()
    {
        Dots = GameObject.Find("Dots");
        _projectilesPath = Dots.transform.Cast<Transform>().ToList().ConvertAll(t => t.gameObject);
        for (int i = 0; i < _projectilesPath.Count; i++)
        {
            _projectilesPath[i].GetComponent<Renderer>().enabled = false;
        }
    }

    private void Aim()
    {
        if (_isShooting) return;
        if (Input.GetMouseButtonDown(0))
        {
            if (!_isAiming)
            {
                _isAiming = true;
                _startPos = Input.mousePosition;
            }
            else
            {
                //Aim Cal path
                PathCalculation();
            }
        }
        else if(_isAiming && !_isShooting)
        {
            _isAiming = false;
            print("Shoot");
            //Shoot
        }
    }

    private Vector2 ShootForce(Vector3 force)
    {
        return (new Vector2(_startPos.x, _startPos.y) - new Vector2(force.x, force.y)) * _power;
    }

    private Vector2 DotPath(Vector2 startPosition, Vector2 startVelocity, float t)
    {
        return startPosition + startVelocity * t + 0.5f * Physics2D.gravity * t * t;
    }

    private void PathCalculation()
    {
        Vector2 vel = ShootForce(Input.mousePosition) * Time.fixedDeltaTime / _ballBody.mass;

        for (int i = 0; i < _projectilesPath.Count; i++)
        {
            _projectilesPath[i].GetComponent<Renderer>().enabled = true;
            float t = i / 15f;
            Vector3 point = DotPath(transform.position, vel, t);
            point.z = 1;
            _projectilesPath[i].transform.position = point;
        }
        
    }

    
    private void Update()
    {
        _ballBody = ballPrefab.GetComponent<Rigidbody2D>();
        Aim();
        /*if (GC.shotCount <= 3 && !IsMouseOverUI())
        {
            Aim();
            Rotate();
        }*/
    }
}
