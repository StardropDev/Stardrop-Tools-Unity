
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
            modules.Components.animationController.PlayAnimation(animationName);
        }

        protected void PlayAnimation(int animationID)
        {
            Debug.Log($"Playing animation with ID: '{animationID}'");
            modules.Components.animationController.PlayAnimation(animationID);
        }

        protected void PlayAnimationByIndex(int animationIndex)
        {
            Debug.Log($"Playing animation at Index: '{animationIndex}'");
            modules.Components.animationController.PlayAnimationByIndex(animationIndex);
        }


        protected void CrossfadeAnimation(string animationName)
        {
            //PlayAnimation(animationController, animationName);
            Debug.Log($"Playing animation: '{animationName}'");
            modules.Components.animationController.CrosfadeAnimation(animationName);
        }

        protected void CrossfadeAnimation(int animationID)
        {
            Debug.Log($"Playing animation with ID: '{animationID}'");
            modules.Components.animationController.CrosfadeAnimation(animationID);
        }

        protected void CrossfadeAnimationByIndex(int animationIndex)
        {
            Debug.Log($"Playing animation at Index: '{animationIndex}'");
            modules.Components.animationController.CrosfadeAnimationByIndex(animationIndex);
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
