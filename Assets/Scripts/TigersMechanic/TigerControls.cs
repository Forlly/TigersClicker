using DefaultNamespace.Events;
using Project;
using UnityEngine;

namespace DefaultNamespace.TigersMechanic
{
    public class TigerControls :MonoBehaviour
    {
        [SerializeField] public Transform _centerPoint; 
        [SerializeField] public float _speed = 5.0f; 
        [SerializeField] public float _radiusX = 7.0f;
        [SerializeField] public float _radiusZ = 5.0f; 
        [SerializeField] private float _angle;
        [Header("Acceleration Data")]
        [SerializeField]  private float _accelerationDuration = 0.5f;
        [SerializeField]  private float _minAccelerationRatio = 2f;
        [SerializeField]  private float _maxAccelerationRatio = 2f;
        private bool isAccelerating = false;
        private float _originalY;
        private float _originalSpeed;
        private float _accelerationEndTime; 

        void Awake()
        {
            EventBus.Instance.AddListener<OnClickInputManager>(IncreaseSpeed);
            _originalY = transform.position.y;
            _originalSpeed = _speed;
        }
        public void SetCenterPoint(Transform center)
        {
            _centerPoint = center;
        }
        private void IncreaseSpeed(OnClickInputManager e)
        {
            _speed = _originalSpeed * Random.Range(_minAccelerationRatio, _maxAccelerationRatio);
            _accelerationEndTime = Time.time + _accelerationDuration;
        }

        public void Move()
        {
            CheckAcceleration();
            _angle += _speed * Time.deltaTime;

            float angleInRadians = _angle * Mathf.Deg2Rad;
            
            Vector3 localOffset = new Vector3(
                Mathf.Cos(angleInRadians) * _radiusX,
                0,
                Mathf.Sin(angleInRadians) * _radiusZ
            );
            
            Vector3 worldOffset = _centerPoint.TransformDirection(localOffset);

            transform.position = new Vector3(_centerPoint.position.x + worldOffset.x, _originalY, _centerPoint.position.z + worldOffset.z);

            Vector3 localTangent = new Vector3(
                -Mathf.Sin(angleInRadians) * _radiusZ,
                0,
                Mathf.Cos(angleInRadians) * _radiusX
            ).normalized;
            
            Vector3 worldTangent = _centerPoint.TransformDirection(localTangent);
            
            transform.rotation = Quaternion.LookRotation(worldTangent);
        }
        
        private void CheckAcceleration()
        {
            if (Time.time >= _accelerationEndTime)
            {
                _speed = _originalSpeed; 
            }
            _angle += _speed * Time.deltaTime;
        }

    }
}