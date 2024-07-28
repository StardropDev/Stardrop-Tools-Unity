
using UnityEngine;

namespace StardropTools.CharacterControllers
{
    public abstract class CharacterControllerState
    {
        private CharacterControllerStateMachine stateMachine;

        protected CharacterControllerModules modules;

        public CharacterControllerModuleStateID StateData { get; }

        public int ID => StateData.ID;
        public string Name => StateData.Name;

        public CharacterControllerState(CharacterControllerModuleStateID stateData, CharacterControllerModules modules)
            : this(null, stateData, modules)
        {
        }

        public CharacterControllerState(CharacterControllerStateMachine stateMachine, CharacterControllerModuleStateID stateData, CharacterControllerModules modules)
        {
            this.stateMachine = stateMachine;
            this.StateData = stateData;
            this.modules = modules;
        }

        public void SetStateMachine(CharacterControllerStateMachine stateMachine)
        {
            this.stateMachine = stateMachine;
        }

        protected void ChangeState(CharacterControllerModuleStateID stateID, CharacterControllerModuleInput inputData)
        {
            stateMachine.ChangeState(stateID, inputData);
        }

        protected void PlayAnimation(string animationName)
        {
            //PlayAnimation(animationController, animationName);
            Debug.Log($"Playing animation: '{animationName}'");
        }

        protected void PlayAnimation(int animationIndex)
        {
            //PlayAnimation(animationController, animationIndex);
        }

        protected void PlayAnimation(AnimationController animationController, string animationName)
        {
            //animationController.Play(animationName);
        }

        protected void PlayAnimation(AnimationController animationController, int animationIndex)
        {
            //animationController.Play(animationIndex);
        }


        protected void CrossFadeAnimation(string animationName, float fadeLength = .15f)
        {
            //CrossFadeAnimation(animationController, animationName, fadeLength);
        }

        protected void CrossFadeAnimation(int animationIndex, float fadeLength = .15f)
        {
            //CrossFadeAnimation(animationController, animationIndex, fadeLength);
        }

        protected void CrossFadeAnimation(AnimationController animationController, string animationName, float fadeLength = .15f)
        {
            //animationController.CrossFade(animationName, fadeLength);
        }

        protected void CrossFadeAnimation(AnimationController animationController, int animationIndex, float fadeLength = .15f)
        {
            //animationController.CrossFade(animationIndex, fadeLength);
        }


        protected void StopAnimation(AnimationController animationController)
        {
            //animationController.Stop();
        }


        protected void Move(CharacterController characterController, Vector3 direction, float speed, float deltaTime)
        {
            characterController.Move(direction * speed * deltaTime);
        }

        protected void SmoothLookAt(Transform observer, Vector3 direction, float speed)
        {
            if (direction != Vector3.zero)
            {
                Quaternion targetRotation = Quaternion.LookRotation(direction);
                observer.rotation = Quaternion.Slerp(observer.rotation, targetRotation, speed * Time.deltaTime);
            }
        }
    }
}
