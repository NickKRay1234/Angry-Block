using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CannonMechanic : MonoBehaviour
{
    [SerializeField] private float _power = 2f;
    [SerializeField] private int _amountOfDots = 15;

    private Vector2 _startPosition;
    private bool _isShooting;
    private bool _isAiming;

    [SerializeField] private GameObject Dots;
    [SerializeField] private List<GameObject> _projectilesPath;

    private Rigidbody2D _ballBody;
    public GameObject ballPrefab;
    public GameObject ballsContainer;
    
    private void Start()
    {
        Dots = GameObject.Find("Dots");
        _projectilesPath = Dots.transform.Cast<Transform>().ToList().ConvertAll(t => t.gameObject);
        for (int i = 0; i < _projectilesPath.Count; i++)
        {
            _projectilesPath[i].GetComponent<Renderer>().enabled = false; // Выключение всех точек прицеливания
        }
    }

    private void Aim() // Прицеливание
    {
        if (_isShooting) return;
        if (Input.GetMouseButton(0)) // Пока клавиша зажата
        {
            if (!_isAiming) // Когда клавиша зажата выстрел не происходит.
            {
                _isAiming = true;
                _startPosition = Input.mousePosition;
            }
            else // Как только клавиша резко нажимается или отпускается, то происходит выстрел
            {
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

    private Vector2 ShootForce(Vector2 force) // Сила выстрела
    {
        Debug.Log(new Vector2(_startPosition.x, _startPosition.y) - (new Vector2(force.x, force.y)) * _power);
        return (new Vector2(_startPosition.x, _startPosition.y) - (new Vector2(force.x, force.y)) * _power);
    }

    private Vector2 DotPath(Vector2 startPosition, Vector2 startVelocity, float t) // Точечный путь
    {
        return startPosition + startVelocity * t + 0.5f * Physics2D.gravity * (int)Math.Pow(t,2);
    }

    private void PathCalculation() // Расчёт пути
    {
        Debug.Log(Input.mousePosition);
        Vector2 velocity = ShootForce(Input.mousePosition) * Time.fixedDeltaTime / _ballBody.mass;

        for (int i = 0; i < _projectilesPath.Count; i++)
        {
            _projectilesPath[i].GetComponent<Renderer>().enabled = true; // Включение точек прицеливания
            float t = i / 15f;
            Vector3 point = DotPath(transform.position, velocity, t);
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
