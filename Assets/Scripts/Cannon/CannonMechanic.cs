using UnityEngine;
using System.Collections;

namespace Cannon
{
    [RequireComponent(typeof(TrajectoryCalculation))]
    public class CannonMechanic : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D _ballPrefab;
        [SerializeField] private Transform _cannonTarget;
        [SerializeField] private GameObject _ballsContainer;
        [SerializeField] private float _power = 2f;
        private TrajectoryCalculation _trajectory;

        private bool _isShooting = false;
        private bool _isAiming;

        private void Awake()
        {
            _trajectory = GetComponent<TrajectoryCalculation>();
        }

        private void Aim()
        {
            if (_isShooting) return;
            if (Input.GetMouseButton(0)) // While button is pressed
            {
                if (!_isAiming) _isAiming = true;
                else
                {
                    _trajectory.PathCalculation();
                    Rotate();
                }
            }
            else if (_isAiming && !_isShooting)
            {
                _isAiming = false;
                StartCoroutine(Shoot());
                _trajectory.HideDots();
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
        
        /// <summary>
        /// Return negative mouse position multiplied by power
        /// </summary>
        public Vector2 ShootForce(Vector2 force)
        {
            return new Vector2(Input.mousePosition.x, Input.mousePosition.y) -
                   ((new Vector2(force.x, force.y)) * _power);
        }

        IEnumerator Shoot()
        {
            for (int i = 0; i < 5; i++)
            {
                yield return new WaitForSeconds(0.07f);
                Rigidbody2D ball = Instantiate(_ballPrefab, transform.position, Quaternion.identity);
                ball.transform.SetParent(_ballsContainer.transform);
                ball.AddForce(ShootForce(Input.mousePosition));
            }
        }

    }
}
