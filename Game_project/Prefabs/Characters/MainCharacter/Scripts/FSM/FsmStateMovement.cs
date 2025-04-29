using Unity.MLAgents.Integrations.Match3;
using UnityEngine;

namespace Assets.Game_project.Prefabs.Characters.MainCharacter.Scripts.FSM
{
    public class FsmStateMovement : FsmState
    {
        protected readonly Transform Transform;
        protected readonly float Speed;
        protected Vector3 moveDirection;

        float inputHorizontal;

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
            inputHorizontal = Input.GetAxis("Horizontal");
            var inputVertical = Input.GetAxis("Vertical");
            var inputDirection = new Vector2(inputHorizontal, inputVertical);
            return inputDirection;
        }

        protected virtual void Move(Vector2 inputDirection)
        {
            moveDirection = Transform.right * inputDirection.x + Transform.forward * inputDirection.y;


            if (Input.GetKey(KeyCode.W))
            {
                MovementPlayerforWS(1);
                RotatePlayer(0);

            }

            if (Input.GetKey(KeyCode.S))
            {
                MovementPlayerforWS(-1);
                RotatePlayer(180);
            }

            if (Input.GetKey(KeyCode.D))
            {
                RotatePlayer(90);
                MovementPlayerforAD(1);
            }

            if (Input.GetKey(KeyCode.A))
            {
                RotatePlayer(-90);
                MovementPlayerforAD(-1);
            }
        }

        protected void MovementPlayerforAD(int mnj) 
        {
            Transform.position += Transform.forward * inputHorizontal * mnj * (Speed * Time.deltaTime);
        }    // движение по A and D
        protected void MovementPlayerforWS(int mnj)
        {
            Transform.position += moveDirection * (Speed * Time.deltaTime) * mnj;
        }//  движение по W and S

        protected void RotatePlayer(int _rot)
        {
            Transform.rotation = Quaternion.Slerp(
                    Transform.rotation,
                    Quaternion.Euler(0,
                    Camera.main.transform.eulerAngles.y + (_rot),
                    0), Time.fixedDeltaTime * 14);
        }  // поворот обьекта относительно камеры
    }
}