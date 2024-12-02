using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameOverScreen : MonoBehaviour
{
    public BoatGUI boatGUI;

    public TMP_Text KillCount;
    public TMP_Text LevelNumber;
    public TMP_Text TerrainType;

 
    public void SetUp()
    {
        Debug.Log("GameOverScreen activated");
        gameObject.SetActive(true);
        KillCount.text = boatGUI.Killed.text;
        LevelNumber.text = boatGUI.cLevel.text;
        TerrainType.text = boatGUI.tLevel.text;
        Debug.Log($"KillCount: {KillCount.text}, LevelNumber: {LevelNumber.text}, TerrainType: {TerrainType.text}");
    }

    public void RestartButton()
    {
        // Reset scene and ensure GameOver screen is deactivated
        gameObject.SetActive(false);
        SceneManager.LoadScene("Boundless Vessel");
    }
}
