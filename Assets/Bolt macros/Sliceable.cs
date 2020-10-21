using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sliceable : MonoBehaviour
{
    public List<GameObject> pieces;
    private bool[] sliced;
    private void Start()
    {
        sliced = new bool[pieces.Count];
        for (int i = 0; i < sliced.Length; i++)
        {
            sliced[i] = false;
        }
    }

    private int GetLastPieceIndex()
    {
        int i;
        for (i = -1; i < sliced.Length - 1; i++)
        {
            if (sliced[i + 1] == true)
            {
                break;
            }
        }
        return i;
    }

    public Vector2 GetNextSlicePos()
    {
        int ind = GetLastPieceIndex();
        return pieces[ind].transform.position + (Vector3.left * (pieces[ind].GetComponent<SpriteRenderer>().bounds.size.x) / 2);
    }
    public bool Slice() //to do, returns whether or not an obj has been sliced fully
    {
        int ind = GetLastPieceIndex();
        pieces[ind].GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(0.2f, 0.8f), Random.Range(-0.3f, 0.3f)), ForceMode2D.Impulse);
        sliced[ind] = true;
        Debug.Log("PSDIGJIOSDGJIOSDGIOSEGJOJ");
        return GetLastPieceIndex() <= 0;
    }
}
