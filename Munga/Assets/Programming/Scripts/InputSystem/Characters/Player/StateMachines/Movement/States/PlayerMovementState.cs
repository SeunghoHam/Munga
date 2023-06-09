using System.Collections.Generic;
using Assets.Scripts.Manager;
using UnityEngine;
using UnityEngine.InputSystem;

namespace GenshinImpactMovementSystem
{
    public class PlayerMovementState : IState
    {
        protected PlayerMovementStateMachine stateMachine;

        protected readonly PlayerGroundedData groundedData;
        protected readonly PlayerAirborneData airborneData;
        //protected readonly PlayerAttackData attackData;
        
        public PlayerMovementState(PlayerMovementStateMachine playerMovementStateMachine)
        {
            stateMachine = playerMovementStateMachine;

            groundedData = stateMachine.Player.Data.GroundedData;
            airborneData = stateMachine.Player.Data.AirborneData;
            //attackData = stateMachine.Player.Data.AttackData;
            
            InitializeData();
        }

        public virtual void Enter()
        {
            AddInputActionsCallbacks();
        }

        public virtual void Exit()
        {
            RemoveInputActionsCallbacks();
        }

        public virtual void HandleInput()
        {
            ReadMovementInput();
        }

        public virtual void Update()
        {
            
        }

        public virtual void PhysicsUpdate()
        {
            Move();
        }

        public virtual void OnTriggerEnter(Collider collider)
        {
            Debug.Log("collidernameEnter : " +collider.name);
            if (stateMachine.Player.LayerData.IsGroundLayer(collider.gameObject.layer))
            {
                OnContactWithGround(collider);
                return;
            }
        }

        public virtual void OnTriggerExit(Collider collider)
        {
            Debug.Log("collidernameExit : "+ collider.name);
            if (stateMachine.Player.LayerData.IsGroundLayer(collider.gameObject.layer))
            {
                OnContactWithGroundExited(collider);

                return;
            }
        }

        public virtual void OnAnimationEnterEvent()
        {
        }

        public virtual void OnAnimationExitEvent()
        {
        }

        public virtual void OnAnimationTransitionEvent()
        {
        }

        private void InitializeData()
        {
            SetBaseCameraRecenteringData();

            SetBaseRotationData();
        }

        protected void SetBaseCameraRecenteringData()
        {
            stateMachine.ReusableData.SidewaysCameraRecenteringData = groundedData.SidewaysCameraRecenteringData;
            stateMachine.ReusableData.BackwardsCameraRecenteringData = groundedData.BackwardsCameraRecenteringData;
        }

        protected void SetBaseRotationData()
        {
            stateMachine.ReusableData.RotationData = groundedData.BaseRotationData;
            stateMachine.ReusableData.TimeToReachTargetRotation = stateMachine.ReusableData.RotationData.TargetRotationReachTime;
        }

        protected void StartAnimation(int animationHash)
        {
            stateMachine.Player.Animator.SetBool(animationHash, true);
        }

        protected void StopAnimation(int animationHash)
        {
            stateMachine.Player.Animator.SetBool(animationHash, false);
        }

        protected virtual void AddInputActionsCallbacks()
        {
            stateMachine.Player.Input.PlayerActions.WalkToggle.started += OnWalkToggleStarted;

            stateMachine.Player.Input.PlayerActions.Look.started += OnMouseMovementStarted;

            stateMachine.Player.Input.PlayerActions.Movement.performed += OnMovementPerformed;
            stateMachine.Player.Input.PlayerActions.Movement.canceled += OnMovementCanceled;
            
            stateMachine.Player.Input.PlayerActions.Attack.started += OnAttackStarted;
            stateMachine.Player.Input.PlayerActions.Parrying.started += OnParryingStarted;
            stateMachine.Player.Input.PlayerActions.Pin.started += OnPin;
        }

