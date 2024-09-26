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
                    fullMap[i, fullMapSizeX-1] = 0;
                    continue;
                }
                if (j==0)
                {
                    fullMap[i, j] = 0;
                    fullMap[i, fullMapSizeX-1] = 0;
                    continue; 
                }
                fullMap[i, j] = levelMap[i - 1, j - 1];
                fullMap[i, fullMapSizeX-(j+1)] = levelMap[i - 1, j - 1];
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
        
        for (int i = 0; i < fullMap.GetLength(0); i++)
        {
            Debug.Log(fullMap[i,fullMap.GetLength(1)-1]);
        }
        /*
        Debug.Log(fullMap[1,fullMap.GetLength(1)]-1);
        Debug.Log(fullMap[fullMap.GetLength(0)-1,fullMap.GetLength(1)]-1);
        
        Debug.Log(fullMap[fullMap.GetLength(0)-1,0]);
        */
        
        //muss in funktion um clean zu sein
        for (int i = 0; i < fullMapSizeY; i++)
        {
            for (int j = 0; j < fullMapSizeX-1; j++)
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
                

                if (j>mapSizeX+1)
                {
                    x += 1f;
                }
                
                GameObject mapPart = Instantiate(GetObjFromPartNr(partNr));
                mapPart.transform.position = new Vector3(x, y, 0);
                mapPart.transform.Rotate(0, 0, findRotation(i, j));
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
    int findRotation(int x, int y)
    {
        switch (fullMap[x,y])
        {
            case 1:
                if (fullMap[x-1,y]!=1&&fullMap[x-1,y]!=2&&fullMap[x,y+1]!=1&&fullMap[x,y+1]!=2)
                {
                    return 0;
                }
                if (fullMap[x+1,y]!=1&&fullMap[x+1,y]!=2&&fullMap[x,y-1]!=1&&fullMap[x,y-1]!=2)
                {
                    return 180;
                }
                if (fullMap[x+1,y]!=1&&fullMap[x+1,y]!=2&&fullMap[x,y+1]!=1&&fullMap[x,y+1]!=2)
                {
                    return 270;
                }
                return 90;
            case 2:
                if (fullMap[x-1,y]==1||fullMap[x-1,y]==2)
                {
                    //falls links oder recht nicht 0/5/6 sind -> 0
                    //else -> 90
                    return 90;
                }
                return 0;
            case 3:
                if ((fullMap[x - 1, y] == 3 || fullMap[x - 1, y] == 4)&&
                    (fullMap[x, y + 1] == 3 || fullMap[x, y + 1] == 4))
                {
                    return 180;
                }
                if ((fullMap[x + 1, y] == 3 || fullMap[x + 1, y] == 4)&&(fullMap[x, y - 1] == 3 || fullMap[x, y - 1] == 4))
                {
                    return 0;
                }
                if (fullMap[x+1,y]==3||fullMap[x+1,y]==4&&fullMap[x,y+1]==3||fullMap[x,y+1]==4)
                {
                    return 90;
                }
                return 270;
            case 4:
                //falls links oder recht nicht 0/5/6 sind -> 0
                //else -> 90
                if ((fullMap[x - 1, y] == 3 || fullMap[x - 1, y] == 4|| fullMap[x - 1, y] == 7)&&(fullMap[x+1,y]==3||fullMap[x+1,y]==4||fullMap[x+1,y]==7))
                {
                    //falls links oder recht nicht 0/5/6 sind -> 0
                    //else -> 90
                    return 90;
                }
                return 0;
            case 7:
                return 0;
                
                return 0;
            default:
                return 0;
        }
    }
    
}
