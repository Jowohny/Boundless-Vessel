using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BoatGUI : MonoBehaviour
{
    public TMP_Text Killed; // The text component for kill count
    public TMP_Text cLevel; // The text component for current level
    public TMP_Text tLevel; // The text component for target level
    private int KillCount = 0;
    private string currentTriggerName = "";
    private string newTriggerName = "";

    void Start()
    {
        Killed.text = "0";
        cLevel.text = "1";
        tLevel.text = "Ocean";
    }

    public void AddKill()
    {
        KillCount++;
        Killed.text = KillCount.ToString();
    }

    public void Assign(string name)
    {
        newTriggerName = name;
    }

    void Update()
    {
        // Only update the level text if the trigger changes
        if (newTriggerName != currentTriggerName)
        {
            currentTriggerName = newTriggerName;
            LevelTransition(currentTriggerName);
        }
    }

    private void LevelTransition(string name)
    {
        if (name == "TriggerLVL2")
        {
            cLevel.text = "2";
            tLevel.text = "Magma Zone";
        }
        else if (name == "TriggerLVL3")
        {
            cLevel.text = "3";
            tLevel.text = "Icy Mountain";
        }
    }
}