        protected virtual void RemoveInputActionsCallbacks()
        {
            stateMachine.Player.Input.PlayerActions.WalkToggle.started -= OnWalkToggleStarted;

            stateMachine.Player.Input.PlayerActions.Look.started -= OnMouseMovementStarted;

            stateMachine.Player.Input.PlayerActions.Movement.performed -= OnMovementPerformed;
            stateMachine.Player.Input.PlayerActions.Movement.canceled -= OnMovementCanceled;
            
            stateMachine.Player.Input.PlayerActions.Attack.started -= OnAttackStarted;
            stateMachine.Player.Input.PlayerActions.Parrying.started -= OnParryingStarted;
            stateMachine.Player.Input.PlayerActions.Pin.started -= OnPin;
        }

        protected virtual void OnWalkToggleStarted(InputAction.CallbackContext context)
        {
            stateMachine.ReusableData.ShouldWalk = !stateMachine.ReusableData.ShouldWalk;
        }

        private void OnMouseMovementStarted(InputAction.CallbackContext context)
        {
            if (BattleManager.Instance.InputMode == InputMode.UI)
                return;
            UpdateCameraRecenteringState(stateMachine.ReusableData.MovementInput);
        }

        protected virtual void OnMovementPerformed(InputAction.CallbackContext context)
        {
            if (BattleManager.Instance.InputMode == InputMode.UI)
                return;
            UpdateCameraRecenteringState(context.ReadValue<Vector2>());
        }
        protected virtual void OnMovementCanceled(InputAction.CallbackContext context)
        {
            DisableCameraRecentering();
        }
        protected virtual void OnAttackStarted(InputAction.CallbackContext context)
        {
            if (BattleManager.Instance.InputMode == InputMode.UI)
                return;
            Debug.Log("공격 반환");
            stateMachine.ChangeState(stateMachine.AttackState);
        }

        protected virtual void OnParryingStarted(InputAction.CallbackContext context)
        {
            //패링 ;
        }
        
        private void OnPin(InputAction.CallbackContext context)
        {
            if (BattleManager.Instance.InputMode == InputMode.UI)
                return;
            
            // 시점 고정
            //BattleManager.Instance.cameraSystem.PinTargetSetting();
            CameraController.instance.PinTargetSetting();
        }
        private void ReadMovementInput()
        {
            stateMachine.ReusableData.MovementInput = stateMachine.Player.Input.PlayerActions.Movement.ReadValue<Vector2>();
        }

        
        private void Move()
        {
            if(stateMachine.CurrentState == stateMachine.AttackState)
                stateMachine.Player.Rigidbody.velocity = Vector3.zero;
            
            if (stateMachine.ReusableData.MovementInput == Vector2.zero || stateMachine.ReusableData.MovementSpeedModifier == 0f
                || stateMachine.CurrentState == stateMachine.AttackState)
            {
                return;
            }

            Vector3 movementDirection = GetMovementInputDirection();

            float targetRotationYAngle = Rotate(movementDirection);

            Vector3 targetRotationDirection = GetTargetRotationDirection(targetRotationYAngle);

            float movementSpeed = GetMovementSpeed();

            Vector3 currentPlayerHorizontalVelocity = GetPlayerHorizontalVelocity();
            

            // Pin Mode 에 따라서 회전이 다르게 적용되록
            // player : Rotate상시
            // monster : 회전 적용이 뒤로 갈때는 적용 안되도록
            if (CameraController.instance._pinType == PinType.Player)
            {
                // wasd 모두 적용되도록
            }
            else if (CameraController.instance._pinType == PinType.Monster)
            {
                //stateMachine.Player.Rigidbody.AddForce(targetRotationDirection * movementSpeed - currentPlayerHorizontalVelocity, ForceMode.VelocityChange);
            }
            stateMachine.Player.Rigidbody.AddForce(targetRotationDirection * movementSpeed - currentPlayerHorizontalVelocity, ForceMode.VelocityChange);
        }

