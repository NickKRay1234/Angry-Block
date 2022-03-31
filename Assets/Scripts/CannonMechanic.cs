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
        Dots = GameObject.Find("dots");
        _projectilesPath = Dots.transform.Cast<Transform>().ToList().ConvertAll(t => t.gameObject);
        for (int i = 0; i < _projectilesPath.Count; i++)
        {
            _projectilesPath[i].GetComponent<Renderer>().enabled = false;
        }
    }

    private void Aim()
    {
        if (_isShooting) return;
        if (Input.GetAxis("Fire1") == 1)
        {
            if (!_isAiming)
            {
                print("1St part");
                //cal
            }
            else
            {
                //Aim Cal path
            }
        }
        else
        {
            //Shoot
        }
    }

    
    private void Update()
    {
        Aim();
    }
}
