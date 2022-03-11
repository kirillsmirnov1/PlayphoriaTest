using System.Linq;
using UnityEngine;
using UnityUtils.Variables;

namespace PlayphoriaTest.Control.Flow
{
    public class SaveObstaclePositions : MonoBehaviour
    {
        [SerializeField] private Transform[] obstacles;
        [SerializeField] private Vector3ArrayVariable positions;
        [SerializeField] private Vector3ArrayVariable rotations;

#if UNITY_WEBGL
        [SerializeField] private float saveDelay = .5f;
        private float _nextSave;
#endif
        
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
        
        private void Update()
        {
#if UNITY_WEBGL
            DelayedSave();
#endif
        }

#if UNITY_WEBGL
        private void DelayedSave()
        {
            if (Time.time < _nextSave) return;
            _nextSave = Time.time + saveDelay;
            SavePositions();
        }
#endif

        private void OnDestroy()
        {
            SavePositions();
        }

        private void SetPositionsFromSave()
        {
            var length = Mathf.Min(obstacles.Length, positions.Length);
            for (int i = 0; i < length; i++)
            {
                obstacles[i].SetPositionAndRotation(positions[i], Quaternion.Euler(rotations[i]));
            }
        }

        private void SavePositions()
        {
            positions.Value = obstacles.Select(t => t.position).ToArray();
            rotations.Value = obstacles.Select(t => t.rotation.eulerAngles).ToArray();
        }
    }
}