        protected Vector3 GetMovementInputDirection()
        {
            /*
            Debug.Log("InputDirection : " +
                      new Vector3(stateMachine.ReusableData.MovementInput.x, 0f,
                          stateMachine.ReusableData.MovementInput.y));
                          */
            // 반환값의 z 가 음수일때 : 뒤로가는중
            // Pin 상태라면 회전은 시키지말고 위치만 이동되도록
            return new Vector3(stateMachine.ReusableData.MovementInput.x, 0f,
                stateMachine.ReusableData.MovementInput.y);
        }

        
        /// <summary>
        /// Pin[On] 상태일 때 공격시 적 방향으로 캐릭터 회전
        /// </summary>
        protected void PlayerDirectionToAttackVector()
        {
            // 공격한 방향으로 캐릭터 회전
            // 첫 공격 말고는 호출할 필요가 있을까? 싶은 느낌이랄까
            if (CameraController.instance._pinType == PinType.Player)
                return;
            Vector3 dir = BattleManager.Instance.currentPinMonster.transform.position - stateMachine.Player.transform.position;
            var nextRot = Quaternion.LookRotation(dir);
            stateMachine.Player.transform.rotation = nextRot;
        }

        private float Rotate(Vector3 direction)
        {
            float directionAngle = UpdateTargetRotation(direction);
            
            RotateTowardsTargetRotation();
            
            return directionAngle;
        }
        
        protected float UpdateTargetRotation(Vector3 direction, bool shouldConsiderCameraRotation = true)
        {
            float directionAngle = GetDirectionAngle(direction);

            if (shouldConsiderCameraRotation)
            {
                directionAngle = AddCameraRotationToAngle(directionAngle);
            }

            if (directionAngle != stateMachine.ReusableData.CurrentTargetRotation.y)
            {
                UpdateTargetRotationData(directionAngle);
            }

            return directionAngle;
        }

        private float GetDirectionAngle(Vector3 direction)
        {
            float directionAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;

            if (directionAngle < 0f)
            {
                directionAngle += 360f;
            }

            return directionAngle;
        }

        private float AddCameraRotationToAngle(float angle)
        {
            angle += stateMachine.Player.MainCameraTransform.eulerAngles.y;

            if (angle > 360f)
            {
                angle -= 360f;
            }

            return angle;
        }

        private void UpdateTargetRotationData(float targetAngle)
        {
            stateMachine.ReusableData.CurrentTargetRotation.y = targetAngle;

            stateMachine.ReusableData.DampedTargetRotationPassedTime.y = 0f;
        }

        protected void RotateTowardsTargetRotation()
        {
            float currentYAngle = stateMachine.Player.Rigidbody.rotation.eulerAngles.y;

            if (currentYAngle == stateMachine.ReusableData.CurrentTargetRotation.y)
            {
                return;
            }

            float smoothedYAngle = Mathf.SmoothDampAngle(currentYAngle, stateMachine.ReusableData.CurrentTargetRotation.y, ref stateMachine.ReusableData.DampedTargetRotationCurrentVelocity.y, stateMachine.ReusableData.TimeToReachTargetRotation.y - stateMachine.ReusableData.DampedTargetRotationPassedTime.y);

            stateMachine.ReusableData.DampedTargetRotationPassedTime.y += Time.deltaTime;

            Quaternion targetRotation = Quaternion.Euler(0f, smoothedYAngle, 0f);

            stateMachine.Player.Rigidbody.MoveRotation(targetRotation);
        }

        protected Vector3 GetTargetRotationDirection(float targetRotationAngle)
        {
            return Quaternion.Euler(0f, targetRotationAngle, 0f) * Vector3.forward;
        }

        protected float GetMovementSpeed(bool shouldConsiderSlopes = true)
        {
            float movementSpeed = groundedData.BaseSpeed * stateMachine.ReusableData.MovementSpeedModifier;

            if (shouldConsiderSlopes)
            {
                movementSpeed *= stateMachine.ReusableData.MovementOnSlopesSpeedModifier;
            }

            return movementSpeed;
        }

        protected Vector3 GetPlayerHorizontalVelocity()
        {
            Vector3 playerHorizontalVelocity = stateMachine.Player.Rigidbody.velocity;

            playerHorizontalVelocity.y = 0f;

            return playerHorizontalVelocity;
        }

