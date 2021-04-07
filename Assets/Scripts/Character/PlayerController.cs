using UnityEngine;
using UnityEngine.InputSystem;

namespace Character
{
    [RequireComponent(typeof(PlayerHealthComponent))]
    public class PlayerController : MonoBehaviour , IPauseable
    {
        public CrosshairScript Crosshair => CrosshairComponent;
        [SerializeField] private CrosshairScript CrosshairComponent;

        public bool IsFiring;
        public bool IsReloading;
        public bool IsJumping;
        public bool IsRunning;

        private GameUIController UIController;
        private PlayerInput PlayerInput;

        public HealthComponant Health => HealthComponant;
        private HealthComponant HealthComponant;

        public WeaponHolder WeaponHolder => WeaponHolderComponant;
        private WeaponHolder WeaponHolderComponant;

        private void Awake()
        {
            UIController = FindObjectOfType<GameUIController>();
            PlayerInput = GetComponent<PlayerInput>();
            if (HealthComponant == null) HealthComponant = GetComponent<HealthComponant>();
            if (WeaponHolderComponant == null) WeaponHolderComponant = GetComponent<WeaponHolder>();
        }

        public void OnPauseGame(InputValue value)
        {
            PauseManager.Instance.PauseGame();
        }

        public void OnUnPauseGame(InputValue value)
        {
            PauseManager.Instance.UnPauseGame();
        }

        public void PauseMenu()
        {
            UIController.EnablePauseMenu();
            PlayerInput.SwitchCurrentActionMap("PauseActionMap");
        }

        public void UnPauseMenu()
        {
            UIController.EnableGameMenu();
            PlayerInput.SwitchCurrentActionMap("Third Person");
        }

        
    }
}
