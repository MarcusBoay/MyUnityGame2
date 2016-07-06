using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Pooler : MonoBehaviour
{

    List<GameObject> cubePool = new List<GameObject>();

    public GameObject cube;
    // Use this for initialization
    void Start()
    {
        for (int numCubes = 0; numCubes < 13; numCubes++)
        {
            GameObject obj = (GameObject)Instantiate(cube);
            obj.SetActive(false);
            cubePool.Add(obj);
        }
    }
    GameObject[] cubesActiveInScene;
    int indexForUpdate;
    bool exec;
    float randomX;
    // Update is called once per frame
    void Update()
    {
        cubesActiveInScene = GameObject.FindGameObjectsWithTag("CubeTag");

        int numberOfActives = 0;
        foreach (GameObject cube in cubesActiveInScene)
        {
            numberOfActives++;
        }
        if (Input.GetMouseButtonDown(0))
        {
            indexForUpdate = takeAction();
        }
        else if (Input.GetKeyDown(KeyCode.Space) && numberOfActives > 0)
        {
            deactivate();
        }
        for (int v = 0; v < 13; v++)
        {
            if (numberOfActives < 13 && cubePool[v].activeInHierarchy)
            {
                if (!exec)
                {
                    randomX = randX();
                }
                Vector3 pos = cubePool[v].transform.position;
                pos.x += randomX;
                pos.z += 2;
                cubePool[v].transform.position = Vector3.Lerp(cubePool[v].transform.position,
                         pos, 0.09f);

            }
        }
    }

    int takeAction()
    {
        int index = 0;
        while (cubePool[index].activeInHierarchy)
        {
            index++;
        }
        cubePool[index].SetActive(true);
        return index;
    }

    void deactivate()
    {
        int index = 0;
        while (!cubePool[index].activeInHierarchy)
        {
            index++;
        }
        cubePool[index].transform.position = new Vector3(0, 0, 0);
        cubePool[index].SetActive(false);
    }

    float randX()
    {
        exec = true;
        return Random.Range(-15.0f, 15.0f);
    }
}
