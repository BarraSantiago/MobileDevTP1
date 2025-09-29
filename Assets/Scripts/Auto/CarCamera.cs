using UnityEngine;

namespace Auto
{
    public class CarCamera : MonoBehaviour
    {
        public Transform target;
        public float height = 1f;
        public float positionDamping = 3f;
        public float velocityDamping = 3f;
        public float distance = 4f;
        public LayerMask ignoreLayers = -1;

        public float lejaniaZ = 1;

        private Vector3 _currentVelocity = Vector3.zero;

        private RaycastHit _hit;

        private Vector3 _prevVelocity = Vector3.zero;
        private LayerMask _raycastLayers = -1;

        private void Start()
        {
            _raycastLayers = ~ignoreLayers;
        }

        private void FixedUpdate()
        {
            _currentVelocity = Vector3.Lerp(_prevVelocity, target.root.GetComponent<Rigidbody>().linearVelocity,
                velocityDamping * Time.deltaTime);
            _currentVelocity.y = 0;
            _prevVelocity = _currentVelocity;
        }

        private void LateUpdate()
        {
            float speedFactor = Mathf.Clamp01(target.root.GetComponent<Rigidbody>().linearVelocity.magnitude / 70.0f);
            GetComponent<Camera>().fieldOfView = Mathf.Lerp(55, 72, speedFactor);
            float currentDistance = Mathf.Lerp(7.5f, 6.5f, speedFactor);

            _currentVelocity = _currentVelocity.normalized;

            Vector3 newTargetPosition = target.position + Vector3.up * height;
            Vector3 newPosition = newTargetPosition - _currentVelocity * currentDistance;
            newPosition.y = newTargetPosition.y;

            Vector3 targetDirection = newPosition - newTargetPosition;
            if (Physics.Raycast(newTargetPosition, targetDirection, out _hit, currentDistance, _raycastLayers))
                newPosition = _hit.point;

            newPosition += transform.forward * lejaniaZ; //diferencia en z agregada por mi

            transform.position = newPosition;
            transform.LookAt(newTargetPosition);

            //rotacion agregada por mi
            Vector3 vAux = transform.rotation.eulerAngles;
            vAux.x = 20;
            transform.eulerAngles = vAux;
        }
    }
}