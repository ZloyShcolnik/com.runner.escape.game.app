using UnityEngine;
using System.Collections.Generic;

public class LevelGenerator : MonoBehaviour
{
    private int tileIndex;
    public GameObject[] tilePrefab; // ������ ����� (���)
    public int numberOfTiles = 10; // ���������� ������ �� ������
    public float tileLength = 15f; // ����� ������ �����
    public float levelSpeed = 5f; // �������� �������� ������

    private List<GameObject> tiles; // ������ ���� ������
    private Vector3 nextTilePosition; // ������� ���������� �����

    void Start()
    {
        tiles = new List<GameObject>();
        nextTilePosition = Vector3.zero;

        // ��������� ��������� ������
        for (int i = 0; i < numberOfTiles; i++)
        {
            CreateTile();
        }
    }

    void Update()
    {
        MoveTiles();
    }

    // ������� ��� �������� ������ �����
    void CreateTile()
    {
        GameObject newTile = Instantiate(tilePrefab[tileIndex], nextTilePosition, Quaternion.identity);
        tiles.Add(newTile);
        tileIndex++;

        // ����������� ����� �� ��� Z
        nextTilePosition = new Vector3(0, 0, nextTilePosition.z + tileLength);
    }

    // ������� ��� ����������� ������
    void MoveTiles()
    {
        for (int i = 0; i < tiles.Count; i++)
        {
            GameObject tile = tiles[i];
            tile.transform.Translate(Vector3.back * levelSpeed * Time.deltaTime);

            // ���� ���� ������� �� ������� ������, ���������� ��� � �����
            if (tile.transform.position.z < -tileLength)
            {
                // ���������� ���� � ����� (����� ����������)
                Vector3 newPosition = new Vector3(0, 0, tiles[tiles.Count - 1].transform.position.z + tileLength);
                tile.transform.position = newPosition;

                // ���������� ���� � ����� ������
                tiles.RemoveAt(i);
                tiles.Add(tile);
            }
        }
    }
}