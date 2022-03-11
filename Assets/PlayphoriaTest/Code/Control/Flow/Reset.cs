using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityUtils.Variables;

namespace PlayphoriaTest.Control.Flow
{
    public class Reset : MonoBehaviour
    {
        [SerializeField] private Vector3ArrayVariable[] dataArray;
        [SerializeField] private SaveObstaclePositions saveObstaclePositions;

        public void ResetLevel()
        {
            saveObstaclePositions.gameObject.SetActive(false);
            foreach (var array in dataArray)
            {
                array.Value = Array.Empty<Vector3>();
            }
            SceneManager.LoadScene(0);
        }
    }
}
