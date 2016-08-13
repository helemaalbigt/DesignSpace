using UnityEngine;
using System.Collections;

[System.Serializable]
public struct BytesCounter  {

    public long bytes;
    public int bytesAdded;
    public float bytesPerSecond;
    public int secondPast;

    public void Add(int bytesNumber) {
        if(bytesNumber>0)
           bytesAdded += bytesNumber;
    }

    public void SecondHasPasted() {

        bytesPerSecond = ((3f * bytesPerSecond + bytesAdded)/4f /1000000f );

        bytes += bytesAdded;
        bytesAdded = 0;
        secondPast++;



    }

}
