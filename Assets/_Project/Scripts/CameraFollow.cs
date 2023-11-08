using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WorldGeneration
{
    public class CameraFollow : MonoBehaviour
    {
        public Transform target;
        public float smoothTime = 0.3f;
        public Vector3 offset;
        private Vector3 velocity = Vector3.zero;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (target != null) 
            {
                MoveCamera();
            }
        }

        private void MoveCamera()
        {
            Vector3 targetPosition = target.position + offset;

            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
        }

        private void OnEnable()
        {
            EventSystem.OnPlayerSpawned += SetTarget;
        }

        private void OnDisable()
        {
            EventSystem.OnPlayerSpawned -= SetTarget;
        }

        private void SetTarget(Transform _target)
        {
            target = _target;
        }
    }

}