using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEditor.AnimatedValues;
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
    public static double Average(IEnumerable<double> input) {
        double s1 = 0.0;
        int n = 0;
        foreach(var item in input) {
            s1 += item;
            n++;
        }
        return s1 / n;
    }

    public static int GCD(int a, int b) {
        return a % b == 0 ? b : GCD(b, a % b);
    }
    public static int LCM(int a, int b) {
        return a * b / GCD(a, b);
    }
    public static double Variance(IEnumerable<double> nums) {
        double s = 0, av = Average(nums); int n = 0;
        foreach(double i in nums) {
            n++; s += (i - av) * (i - av);
        }
        return s / n;
    }
    public static double Covariance(double[] X, double[] Y) {
        if(X == null || Y == null || X.Length != Y.Length) {
            throw new UnityException("数组长度不统一");
        }
        double s = 0.0, sx = 0.0, sy = 0.0;
        int n = X.Length;
        for(int i = 0; i < n; i++) {
            s += X[i] * Y[i];
            sx += X[i];
            sy += Y[i];
        }
        return (s / n) - (sx / n) * (sy / n);
    }
    public static double[] LinearFit(double[] X, double[] Y) {
        if(X == null || Y == null || X.Length != Y.Length) {
            return null;
        }
        int n = X.Length;
        double xav = Average(X), yav = Average(Y), xyav = 0.0, x2av = 0.0, y2av = 0.0, u = 0.0;
        for(int i = 0; i < n; i++) {
            xyav += X[i] * Y[i];
            x2av += X[i] * X[i];
            y2av += Y[i] * Y[i];
        }
        xyav /= n; x2av /= n;
        double a, b, r;
        b = (xav * yav - xyav) / (xav * xav - x2av);
        a = yav - b * xav;
        r = (xyav - xav * yav) / MATH.Sqrt((x2av - xav * xav) * (y2av - yav * yav));
        for(int i = 0; i < n; i++) {
            u += (Y[i] - (a + b * X[i])) * (Y[i] - (a + b * X[i]));
        }
        return new double[] { b, a, r, MATH.Sqrt(u / (n - 2)) };
    }
    public static double[][] SuccessionalDifference(double[] X, double[] Y) {
        if(X == null || Y == null || X.Length != Y.Length) {
            return null;
        }
        int k = X.Length;
        int n = k / 2;
        int dif = k % 2 == 0 ? n : n + 1;
        double[] bi = new double[n];
        for(int i = 0; i < n; i++) {
            bi[i] = (Y[i + dif] - Y[i]) / (X[i + dif] - X[i]);
        }
        double bar = Average(bi), xav = Average(X), yav = Average(Y);
        return new double[][] { new double[] { bar, yav - xav * bar, xav, yav, Uncertain_A(bi) / n }, bi };
    }
}
public static class InstrumentError {
    public const double time_error = 5.8e-6 + 0.01;
    public const double steel_ruler_simp = 0.5;
    public const double steel_tape_simp = 0.5;
    public const double micrometer_simp = 0.005;
    public const double time_error_simp = 0.01;
    public static double dc_potentiometer(double a, double Ux, double U0) {
        return 0.01 * a * (Ux + U0 / 10.0);
    }
    public static double dc_bridge(double a, double Rx, double R0) {
        return 0.01 * a * (Rx + R0 / 10.0);
    }
    public static double digital_instrument(double a, double Nx, Dictionary<string, double> kwargs) {
        if(kwargs.ContainsKey("b") && kwargs.ContainsKey("Nm")) {
            return 0.01 * (a * Nx + kwargs["b"] * kwargs["Nm"]);
        }
        else if(kwargs.ContainsKey("n") && kwargs.ContainsKey("scale")) {
            return 0.01 * a * Nx + kwargs["n"] * kwargs["scale"];
        }
        throw new UnityException("输入格式有误:)");
    }
    public static double steel_ruler(double length) {
        if(length < 0.0) throw new UnityException("长度不能为负数:)");
        else if(length <= 300.0) return 0.10;
        else if(length <= 500.0) return 0.15;
        else if(length <= 1000.0) return 0.20;
        else if(length <= 1500.0) return 0.27;
        else if(length <= 2000.0) return 0.35;
        else throw new UnityException("方法未定义:)");
    }
    public static double steel_tape(int level, double length) {
        switch(level) {
            case 1:
                return 0.1 + 0.001 * length;
            case 2:
                return 0.3 + 0.002 * length;
            default:
                throw new UnityException(":)");
        }
    }
    public static double caliper(double division, double length) {
        if(length < 0.0) throw new UnityException("长度必须>=0");
        switch(division) {
            case 0.02:
                if(length <= 150) return 0.02;
                else if(length <= 200) return 0.03;
                else if(length <= 300) return 0.04;
                else if(length <= 500) return 0.05;
                else if(length <= 1000) return 0.07;
                else throw new UnityException("不支持>=1000的");
            case 0.05:
                if(length <= 200) return 0.05;
                else if(length <= 500) return 0.08;
                else if(length <= 1000) return 0.1;
                else throw new UnityException("不支持>=1000的");
            case 0.1:
                if(length <= 500) return 0.10;
                else if(length <= 1000) return 0.15;
                else throw new UnityException("不支持>=1000的");
            default:
                throw new UnityException("分度值不支持");
        }
    }
    public static double micrometer(double length) {
        if(length < 0) throw new UnityException("长度必须>=0");
        else if(length <= 50.0) return 0.004;
        else if(length <= 100.0) return 0.005;
        else if(length <= 150.0) return 0.006;
        else if(length <= 200.0) return 0.007;
        else throw new UnityException("不支持 >= 200的");
    }
    public static double electromagnetic_instrument(double a, double Nm) {
        return a * Nm / 100.0;
    }
    public static double dc_resistor(double r20, double a, double b, double t) {
        return r20 * (1 + a * (t - 20.0) + b * (t - 20.0) * (t - 20.0));
    }
    public static double resistance_box(double[] a, double[] r, double r0) {
        double s = 0;
        for(int i = 0; i < a.Length; i++) {
            s += (a[i] * r[i] / 100);
        }
        return s += r0;
    }
    public static double full_immersion_mercury_thermometer(double t, double division) {
        if(-30.0 <= t && t <= 100.0) {
            switch(division) {
                case 0.1: return 0.2;
                case 0.2: return 0.3;
                case 0.5: return 0.5;
                case 1.0: return 1.0;
                default: break;
            }
        }
        else if(100.0 < t && t <= 200.0) {
            switch(division) {
                case 0.1: return 0.4;
                case 0.2: return 0.4;
                case 0.5: return 1.0;
                case 1.0: return 1.5;
                default: break;
            }
        }
        throw new UnityException("不支持的操作");
    }
    public static double mercury_thermometer(double t, double division) {
        if(-30.0 <= t && t <= 100.0) {
            switch(division) {
                case 0.5: return 1.0;
                case 1.0: return 1.5;
                default: break;
            }
        }
        else if(100.0 < t && t <= 200.0) {
            switch(division) {
                case 0.5: return 1.5;
                case 1.0: return 2.0;
                default: break;
            }
        }
        throw new UnityException("不支持的操作");
    }
    public static double Pt_Rh_couple(int level, double t) {
        switch(level) {
            case 1:
                if(0 <= t && t <= 1100) return 1;
                else if(1100 < t && t <= 1600) return 1 + 0.03 * (t - 1100);
                else throw new UnityException("不支持的操作");
            case 2:
                if(0 <= t && t <= 600) return 1.5;
                else if(600 < t && t <= 1600) return 0.0025 * t;
                else throw new UnityException("不支持的操作");
            default:
                throw new UnityException("不支持的操作");
        }
    }
    public static double Pt_resistance(char level, double t) {
        switch(level) {
            case 'A':
                return 0.15 + 0.002 * MATH.Abs(t);
            case 'B':
                return 0.30 + 0.005 * MATH.Abs(t);
            default:
                throw new UnityException("不支持的操作");
        }
    }
}


