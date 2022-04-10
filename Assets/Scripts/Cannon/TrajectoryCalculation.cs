using System.Collections.Generic;
using UnityEngine;
using System;

namespace Cannon
{
    public class TrajectoryCalculation : MonoBehaviour
    {
        [SerializeField] private List<Renderer> _projectilesPath;
        [SerializeField] private Rigidbody2D _ballPrefab;

        private void Start()
        {
            EventManager.Aimed += OnPathCalculated;
            EventManager.HidedDots += OnHidedTheDots;
        }

        private void OnDestroy()
        {
            EventManager.Aimed -= OnPathCalculated;
            EventManager.HidedDots -= OnHidedTheDots;
        }

        /// <summary>
        /// Projectile line of flight
        /// </summary>
        private Vector2 DotPath(Vector2 position, Vector2 velocity, float t)
        {
            return position + velocity * t + (Physics2D.gravity / 2) * (float)Math.Pow(t, 2);
        }

        private void ShowTheDots()
        {
            for (int i = 0; i < _projectilesPath.Count; i++)
                _projectilesPath[i].enabled = true;
        }

        private void OnHidedTheDots()
        {
            for (int i = 0; i < _projectilesPath.Count; i++)
                _projectilesPath[i].enabled = false;
        }

        /// <summary>
        /// Formula: V = F/m
        /// </summary>
        private Vector2 VelocityCalculation()
        {
            return ShootedForce(Input.mousePosition) * Time.fixedDeltaTime / _ballPrefab.mass;
        }

        private void OnPathCalculated()
        {
            for (int i = 0; i < _projectilesPath.Count; i++)
            {
                float t = i / (float)_projectilesPath.Count;
                ShowTheDots();
                _projectilesPath[i].transform.position = DotPath(transform.position, VelocityCalculation(), t);
            }
        }
    }
}
