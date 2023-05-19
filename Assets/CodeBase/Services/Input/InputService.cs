using UnityEngine;

namespace CodeBase.Services.Input
{
    public class InputService : IInputService
    {
        public float MoveAxis => Mathf.Clamp(_mainInput.Player.Move.ReadValue<Vector2>().x, -1, 1);
        public bool TouchScreenPress => _mainInput.Player.TouchPress.IsPressed();
        private readonly MainInput _mainInput;

        public InputService()
        {
            _mainInput = new MainInput();
            _mainInput.Player.Enable();
        }
    }
}