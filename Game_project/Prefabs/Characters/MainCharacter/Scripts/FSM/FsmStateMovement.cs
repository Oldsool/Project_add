using UnityEngine;

namespace Assets.Game_project.Prefabs.Characters.MainCharacter.Scripts.FSM
{
    public class FsmStateMovement : FsmState
    {
        protected readonly Transform Transform;
        protected readonly float Speed;

        public FsmStateMovement(Fsm fsm, Transform transform, float speed) : base(fsm) 
        {
            Transform = transform;
            Speed = speed;
        }

        public override void Enter()
        {
            Debug.Log("Movement state [ENTER]");
        }

        public override void Exit() 
        {
            Debug.Log("Movement state [EXIT]");
        }

        public override void Update()
        {
            Debug.Log("Movement state [UPDATE]");

            var inputDirection = ReadInput();

            if (inputDirection.sqrMagnitude == 0f)
            {
                Fsm.SetState<FsmStateIdle>();
            }

            Move(inputDirection);
        }

        protected Vector2 ReadInput()
        {
            var inputHorizontal = Input.GetAxis("Horizontal");
            var inputVertical = Input.GetAxis("Vertical");
            var inputDirection = new Vector2(inputHorizontal, inputVertical);
            return inputDirection;
        }

        protected virtual void Move(Vector2 inputDirection)
        {
            Vector3 moveDirection = Transform.right * inputDirection.x + Transform.forward * inputDirection.y;
            Transform.position += moveDirection * Speed * Time.deltaTime;


            if (inputDirection.x >= 0.1 || inputDirection.y >= 0.1)
            {
                Transform.rotation = Quaternion.Slerp(
                    Transform.rotation,
                    Quaternion.Euler(0,
                    Camera.main.transform.eulerAngles.y,
                    0), Time.fixedDeltaTime * 14f);

            }  // скрипт для поворота по камере

            if (Input.GetKey(KeyCode.A))
            {
                rotatePlayer();
            }
        }

        protected void rotatePlayer()
        {
            Transform.rotation = Quaternion.Slerp(
                    Transform.rotation,
                    Quaternion.Euler(0,
                    Transform.rotation.y - 90f,
                    0), Time.fixedDeltaTime * 14f);
        }

        protected void MoveToPlayer()
        {

        }
    }
}