        protected Vector3 GetPlayerVerticalVelocity()
        {
            return new Vector3(0f, stateMachine.Player.Rigidbody.velocity.y, 0f);
        }

        protected virtual void OnContactWithGround(Collider collider)
        {
            Debug.Log("colliderName : "+ collider.name);
        }

        protected virtual void OnContactWithGroundExited(Collider collider)
        {
        }

        protected void UpdateCameraRecenteringState(Vector2 movementInput)
        {
            if (movementInput == Vector2.zero)
            {
                return;
            }

            if (movementInput == Vector2.up)
            {
                DisableCameraRecentering();

                return;
            }

            float cameraVerticalAngle = stateMachine.Player.MainCameraTransform.eulerAngles.x;

            if (cameraVerticalAngle >= 270f)
            {
                cameraVerticalAngle -= 360f;
            }

            cameraVerticalAngle = Mathf.Abs(cameraVerticalAngle);

            if (movementInput == Vector2.down)
            {
                SetCameraRecenteringState(cameraVerticalAngle, stateMachine.ReusableData.BackwardsCameraRecenteringData);

                return;
            }

            SetCameraRecenteringState(cameraVerticalAngle, stateMachine.ReusableData.SidewaysCameraRecenteringData);
        }

        protected void SetCameraRecenteringState(float cameraVerticalAngle, List<PlayerCameraRecenteringData> cameraRecenteringData)
        {
            foreach (PlayerCameraRecenteringData recenteringData in cameraRecenteringData)
            {
                if (!recenteringData.IsWithinRange(cameraVerticalAngle))
                {
                    continue;
                }

                EnableCameraRecentering(recenteringData.WaitTime, recenteringData.RecenteringTime);

                return;
            }

            DisableCameraRecentering();
        }

        protected void EnableCameraRecentering(float waitTime = -1f, float recenteringTime = -1f)
        {
            float movementSpeed = GetMovementSpeed();

            if (movementSpeed == 0f)
            {
                movementSpeed = groundedData.BaseSpeed;
            }

            stateMachine.Player.CameraRecenteringUtility.EnableRecentering(waitTime, recenteringTime, groundedData.BaseSpeed, movementSpeed);
        }

        protected void DisableCameraRecentering()
        {
            stateMachine.Player.CameraRecenteringUtility.DisableRecentering();
        }

        protected void ResetVelocity()
        {
            stateMachine.Player.Rigidbody.velocity = Vector3.zero;
        }

        protected void ResetVerticalVelocity()
        {
            Vector3 playerHorizontalVelocity = GetPlayerHorizontalVelocity();

            stateMachine.Player.Rigidbody.velocity = playerHorizontalVelocity;
        }

        protected void DecelerateHorizontally()
        {
            Vector3 playerHorizontalVelocity = GetPlayerHorizontalVelocity();

            stateMachine.Player.Rigidbody.AddForce(-playerHorizontalVelocity * stateMachine.ReusableData.MovementDecelerationForce, ForceMode.Acceleration);
        }

        protected void DecelerateVertically()
        {
            Vector3 playerVerticalVelocity = GetPlayerVerticalVelocity();

            stateMachine.Player.Rigidbody.AddForce(-playerVerticalVelocity * stateMachine.ReusableData.MovementDecelerationForce, ForceMode.Acceleration);
        }

        protected bool IsMovingHorizontally(float minimumMagnitude = 0.1f)
        {
            Vector3 playerHorizontaVelocity = GetPlayerHorizontalVelocity();

            Vector2 playerHorizontalMovement = new Vector2(playerHorizontaVelocity.x, playerHorizontaVelocity.z);

            return playerHorizontalMovement.magnitude > minimumMagnitude;
        }

        protected bool IsMovingUp(float minimumVelocity = 0.1f)
        {
            return GetPlayerVerticalVelocity().y > minimumVelocity;
        }

        protected bool IsMovingDown(float minimumVelocity = 0.1f)
        {
            return GetPlayerVerticalVelocity().y < -minimumVelocity;
        }
    }
}