using PlayphoriaTest.Control.Character;
using UnityEngine;
using UnityEngine.UI;

namespace PlayphoriaTest.View.Character
{
    public class CharacterHealthDisplay : MonoBehaviour
    {
        [SerializeField] private CharacterHealth characterHealth;
        [SerializeField] private Slider slider;
        [SerializeField] private float sliderSpeed = 1f;
        
        private void OnValidate()
        {
            slider ??= GetComponent<Slider>();
        }

        private void Update()
        {
            slider.value = Mathf.Lerp(
                slider.value, 
                characterHealth.Health / characterHealth.baseHealth,
                sliderSpeed * Time.deltaTime);
        }
    }
}