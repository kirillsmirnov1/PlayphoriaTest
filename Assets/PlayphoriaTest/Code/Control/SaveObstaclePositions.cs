using System.Linq;
using UnityEngine;
using UnityUtils.Variables;

namespace PlayphoriaTest.Control
{
    public class SaveObstaclePositions : MonoBehaviour
    {
        [SerializeField] private Transform[] obstacles;
        [SerializeField] private Vector3ArrayVariable positions;
        [SerializeField] private Vector3ArrayVariable rotations;
        
        private void OnValidate()
        {
            GetObstacleTransforms();
        }

        private void GetObstacleTransforms()
        {
            obstacles = GetComponentsInChildren<Transform>().Where(t => t != transform).ToArray();
        }

        private void Start()
        {
            SetPositionsFromSave();
        }

        private void OnDestroy()
        {
            SavePositions();
        }

        private void SetPositionsFromSave()
        {
            var length = Mathf.Min(obstacles.Length, positions.Length);
            for (int i = 0; i < length; i++)
            {
                obstacles[i].position = positions[i];
                obstacles[i].rotation = Quaternion.Euler(rotations[i]);
            }
        }

        private void SavePositions()
        {
            positions.Value = obstacles.Select(t => t.position).ToArray();
            rotations.Value = obstacles.Select(t => t.rotation.eulerAngles).ToArray();
        }
    }
}
