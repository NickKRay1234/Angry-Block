using System;
using System.Collections.Generic;
// using System.Linq; теоретически мусор
using UnityEngine;

public class CannonMechanic : MonoBehaviour
{
    [SerializeField] private float _power = 2f;
    [SerializeField] private List<Renderer> _projectilesPath;

    [SerializeField] private Rigidbody2D _ballPrefab;
    //[SerializeField] private GameObject ballsContainer;
    //[SerializeField] private int _amountOfDots = 15; // теоретически мусор
    //[SerializeField] private GameObject Dots; // теоретически мусор

    private Vector2 _startPosition;
    private bool _isShooting = false;
    private bool _isAiming;

    //private Rigidbody2D _ballBody;

    /*private void Start()
    {
        //Dots = GameObject.Find("Dots"); // теоретически мусор
        //_projectilesPath = Dots.transform.Cast<Transform>().ToList().ConvertAll(t => t.gameObject); // теоретически мусор
        for (int i = 0; i < _projectilesPath.Count; i++)
        {
            _projectilesPath[i].GetComponent<Renderer>().enabled = false; // Выключение всех точек прицеливания
        }
    }*/


    private void Aim()
    {
        if (_isShooting) return;
        if (Input.GetMouseButton(0)) // While button is pressed
        {
            if (!_isAiming)
            {
                _isAiming = true;
                _startPosition = Input.mousePosition;
            }
            else
                PathCalculation();
        }
        else if (_isAiming && !_isShooting)
            _isAiming = false;
    }

    /// <summary>
    /// Return negative mouse position multiplied by power
    /// </summary>
    private Vector2 ShootForce(Vector2 force) // Сила выстрела
    {
        return new Vector2(_startPosition.x, _startPosition.y) - ((new Vector2(force.x, force.y)) * _power);
    }

    /// <summary>
    /// Projectile line of flight
    /// </summary>
    private Vector2 DotPath(Vector2 position, Vector2 velocity, float t)
    {
        return position + velocity * t + (Physics2D.gravity / 2) * (int)Math.Pow(t, 2);
    }

    private Vector2 VelocityCalculation() //  Formula: V = F/m
    {
        return ShootForce(Input.mousePosition) * Time.fixedDeltaTime / _ballPrefab.mass; 
    }

    private void PathCalculation()
    {
        for (int i = 0; i < _projectilesPath.Count; i++)
        {
            float t = i / 15f;
            _projectilesPath[i].enabled = true;
            _projectilesPath[i].transform.position = DotPath(transform.position, VelocityCalculation(), t);
        }
    }

    
    private void Update()
    {
        Aim();
        /*if (GC.shotCount <= 3 && !IsMouseOverUI())
        {
            Aim();
            Rotate();
        }*/
    }
}
