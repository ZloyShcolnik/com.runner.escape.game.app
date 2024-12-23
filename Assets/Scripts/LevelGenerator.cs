using UnityEngine;
using System.Collections.Generic;

public class LevelGenerator : MonoBehaviour
{
    private int tileIndex;
    public GameObject[] tilePrefab; // Префаб тайла (куб)
    public int numberOfTiles = 10; // Количество тайлов на уровне
    public float tileLength = 15f; // Длина одного тайла
    public float levelSpeed = 5f; // Скорость движения тайлов

    private List<GameObject> tiles; // Список всех тайлов
    private Vector3 nextTilePosition; // Позиция следующего тайла

    void Start()
    {
        tiles = new List<GameObject>();
        nextTilePosition = Vector3.zero;

        // Генерация начальных тайлов
        for (int i = 0; i < numberOfTiles; i++)
        {
            CreateTile();
        }
    }

    void Update()
    {
        MoveTiles();
    }

    // Функция для создания нового тайла
    void CreateTile()
    {
        GameObject newTile = Instantiate(tilePrefab[tileIndex], nextTilePosition, Quaternion.identity);
        tiles.Add(newTile);
        tileIndex++;

        // Расставляем тайлы по оси Z
        nextTilePosition = new Vector3(0, 0, nextTilePosition.z + tileLength);
    }

    // Функция для перемещения тайлов
    void MoveTiles()
    {
        for (int i = 0; i < tiles.Count; i++)
        {
            GameObject tile = tiles[i];
            tile.transform.Translate(Vector3.back * levelSpeed * Time.deltaTime);

            // Если тайл выходит за пределы экрана, сбрасываем его в конец
            if (tile.transform.position.z < -tileLength)
            {
                // Перемещаем тайл в конец (после последнего)
                Vector3 newPosition = new Vector3(0, 0, tiles[tiles.Count - 1].transform.position.z + tileLength);
                tile.transform.position = newPosition;

                // Перемещаем тайл в конец списка
                tiles.RemoveAt(i);
                tiles.Add(tile);
            }
        }
    }
}