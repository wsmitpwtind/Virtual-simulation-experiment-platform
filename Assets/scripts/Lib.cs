using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MATH = System.Math;
public static class StaticMethods {
    public static void cbj0() {
        Debug.Log("test");
    }   
    public static double Uncertain_A(double[] input) {
        double s1 = 0.0, s2 = 0.0;
        int length = input.Length;
        for(int i = 0; i < length; i++) {
            s1 += input[i];
            s2 += input[i] * input[i];
        }
        return MATH.Sqrt((s2 - s1) / (length - 1));
    }
    public static double Average(double[] input) {
        double s1 = 0.0;
        int length = input.Length;
        for(int i = 0; i < length; i++) {
            s1 += input[i];
        }
        return s1 / length;
    }
}


