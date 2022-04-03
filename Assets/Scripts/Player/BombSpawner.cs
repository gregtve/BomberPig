using UnityEngine;
using UnityEngine.Tilemaps;

public class BombSpawner : MonoBehaviour
{
    [SerializeField] private Tilemap tilemap;
    [SerializeField] private GameObject bomb;

    private int bombCapacity;
    private int bombCount;

    private int explosionLength;

    private bool bombButtonPressed = false;

    private void Start()
    {
        bombCapacity = GameManager.Instance.bombCapacity;
        explosionLength = GameManager.Instance.explosionLength;
        bombCount = bombCapacity;
    }

    public void BombButtonPressed()
    {
        bombButtonPressed = true;
        Debug.Log("PRESSED BOMB");
    }

    private void Update()
    {
        if ((bombCount > 0) && bombButtonPressed)//Input.GetKeyDown(KeyCode.Space))
        {
            Vector3Int cell = tilemap.WorldToCell(transform.position);
            Debug.Log("cell" + cell);
            Vector3 cellCentrePosition = tilemap.GetCellCenterWorld(cell);
            Debug.Log("cellCentrePosition" + cellCentrePosition);

            SpawnBomb(cellCentrePosition); 
           
            bombCount -= 1;
            bombButtonPressed = false;///////////////
        }
    }

    private void SpawnBomb(Vector3 cellCentrePosition)
    {
        var bombGO = Instantiate(bomb, cellCentrePosition, Quaternion.identity) as GameObject;
        bombGO.GetComponent<Bomb>().ExplosionLength = explosionLength;
    }

    public void IncrementBombCapacity()
    {
        bombCapacity++;
        bombCount++;
        GameManager.Instance.bombCapacity++;
    }

    public void IncrementBombCount()
    {
        bombCount++;
    }

    public void IncrementExplosionLength()
    {
        explosionLength++;
        GameManager.Instance.explosionLength++;
    }
}
