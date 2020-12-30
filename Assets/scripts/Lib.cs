using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Xml.Serialization;
using UnityEngine;
using MATH = System.Math;
using System.Text;
using Newtonsoft.Json;
using UnityEngine.UIElements;
using UnityEditor;
using OBJECT = UnityEngine.Object;
using System.Text.RegularExpressions;
using System.Linq;

public static class StaticMethods {
    public static void cbj0() {
        Debug.Log("test");
    }
    public static double Uncertain_A(double[] input) {
        //A类不确定度
        double s1 = 0.0, s2 = 0.0;
        int length = input.Length;
        for(int i = 0;i < length;i++) {
            s1 += input[i];
            s2 += input[i] * input[i];
        }
        s2 /= length;
        s1 /= length;
        s1 = s1 * s1;
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
        //方差
        double s = 0, av = Average(nums); int n = 0;
        foreach(double i in nums) {
            n++; s += (i - av) * (i - av);
        }
        return s / n;
    }
    public static double Covariance(double[] X, double[] Y) {
        //协方差
        if(X == null || Y == null || X.Length != Y.Length) {
            throw new UnityException("数组长度不统一");
        }
        double s = 0.0, sx = 0.0, sy = 0.0;
        int n = X.Length;
        for(int i = 0;i < n;i++) {
            s += X[i] * Y[i];
            sx += X[i];
            sy += Y[i];
        }
        return (s / n) - (sx / n) * (sy / n);
    }
    public static double[] LinearFit(double[] X, double[] Y) {
        //对自变量数组X和因变量Y做线性拟合
        if(X == null || Y == null || X.Length != Y.Length) {
            return null;
        }
        int n = X.Length;
        double xav = Average(X), yav = Average(Y), xyav = 0.0, x2av = 0.0, y2av = 0.0, u = 0.0;
        for(int i = 0;i < n;i++) {
            xyav += X[i] * Y[i];
            x2av += X[i] * X[i];
            y2av += Y[i] * Y[i];
        }
        xyav /= n; x2av /= n;
        double a, b, r;
        b = (xav * yav - xyav) / (xav * xav - x2av);
        a = yav - b * xav;
        r = (xyav - xav * yav) / MATH.Sqrt((x2av - xav * xav) * (y2av - yav * yav));
        for(int i = 0;i < n;i++) {
            u += (Y[i] - (a + b * X[i])) * (Y[i] - (a + b * X[i]));
        }
        //返回值:{拟合的b,拟合的a,相关系数r,不确定度}
        return new double[] { b, a, r, MATH.Sqrt(u / (n - 2)) };
    }
    public static double[][] SuccessionalDifference(double[] X, double[] Y) {
        //对自变量数组X和因变量Y做逐差法
        if(X == null || Y == null || X.Length != Y.Length) {
            return null;
        }
        int k = X.Length;
        int n = k / 2;
        int dif = k % 2 == 0 ? n : n + 1;
        double[] bi = new double[n];
        for(int i = 0;i < n;i++) {
            bi[i] = (Y[i + dif] - Y[i]) / (X[i + dif] - X[i]);
        }
        double bar = Average(bi), xav = Average(X), yav = Average(Y);
        //返回值:{{拟合的b,拟合的a,X的平均值,Y的平均值,b的不确定度},{每个逐差的斜率}}
        return new double[][] { new double[] { bar, yav - xav * bar, xav, yav, Uncertain_A(bi) / n }, bi };
    }
    public static (double, double, string) BookVolume(double[] A0, double[] B0, double[] C0) {
        //测量数据,长A 宽B 高C,用户输入自行计算的体积和不确定度
        //calcstr若不为null返回计算过程字符串
        if(A0 == null || B0 == null || C0 == null) {
            return default;
        }
        //int n = A.Length;//A,B,C:mm
        //if(B.Length != n || C.Length != n) {
        //    return default;
        //}
        double[] A = A0.Where((i) => i != 0.0).ToArray(), B = B0.Where((i) => i != 0.0).ToArray(), C = C0.Where((i) => i != 0.0).ToArray();
        double A_av = Average(A), B_av = Average(B), C_av = Average(C);
        double u1a = Uncertain_A(A), u1b = Uncertain_A(B), u1c = Uncertain_A(C);
        double sq3 = MATH.Sqrt(3), u2 = 0.02 / sq3, u22 = 0.5 / sq3;
        double ua = MATH.Sqrt(u1a * u1a + u22 * u22), ub = MATH.Sqrt(u1b * u1b + u22 * u22), uc = MATH.Sqrt(u1c * u1c + u2 * u2);
        double uar = ua / A_av, ubr = ub / B_av, ucr = uc / C_av;
        double V = A_av * B_av * C_av;
        double uv = MATH.Sqrt(uar * uar + ubr * ubr + ucr * ucr);
        double u = uv * V;
        (string, string) fx = FixResult(V, u), fa = FixResult(A_av, ua), fb = FixResult(B_av, ub), fc = FixResult(C_av, uc);
        return (V, u, $"计算过程:\r\n未修约:A={A_av};B={B_av};C={C_av};\r\n直尺的b类不确定度Ub1={u22};\r\n卡尺的b类不确定度Ub1={u2};\r\na类不确定度:Ua(A)={u1a};\r\nUa(B)={u1b};\r\nUa(C)={u1c};\r\n合成不确实度U(A)={ua};\r\nU(B)={ub};\r\nU(C)={uc};\r\n相对不确定度 U(V)/V=sqrt((U(a)/A)^2+(U(b)/B)^2+(U(C)/C)^2))={uv}\r\n最终修约结果:\r\n(A ± U(A))=({fa.Item1})±({fa.Item2})\r\n(B ± U(B))=({fb.Item1})±({fb.Item2})\r\n(C ± U(C))=({fc.Item1})±({fc.Item2})\r\nV={fx.Item1};U(V)={fx.Item2}\r\n(V ± U(V))=({fx.Item1})±({fx.Item2})\r\n以上内容均以毫米为单位,结果仅供参考");
    }
    public static (string, bool) CheckVar(double calc_val, double calc_uncertain, double user_calc, double user_uncertain) {
        double fix1 = double.Parse(user_uncertain.ToString("#e+0"));
        double fix2 = double.Parse(calc_uncertain.ToString("#e+0"));
        bool checkcorr = Math.Abs(fix1 - user_uncertain) <= double.Epsilon;
        return ($"用户计算值和机器值的误差{(user_calc - calc_val) / calc_val:###%},不确定度误差:{(fix1 - fix2) / fix2:###%}", checkcorr);
    }
    public static (string, string) FixResult(double origin, double uncertain) {
        string s = uncertain.ToString("#e+0");
        if(double.TryParse(s, out double ufix)) {
            Match mch = Regex.Match(s, "e[+|-][0-9]+", RegexOptions.Compiled);
            if(int.TryParse(mch.Value.Substring(1), out int digit)) {
                Console.WriteLine(digit);
                double move = MATH.Pow(10, digit);
                double fix = MATH.Round(origin / move);
                int fixlen = fix.ToString().Length;
                string fmt = "#." + new string('#', fixlen - 1) + "e+0";
                string res = (fix * move).ToString(fmt);
                return (res, s);
            }
        }
        return default;
    }
    public static double CheckCorrectness(
        double v_real, double v_user, double v_user_uncertain,
        double a_real, double a_user, double a_user_uncertain,
        double b_real, double b_user, double b_user_uncertain,
        double c_real, double c_user, double c_user_uncertain
        ) {
        double cor = 0;
        cor += (v_real <= v_user + v_user_uncertain && v_real >= v_user_uncertain) ? 1.5 : 1;
        cor += (a_real <= a_user + a_user_uncertain && a_real >= a_user_uncertain) ? 1 : 0.7;
        cor += (b_real <= b_user + b_user_uncertain && a_real >= b_user_uncertain) ? 1 : 0.7;
        cor += (c_real <= c_user + c_user_uncertain && a_real >= c_user_uncertain) ? 1 : 0.7;
        return cor;
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
        for(int i = 0;i < a.Length;i++) {
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


[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
public struct OpenFileName {
    public int lStructSize;
    public IntPtr hwndOwner;
    public IntPtr hInstance;
    public string lpstrFilter;
    public string lpstrCustomFilter;
    public int nMaxCustFilter;
    public int nFilterIndex;
    public string lpstrFile;
    public int nMaxFile;
    public string lpstrFileTitle;
    public int nMaxFileTitle;
    public string lpstrInitialDir;
    public string lpstrTitle;
    public int Flags;
    public short nFileOffset;
    public short nFileExtension;
    public string lpstrDefExt;
    public IntPtr lCustData;
    public IntPtr lpfnHook;
    public string lpTemplateName;
    public IntPtr pvReserved;
    public int dwReserved;
    public int flagsEx;
}
public delegate int DialogHookProc(IntPtr hwnd, int msg, IntPtr wparam, IntPtr lparam);
public static class IOHelper {
    [DllImport("user32.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Auto, EntryPoint = "MessageBox")]
    public static extern int MessageBox(IntPtr hwnd, string text, string caption, uint type);
    [DllImport("comdlg32.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Auto, EntryPoint = "GetOpenFileName", SetLastError = true, ThrowOnUnmappableChar = true)]
    public static extern int OpenFileDialog(ref OpenFileName lpofn);
    [DllImport("comdlg32.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Auto, EntryPoint = "GetSaveFileName", SetLastError = true, ThrowOnUnmappableChar = true)]
    public static extern int SaveFileDialog(ref OpenFileName lpofn);
    [DllImport("kernel32.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Auto)]
    public static extern int GetLastError();
    public static string OpenFile() {
        try {
            OpenFileName ofn = default;
            ofn.lStructSize = Marshal.SizeOf(ofn);
            ofn.lpstrFilter = "All files(*.*)\0\0";
            ofn.lpstrFile = new string(new char[256]);
            ofn.nMaxFile = ofn.lpstrFile.Length;
            ofn.lpstrFileTitle = new string(new char[64]);
            ofn.nMaxFileTitle = ofn.lpstrFileTitle.Length;
            /*ofn.lpfnHook = (hwnd, msg, wparam, lparam) => {
                Console.WriteLine($"hwnd:{hwnd},msg:{msg},wparam:{wparam},lparam:{lparam}");
                //return DefDlgProc(hwnd, msg, wparam, lparam);
                return 0;
            };*/
            ofn.Flags = 0x00080000 | 0x00001000 | 0x00000800 | 0x00000200 | 0x00000008;//| 0x00000020;
            if(OpenFileDialog(ref ofn) != 0) {
                //Console.WriteLine(ofn.lpstrFile);
                //Console.WriteLine(ofn.lpstrFileTitle);
                return ofn.lpstrFile;
            }
        }
        catch(Exception ex) {
            Debug.LogError(ex);
        }
        return null;
    }
    public static string SaveFile() {
        try {
            OpenFileName ofn = default;
            ofn.lStructSize = Marshal.SizeOf(ofn);
            ofn.lpstrFilter = "All files(*.*)\0\0";
            ofn.lpstrFile = new string(new char[256]);
            ofn.nMaxFile = ofn.lpstrFile.Length;
            ofn.lpstrFileTitle = new string(new char[64]);
            ofn.nMaxFileTitle = ofn.lpstrFileTitle.Length;
            /*ofn.lpfnHook = (hwnd, msg, wparam, lparam) => {
                Console.WriteLine($"hwnd:{hwnd},msg:{msg},wparam:{wparam},lparam:{lparam}");
                //return DefDlgProc(hwnd, msg, wparam, lparam);
                return 0;
            };*/
            ofn.Flags = 0x00080000 | 0x00001000 | 0x00000800 | 0x00000200 | 0x00000008;//| 0x00000020;
            if(SaveFileDialog(ref ofn) != 0) {
                //Console.WriteLine(ofn.lpstrFile);
                //Console.WriteLine(ofn.lpstrFileTitle);
                return ofn.lpstrFile;
            }
        }
        catch(Exception ex) {
            Debug.LogError(ex);
        }
        return null;
    }
    public static T ReadJson<T>() {
        try {
            string jsonpath = OpenFile();
            Debug.Log(jsonpath);
            T result;
            using(StreamReader sr = new StreamReader(jsonpath)) {
                string tmp = sr.ReadToEnd();
                result = JsonConvert.DeserializeObject<T>(tmp);
            }
            return result;
        }
        catch(Exception ex) {
            Debug.LogError(ex);
        }
        return default;
    }
    public static void WriteJson<T>(T json) {
        try {
            string jsonpath = SaveFile();
            using(StreamWriter sw = new StreamWriter(jsonpath, false, Encoding.UTF8)) {
                sw.WriteLine(JsonConvert.SerializeObject(json));
            }
        }
        catch(Exception ex) {
            Debug.LogError(ex);
        }
    }
    public static string ReadString() {
        try {
            string jsonpath = OpenFile(), tmp;
            using(StreamReader sr = new StreamReader(jsonpath)) {
                tmp = sr.ReadToEnd();
            }
            return tmp;
        }
        catch(Exception ex) {
            Debug.LogError(ex);
        }
        return default;
    }
    public static void WriteString(string data) {
        try {
            string jsonpath = SaveFile();
            using(StreamWriter sw = new StreamWriter(jsonpath, false, Encoding.UTF8)) {
                sw.WriteLine(data);
            }
        }
        catch(Exception ex) {
            Debug.LogError(ex);
        }
    }
    public static GameObject AddPrefab() {
        try {
            string open = OpenFile(), tmp = string.Copy(open);
            if(open == null) {
                return null;
            }
            string dest = Path.GetFileName(tmp);
            string dir = $"{Application.dataPath}/resources/{dest}";
            try {
                File.Copy(open, dir, true);
            }
            catch(Exception ex2) {
                Debug.Log(ex2);
            }

            //var obj = PrefabUtility.LoadPrefabContents($"assets/prefab/{dest}");
            string s = Path.GetFileNameWithoutExtension(dest);
            //Debug.Log(s);
            var obj = Resources.Load<GameObject>(s);
            //Debug.Log(obj);
            return obj;
        }
        catch(Exception ex) {
            Debug.LogError(ex);
        }
        return null;
    }
}

public class Storage {
    public int id { get; }
    private string name { get; }
    private string directory = null;
    /// <summary>
    /// 创建一个Storage对象，对应指定ID的Storage
    /// </summary>
    /// <param name="id">Storage的ID</param>
    public Storage(int id) {
        this.id = id;
        directory = $"{Application.persistentDataPath}/LocalStorage/{id}/";
    }
    private Storage(string name) {
        id = -1;
        this.name = name;
        directory = $"{Application.persistentDataPath}/LocalStorage/{name}/";
    }
    /// <summary>
    /// 游戏通用Storage
    /// </summary>
    public static Storage CommonStorage {
        get => new Storage("common");
    }
    public object this[string key, Type t] {
        get => JsonConvert.DeserializeObject(FileIOHelper.ReadJSONFile(directory + key), t);
        set => SetStorage(key, value);
    }
    public object this[string key] {
        set => SetStorage(key, value);
    }
    /// <summary>
    /// 获取Storage存储的对象，以类型T返回
    /// </summary>
    /// <typeparam name="T">映射的对象类型</typeparam>
    /// <param name="key">键</param>
    /// <returns></returns>
    public T GetStorage<T>(string key) where T : new() {
        string path = directory + key;
        var ret = JsonConvert.DeserializeObject<T>(FileIOHelper.ReadJSONFile(path), new JsonSerializerSettings() {
            Error = (sender, e) => {
                e.ErrorContext.Handled = true;
                SetStorage(key, new T());
            }
        });
        if(ret == null)
            return new T();
        return ret;
    }
    public T GetStorage<T>(string key, Func<T> initializer) where T : new() {
        string path = directory + key;
        T whenError = new T();
        var ret = JsonConvert.DeserializeObject<T>(FileIOHelper.ReadJSONFile(path), new JsonSerializerSettings() {
            Error = (sender, e) => {
                e.ErrorContext.Handled = true;
                whenError = initializer();
            }
        });
        if(ret == null)
            return whenError;
        return ret;
    }
    /// <summary>
    /// 将对象存储到Storage中
    /// </summary>
    /// <param name="key">键</param>
    /// <param name="values">对象值，不可为自包含属性类型</param>
    public void SetStorage(string key, object values) {
        string path = directory + key;
        FileIOHelper.SaveFile(path, JsonConvert.SerializeObject(values, new JsonSerializerSettings() {
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
            Error = (sender, e) => {
                Debug.Log($"JsonSerializationError: {e.ErrorContext.Error}");
                e.ErrorContext.Handled = true;
            }
        }));
    }
    public void SetStorage(string key, object values, EventHandler<Newtonsoft.Json.Serialization.ErrorEventArgs> errorHandler) {
        string path = directory + key;
        FileIOHelper.SaveFile(path, JsonConvert.SerializeObject(values, new JsonSerializerSettings() {
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
            Error = errorHandler
        }));
    }
    public void DeleteStorage() {
        Directory.Delete(directory, true);
    }
    protected static class FileIOHelper {
        internal static string ReadJSONFile(string Path) {
            string json = ReadFile(Path);
            if(json == null) return "{}";
            else return json;
        }
        internal static string ReadFile(string Path) {
            if(!File.Exists(Path))
                return null;
            using(StreamReader sr = new StreamReader(Path)) {
                if(sr == null)
                    return null;
                return sr.ReadToEnd();
            }
        }
        internal static void SaveFile(string path, string data) {
            if(!Directory.Exists(Path.GetDirectoryName(path)))
                Directory.CreateDirectory(Path.GetDirectoryName(path));
            File.Create(path).Close();
            using(StreamWriter sw = new StreamWriter(path, false)) {
                sw.Write(data);
            }
        }
    }
}