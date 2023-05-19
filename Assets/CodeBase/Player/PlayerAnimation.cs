using UnityEngine;

namespace CodeBase.Player
{
    public class PlayerAnimation : MonoBehaviour
    {
        private readonly int JumpHash = Animator.StringToHash("Jump");

        [SerializeField] private Animator _animator;

        public void PlayJump() =>
            _animator.SetTrigger(JumpHash);
    }
}