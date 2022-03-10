using PlayphoriaTest.Control;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityUtils.Saves;

public class Reset : MonoBehaviour
{
    [SerializeField] private SaveFile saveFile;
    [SerializeField] private SaveObstaclePositions saveObstaclePositions;

    public void ResetLevel()
    {
        saveObstaclePositions.gameObject.SetActive(false);
        saveFile.ResetToDefaults();
        SceneManager.LoadScene(0);
    }
}
