using System.Collections.Generic;
using UnityEngine;
using System;

public class CannonMechanic : MonoBehaviour
{
    [SerializeField] private float _power = 2f;
    [SerializeField] private Rigidbody2D _ballPrefab;
    [SerializeField] private List<Renderer> _projectilesPath;
    
    private bool _isShooting = false;
    private bool _isAiming;



    private void Aim()
    {
        if (_isShooting) return;
        if (Input.GetMouseButton(0)) // While button is pressed
        {
            if (!_isAiming)
                _isAiming = true;
            else
                PathCalculation();
        }
        else if (_isAiming && !_isShooting)
            _isAiming = false;
    }

    /// <summary>
    /// Return negative mouse position multiplied by power
    /// </summary>
    private Vector2 ShootForce(Vector2 force)
    {
        return new Vector2(Input.mousePosition.x, Input.mousePosition.y) - ((new Vector2(force.x, force.y)) * _power);
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
            float t = i / (float)_projectilesPath.Count;
            _projectilesPath[i].enabled = true;
            _projectilesPath[i].transform.position = DotPath(transform.position, VelocityCalculation(), t);
        }
    }
    
    private void Update()
    {
        Aim();
    }
}
