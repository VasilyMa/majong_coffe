using System.Collections.Generic;
using UnityEngine; 

[CreateAssetMenu(fileName = "LevelData", menuName = "Scriptable Objects/LevelData")]
public class LevelData : ScriptableObject
{
    [Header("������ ����")]
    public int width = 5;
    public int height = 5;

    [Header("��������� ���������")]
    public int layersCount = 3;
    public int pairsCount = 10;
    public int tileTypesCount = 4;

    [Header("������� ���� (������ ���������)")]
    [HideInInspector] public List<int> baseLayer = new List<int>();
     
    private void OnValidate()
    {
        int targetSize = width * height;
        while (baseLayer.Count < targetSize)
            baseLayer.Add(0);
        while (baseLayer.Count > targetSize)
            baseLayer.RemoveAt(baseLayer.Count - 1);
    }
} 