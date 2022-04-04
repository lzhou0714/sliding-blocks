using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using TMPro.EditorUtilities;
using UnityEngine;
using UnityEngine.Tilemaps;
using Debug = UnityEngine.Debug;

public class GameScript : MonoBehaviour
{
    [SerializeField] private Transform emptySpace ;
    private Camera _camera;
    private int emptySpaceIndex = 15;
    [SerializeField] private TilesScript[] tiles;
    

    // Start is called before the first frame update
    void Start() 
    {
        
        _camera = Camera.main;
        // Shuffle();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) //if layer clicks left buttton
        {
            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);
            if (hit)
            {
                Debug.Log(Vector2.Distance(emptySpace.position, hit.transform.position));

                //if distance betwene the empty space and hit is less than 2
                if (Vector2.Distance(emptySpace.position, hit.transform.position) < 5)
                {
                    Vector2 lastEmptySpacePosition = emptySpace.position;
                    
                    //tile being clicked on
                    TilesScript thisTile = hit.transform.GetComponent<TilesScript>();
                    emptySpace.position = thisTile.targetPosition;
                    thisTile.targetPosition = lastEmptySpacePosition;
                    int tileIndex = findIndex(thisTile);
                    Debug.Log("tile index" + tileIndex + "emptySpace Index" + emptySpaceIndex);

                    tiles[emptySpaceIndex] = tiles[tileIndex];
                    Debug.Log("tile index" + tileIndex + "emptySpace Index" + emptySpaceIndex);

                    tiles[tileIndex] = null;
                    Debug.Log("tile index" + tileIndex + "emptySpace Index" + emptySpaceIndex);

                    emptySpaceIndex = tileIndex;

                }
                
            }
        }
    }
    public void Shuffle()
    {
        if (emptySpaceIndex != 15)
        {
            var tileOn15LastPos = tiles[15].targetPosition;
            tiles[15].targetPosition = emptySpace.position;
            emptySpace.position = tileOn15LastPos;
            tiles[emptySpaceIndex] = tiles[15];
            tiles[15] = null;
            emptySpaceIndex = 15;
        }

        int invertion;
        do
        {
            for (int i = 0; i < 15; i++)
            {
                var lastPos = tiles[i].targetPosition;
                int randomIndex = Random.Range(0, 14);
                tiles[i].targetPosition = tiles[randomIndex].targetPosition;
                tiles[randomIndex].targetPosition = lastPos;

                //change index of tiles in array according to their positio on the boards
                var tile = tiles[i];
                tiles[i] = tiles[randomIndex];
                tiles[randomIndex] = tile;
            }

            invertion = GetInversions();
            Debug.Log("puzzle shuffled");
        } while (invertion % 2 != 0);


    }

    public int findIndex(TilesScript ts)
    {
        for (int i = 0; i < tiles.Length; i++)
        {
            if (tiles[i] != null)
            {
                if (tiles[i] == ts)
                {
                    return i;
                }
            }
        }

        return -1;
    }

    int GetInversions()
    {
        var inversionsSum = 0;
        for (int i = 0; i < tiles.Length; i++)
        {
            int thisTileInvertion = 0;
            for (int j = i; j < tiles.Length; j++)
            {
                if (tiles[j] != null)
                {
                    if (tiles[i].number > tiles[j].number)
                    {
                        thisTileInvertion++;
                    }
                }
            }
            inversionsSum += thisTileInvertion;
        }

        return inversionsSum;

    }
}
