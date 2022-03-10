using System;
using UnityEngine;

namespace PlayphoriaTest.Control.Character
{
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(CharacterCollisionDetector))]
    [RequireComponent(typeof(RagdollHelper))]
    public class CharacterDeath : MonoBehaviour
    {
        [SerializeField] private Rigidbody rb;
        [SerializeField] private CharacterCollisionDetector characterCollisionDetector;
        [SerializeField] private RagdollHelper ragdollHelper;
        [SerializeField] private float deathHitForce = 50;
        
        private void OnValidate()
        {
            rb ??= GetComponent<Rigidbody>();
            characterCollisionDetector ??= GetComponent<CharacterCollisionDetector>();
            ragdollHelper ??= GetComponent<RagdollHelper>();
        }

        public void Handle()
        {
            ragdollHelper.ragdolled = true;
            rb.AddForce(characterCollisionDetector.LastBulletHitDirection * deathHitForce, ForceMode.VelocityChange);
        }
    }
}