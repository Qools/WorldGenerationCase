using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace WorldGeneration
{
    public class CharacterController : MonoBehaviour
    {
        [SerializeField] private Animator characterAnimator;

        [SerializeField] private float characterSpeed;
        private Vector2 move;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            MovePlayer();
        }

        public void OnMove(InputAction.CallbackContext context)
        {
            move = context.ReadValue<Vector2>();
        }

        private void MovePlayer()
        {
            Vector3 _movement = new Vector3(move.x, 0f, move.y);
            float _velocity = _movement.magnitude * characterSpeed;

            SetAnimationSpeed(_velocity);

            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(_movement), 0.15f);

            transform.Translate(_movement * characterSpeed * Time.deltaTime, Space.World);
        }

        private void SetAnimationSpeed(float _speed)
        {
            characterAnimator.SetFloat("Speed", _speed);
        }
    }

}