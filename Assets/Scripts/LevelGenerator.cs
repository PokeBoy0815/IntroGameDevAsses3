using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing;

public class LevelGenerator : MonoBehaviour
{
    [SerializeField] private GameObject level;
    [SerializeField] private Camera camera;

    [SerializeField] private GameObject tjun;
    [SerializeField] private GameObject outWall;
    [SerializeField] private GameObject outCorner;
    [SerializeField] private GameObject inWall;
    [SerializeField] private GameObject inCorner;
    [SerializeField] private GameObject pellet;
    [SerializeField] private GameObject powerPellet;

    private int mapSizeX;
    private int mapSizeY;

    private int fullMapSizeX;
    private int fullMapSizeY;
    
    
    int[,] levelMap = {
        {1,2,2,2,2,2,2,2,2,2,2,2,2,7},
        {2,5,5,5,5,5,5,5,5,5,5,5,5,4},
        {2,5,3,4,4,3,5,3,4,4,4,3,5,4},
        {2,6,4,0,0,4,5,4,0,0,0,4,5,4},
        {2,5,3,4,4,3,5,3,4,4,4,3,5,3},
        {2,5,5,5,5,5,5,5,5,5,5,5,5,5},
        {2,5,3,4,4,3,5,3,3,5,3,4,4,4},
        {2,5,3,4,4,3,5,4,4,5,3,4,4,3},
        {2,5,5,5,5,5,5,4,4,5,5,5,5,4},
        {1,2,2,2,2,1,5,4,3,4,4,3,0,4},
        {0,0,0,0,0,2,5,4,3,4,4,3,0,3},
        {0,0,0,0,0,2,5,4,4,0,0,0,0,0},
        {0,0,0,0,0,2,5,4,4,0,3,4,4,0},
        {2,2,2,2,2,1,5,3,3,0,4,0,0,0},
        {0,0,0,0,0,0,5,0,0,0,4,0,0,0},
    };

    private int[,] fullMap;
    void Start()
    {
        for (int i = 0; i < level.transform.childCount; i++)
        {
            Destroy(level.transform.GetChild(i).gameObject);
        }

        mapSizeX = levelMap.GetLength(1);
        mapSizeY = levelMap.GetLength(0);
        fullMapSizeX = mapSizeX * 2 + 2;
        fullMapSizeY = mapSizeY * 2 + 1;
        
        Debug.Log(mapSizeY);
        camera.orthographicSize = mapSizeY;
        Debug.Log(mapSizeX);

        
        //need to create new array, size= [2times-1+2,2times+2]
        //0 all around
        fullMap = new int[fullMapSizeY, fullMapSizeX];
        
        //mirror horizontally
        for (int i = 0; i <= mapSizeY; i++)
        {
            
            for (int j = 0; j <= mapSizeX; j++)
            {
                if (i==0)
                {
                    fullMap[i, j] = 0;
                    continue;
                }
                if (j==0)
                {
                    fullMap[i, j] = 0;
                    continue; 
                }
                fullMap[i, j] = levelMap[i - 1, j - 1];
                fullMap[i, fullMapSizeX-j] = levelMap[i - 1, j - 1];
            }
        }
        //mirror vertically
        for (int i = 0; i < mapSizeY; i++)
        {
            for (int j = 0; j <= mapSizeX; j++)
            {
                fullMap[fullMapSizeY - (i+1), j] = fullMap[i, j];
                //Debug.Log("second half begins");
                fullMap[fullMapSizeY - (i+1), fullMapSizeX-(j+1)] = fullMap[i, j];
                //Debug.Log("second half begins");
            }
        }
        
        Debug.Log(fullMap);
        
        //muss in funktion um clean zu sein
        for (int i = 0; i < fullMapSizeY; i++)
        {
            for (int j = 0; j < fullMapSizeX; j++)
            {
                int partNr = fullMap[i,j];
                if (partNr == 0)
                {
                    continue;
                }
                
                float x = (j - mapSizeX);
                float y = (mapSizeY - i);
                if (j>mapSizeX+1)
                {
                    x -= 1.5f;
                    y += 0.5f;
                }    
                if (j<=mapSizeX+1)
                {
                    x -= 0.5f;
                    y += 0.5f;
                }  
                if (j>mapSizeX+1&&i>mapSizeY)
                {
                    x += 1f;
                } 
                
                GameObject mapPart = Instantiate(GetObjFromPartNr(partNr));
                mapPart.transform.position = new Vector3(x, y, 0);
                mapPart.transform.parent = level.transform;


                //Debug.Log(place);
            }
        }
        //bis hier damit man es dann auhc auf die reversten arrays anwenden kann
        
    }

    GameObject GetObjFromPartNr(int partNr)
    {
        switch (partNr)
        {
            case 1:
                return outCorner;
            case 2:
                return outWall;
            case 3:
                return inCorner;
            case 4:
                return inWall;
            case 5:
                return pellet;
            case 6:
                return powerPellet;
            case 7:
                return tjun;
            default:
                return pellet;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    //this will be the function that determines the rotation of the pieces, switch case for ey kind of piece
    int findRotation(int x, int y, int[][] arr)
    {
        switch (arr[x][y])
        {
            case 1:
                return 1;
            case 2:
                //falls links oder recht nicht 0/5/6 sind -> 0
                //else -> 90
                return 2;
            case 3:
                return 1;
            case 4:
                //falls links oder recht nicht 0/5/6 sind -> 0
                //else -> 90
                return 1;
            case 7:
                return 2;
            default:
                return 0;
        }
        return 0;
    }
    
}
