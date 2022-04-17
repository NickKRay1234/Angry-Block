using System.Collections.Generic;
using UnityEngine;
using System;

namespace Cannon
{
    public class TrajectoryCalculation : MonoBehaviour
    {
        [SerializeField] private List<Renderer> _projectilesPath;
        [SerializeField] private Rigidbody2D _ballPrefab;
        private CannonMechanic _cannon;

        private void Awake()
        {
            _cannon = GetComponent<CannonMechanic>();
        }
        
        private Vector2 DotPath(Vector2 position, Vector2 velocity, float distance)
        {
            return position + velocity * distance + (Physics2D.gravity / 2) * (float)Math.Pow(distance, 2);
        }

        private void ShowTheDots()
        {
            for (int i = 0; i < _projectilesPath.Count; i++)
                _projectilesPath[i].enabled = true;
        }

        public void HideDots()
        {
            for (int i = 0; i < _projectilesPath.Count; i++)
                _projectilesPath[i].enabled = false;
        }
        
        private Vector2 VelocityCalculation() // V = F/m
        {
            return _cannon.ShootForce(Input.mousePosition) * Time.fixedDeltaTime / _ballPrefab.mass;
        }

        public void PathCalculation()
        {
            for(int i = 0; i < _projectilesPath.Count; i++)
            {
                float distance = i / (float)_projectilesPath.Count;
                ShowTheDots();
                _projectilesPath[i].transform.position = DotPath(transform.position, VelocityCalculation(), distance);
            }
        }
    }
}
