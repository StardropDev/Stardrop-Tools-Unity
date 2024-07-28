using UnityEngine;

namespace StardropTools.CharacterControllers
{
    [System.Serializable]
    public class CharacterControllerModuleMovementData
    {
        [Header("Movement Settings")]
        public float walkSpeed = 3.0f;
        public float runSpeed = 7.0f;
        public float crouchSpeed = 1.0f;
        public float acceleration = 5.0f;
        public float deceleration = 5.0f;

        [Header("Jump Settings")]
        public float jumpForce = 5.0f;
        public float gravity = 9.8f;

        [Header("Rotation Settings")]
        public float rotationSpeed = 16.0f;

        [Header("Ground Check Settings")]
        public LayerMask groundLayer;
        public float groundCheckDistance = 0.2f;

        [Header("Slope Settings")]
        public float maxSlopeAngle = 45.0f;

        [Header("Air Control Settings")]
        public float airControlMultiplier = 0.5f;

        [Header("Stamina Settings")]
        public float maxStamina = 100.0f;
        public float staminaConsumptionRate = 10.0f;
        public float staminaRecoveryRate = 5.0f;
        public float staminaRecoveryDelay = 2.0f;
    }
}
