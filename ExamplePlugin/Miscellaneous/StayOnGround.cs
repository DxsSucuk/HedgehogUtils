using RoR2;
using UnityEngine;

namespace HedgehogUtils.Miscellaneous
{
    [RequireComponent(typeof(CharacterMotor))]
    public class StayOnGround : MonoBehaviour
    {
        protected CharacterMotor characterMotor;

        public float strength = 7f;

        private void Awake()
        {
            characterMotor = base.GetComponent<CharacterMotor>();
        }

        private void Update()
        {
            if (characterMotor.isGrounded)
            {
                Vector3 horizontal = characterMotor.velocity;
                horizontal.y = 0;
                horizontal = horizontal.normalized;
                characterMotor.AddDisplacement((1 + Vector3.Dot(Vector3.ProjectOnPlane(horizontal, characterMotor.estimatedGroundNormal).normalized, Vector3.down)) * Time.deltaTime * strength * Vector3.down);
            }
        }
    }
}