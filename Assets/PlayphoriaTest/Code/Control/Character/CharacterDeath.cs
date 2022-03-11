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

        private void Awake() 
            => CharacterHealth.OnDeath += HandleDeath;

        private void OnDestroy() 
            => CharacterHealth.OnDeath -= HandleDeath;

        private void HandleDeath()
        {
            ragdollHelper.ragdolled = true;
            rb.AddForce(characterCollisionDetector.LastBulletHitDirection * deathHitForce, ForceMode.VelocityChange);
        }
    }
}