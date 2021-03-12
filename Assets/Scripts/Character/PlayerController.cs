using UnityEngine;

namespace Character
{
    [RequireComponent(typeof(PlayerHealthComponent))]
    public class PlayerController : MonoBehaviour
    {
        public CrosshairScript Crosshair => CrosshairComponent;
        [SerializeField] private CrosshairScript CrosshairComponent;

        public bool IsFiring;
        public bool IsReloading;
        public bool IsJumping;
        public bool IsRunning;
    }
}
