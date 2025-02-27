using CoopHorror.CodeBase.Advanced;
using Fusion;
using UnityEngine;
using System;

namespace CoopHorror.CodeBase
{
    public class HiddingPlayer : NetworkBehaviour
    {
        public event Action HiddingStopped;
        public event Action DoorOpened;
        public event Action DoorClosed;
        
        public bool HiddingIsStarted { get; private set; } = false;
        
        [SerializeField] private float _animationSpeed;
        [SerializeField] private float _rotationSpeed;
        [SerializeField] private AdvancedPlayer _advancedPlayer;

        private Transform _positionInFrontOfCabinet;
        private Transform _shelterPosition;
        private Transform _wardrobeTransform;

        private NetworkObject _currentGameObject;
        private Vector2 _lookRotation;

        private bool _playerIsHidden = false;
        private bool _stopFirstAnimation = false;
        private bool _stopSecondAnimation = false;
        private bool _stopThirdAnimation = true;
        private bool _rotationIsStarted = false;
        private bool _rotationIsStopped = false;
        private bool _isStopped = false;

        private void Awake()
        {
            _currentGameObject = GetComponent<NetworkObject>();
        }

        public void StartHiding(Transform positionInFrontOfCabinet, 
            Transform shelterPosition, Transform wardrobeTransform)
        {
            _positionInFrontOfCabinet = positionInFrontOfCabinet;
            _shelterPosition = shelterPosition;
            _wardrobeTransform = wardrobeTransform;

            _lookRotation = _advancedPlayer.KCC.GetLookRotation();

            DoorOpened?.Invoke();

            HiddingIsStarted = true;
        }

        public void Hide()
        {
            if (HiddingIsStarted == false)
            {
                return;
            }

            _advancedPlayer.BanMovement();

            if (_rotationIsStarted == true && _rotationIsStopped == false)
            {
                RotationAnimation();
            }

            if (_currentGameObject.transform.position != new Vector3(_positionInFrontOfCabinet.position.x, 0f, _positionInFrontOfCabinet.position.z) && _stopFirstAnimation == false)
            {
                MovingTowardsTheGoal(_positionInFrontOfCabinet.position, ref _stopFirstAnimation);

                return;
            }

            if (_rotationIsStarted == false)
            {
                _rotationIsStarted = true;
            }

            if (_currentGameObject.transform.position != new Vector3(_shelterPosition.position.x, 0f, _shelterPosition.position.z) && _stopSecondAnimation == false)
            {
                MovingTowardsTheGoal(_shelterPosition.position, ref _stopSecondAnimation);

                return;
            }

            if (_playerIsHidden == false)
            {
                _advancedPlayer.KCC.SetInputDirection(Vector3.zero);
                _playerIsHidden = true;
                DoorClosed?.Invoke();
            }


            if (_currentGameObject.transform.position != new Vector3(_positionInFrontOfCabinet.position.x, 0f, _positionInFrontOfCabinet.position.z) && _stopThirdAnimation == false)
            {
                MovingTowardsTheGoal(_positionInFrontOfCabinet.position, ref _stopThirdAnimation);

                return;
            }

            if (_advancedPlayer.Input.CurrentInput.MoveDirection != Vector2.zero && _playerIsHidden && _rotationIsStopped && _isStopped == false)
            {
                DoorOpened?.Invoke();
                _isStopped = true;
                _stopThirdAnimation = false;
                _advancedPlayer.KCC.SetLookRotation(new Vector2(0f, _wardrobeTransform.rotation.eulerAngles.y - 180f));
                return;
            }

            if (_isStopped == true && _stopThirdAnimation == true)
            {
                DoorClosed?.Invoke();
                _isStopped = false;
                _advancedPlayer.AllowMovement();
                _stopFirstAnimation = false;
                _stopSecondAnimation = false;
                _stopThirdAnimation = true;
                _playerIsHidden = false;
                _rotationIsStarted = false;
                _rotationIsStopped = false;
                HiddingIsStarted = false;

                _shelterPosition = null;
                _positionInFrontOfCabinet = null;

                HiddingStopped.Invoke();
            }
        }

        private void MovingTowardsTheGoal(Vector3 endPosition, ref bool stopAnimation)
        {
            Vector3 diraction = (new Vector3(endPosition.x, 0f, endPosition.z) - _currentGameObject.transform.position).normalized;

            _advancedPlayer.KCC.SetInputDirection(diraction * Runner.DeltaTime * _animationSpeed);

            if (Vector3.Distance(endPosition, _currentGameObject.transform.position) < 0.3f)
            {
                _currentGameObject.transform.position = endPosition;
                stopAnimation = true;
            }
        }

        private void RotationAnimation()
        {
            Vector2 endPosition = new Vector2(0f, _wardrobeTransform.rotation.eulerAngles.y - 180f);

            if (_advancedPlayer.KCC.GetLookRotation() != endPosition || _currentGameObject == null)
            {
                if (_lookRotation.x < Runner.DeltaTime * _rotationSpeed)
                {
                    _lookRotation.x = 0f;
                }
                else
                {
                    _lookRotation.x += -Mathf.Sign(_lookRotation.x) * Runner.DeltaTime * _rotationSpeed;
                }

                if (_lookRotation.y > endPosition.y - Runner.DeltaTime * _rotationSpeed && _lookRotation.y < endPosition.y + Runner.DeltaTime * _rotationSpeed)
                {
                    _lookRotation.y = endPosition.y;
                }
                else if (_lookRotation.y < -endPosition.y && _lookRotation.y > endPosition.y 
                    || _lookRotation.y < 180f && Mathf.Sign(_lookRotation.y) != -1 && Mathf.Sign(endPosition.y) == -1)
                {
                    _lookRotation.y -= Runner.DeltaTime * _rotationSpeed;
                }
                else
                {
                    _lookRotation.y += Runner.DeltaTime * _rotationSpeed;
                }

                _advancedPlayer.KCC.SetLookRotation(_lookRotation);

                return;
            }

            _rotationIsStopped = true;
        }
    }
}