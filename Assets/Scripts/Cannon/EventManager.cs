using UnityEngine;
using System;

namespace Cannon
{
    public class EventManager : MonoBehaviour
    {
        public static event Action Aimed;
        public static event Action HidedDots;
        public static event Action<Vector2> ShootedForce;

        public static void OnShootedForce()
        {
            ShootedForce?.Invoke();
        }

        public static void OnAimed()
        {
            Aimed?.Invoke();
        }

        public static void OnHidedDots()
        {
            HidedDots?.Invoke();
        }
    }
}
