/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveEnumerator : MonoBehaviour {

   // public List<WaveObj> WaveInfo;
    public int numOfWaves;
    private int spot;

    public WaveEnumerator()
    {
        spot = -1;
    }

    //Required IEnumerator method, return our enumerator
    public IEnumerator GetEnumerator()
    {
        return (IEnumerator)this;
    }

    //Required IEnumerator method, line up next object and return true if exists
    public bool MoveNext()
    {
        spot++;
        return (spot < WaveInfo.Count);

    }
    ////Required IEnumerator method, reset the enumerator
    public void Reset()
    {
        spot = -1;
    }

    ////Required IEnumerator method, return current object
    public object Current
    {
        get {
            return WaveInfo[spot];
        }
        set {

        } //empty as changes are not allowed
    }
}

*/