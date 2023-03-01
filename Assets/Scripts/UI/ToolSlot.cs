using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ToolSlot : MonoBehaviour
{
    TMP_Text textMesh;
    private void Awake()
    {
        textMesh = GetComponentInChildren<TMP_Text>();
    }
    public void Init(int index)
    {
        textMesh.text = (index + 1).ToString();
    }
}
