using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrayShuffle {

  public static T[] Shuffle<T>(T[] array) {
    List<T> orig = new List<T>(array);
    List<T> dest = new List<T>();
    for (int i = 0; i < array.Length; i++) {
      int index = UnityEngine.Random.Range(0, orig.Count);
      dest.Add(orig[index]);
      orig.RemoveAt(index);
    }

    return dest.ToArray();
  }

}