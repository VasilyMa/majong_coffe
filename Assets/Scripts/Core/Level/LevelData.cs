using UnityEngine;

[CreateAssetMenu(fileName = "LevelData", menuName = "Scriptable Objects/LevelData")]
public class LevelData : ScriptableObject
{
    [Header("������ ����� (x, y) � ���� (z)")]
    public Vector2Int gridSize = new Vector2Int(10, 8); // x,y
    [Range(1, 10)] public int layers = 3;               // z (0..layers-1)

    [Header("�������")]
    [Min(1)] public int pairsCount = 24;                // ����� ����� ���
    [Min(1)] public int tileTypes = 12;                 // ���������� �����
    public Sprite[] tileSpritesPool;                    // ����� ��� �������� (>= tileTypes)

    [Header("����������� ���� ���� Z=0")]
    // �����/���������� = ���������, ������ = ���������; ������ ����������� ��� gridSize
    public Texture2D allowedMask;

    [Header("���������/����� ���������")]
    [Tooltip("0 � �� ��������, 1 � ����� ������� ���������� (������� ������)")]
    [Range(0f, 1f)] public float blockiness = 0.55f;

    [Tooltip("�����������, ��� ������ ���� ���� ����� ��������� ����� (��������� ���������� � ������)")]
    [Range(0f, 1f)] public float pairNearness = 0.75f;

    [Tooltip("�������� ������� ��� ������ �������/���������")]
    public int maxTries = 2000;

    // ��������� bool[,] �� allowedMask. ���� mask ��� � ��� ��������� ���������.
    public bool[,] BuildAllowedZone()
    {
        var zone = new bool[gridSize.x, gridSize.y];
        if (allowedMask == null)
        {
            for (int x = 0; x < gridSize.x; x++)
                for (int y = 0; y < gridSize.y; y++)
                    zone[x, y] = true;
            return zone;
        }

        for (int x = 0; x < gridSize.x; x++)
        {
            for (int y = 0; y < gridSize.y; y++)
            {
                // ������� ����� � ������������� �����������
                float u = (x + 0.5f) / gridSize.x;
                float v = (y + 0.5f) / gridSize.y;
                var c = allowedMask.GetPixelBilinear(u, v);
                zone[x, y] = c.grayscale > 0.5f || c.a < 0.5f;
            }
        }
        return zone;
    }
}
