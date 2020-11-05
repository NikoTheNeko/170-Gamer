using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PourScript : MonoBehaviour
{
    public GameObject waterParticle;
    public double cutoffHeight;
    public Vector3 bottomLeft;
    private List<GameObject> waterList;
    public bool finished = false;
    // Start is called before the first frame update
    void Start()
    {
        waterList = new List<GameObject>();
        float xRestraintLow = (float)(bottomLeft.x + .5);
        float xRestraintHigh = (float)(bottomLeft.x + 4);

        float yRestraintLow = (float)(bottomLeft.y + .5);
        float yRestraintHigh = (float)(bottomLeft.y + 4);

        for(int i = 0; i < 500; i++)
        {
            GameObject newWater = Instantiate(waterParticle);
            newWater.name = (string.Format("Particle {0}", i));
            float randX = Random.Range(xRestraintLow, xRestraintHigh);
            float randY = Random.Range(yRestraintLow, yRestraintHigh);
            newWater.transform.position = new Vector3(randX, randY, 0f);
            waterList.Add(newWater);
        }
    }

    // Update is called once per frame
    void Update()
    {
        int fallen = 0;
        for (int i = 0; i < 500; i++)
        {
            if(waterList[i].transform.position.y < cutoffHeight)
            {
                fallen++;
            }
        }
        if(fallen > 450)
        {
            finished = true;
        }
        fallen = 0;
    }
}
