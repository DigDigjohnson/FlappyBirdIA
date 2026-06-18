using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FitnessGraphUI : MonoBehaviour
{
    public static FitnessGraphUI Instance;

    [Header("Dimensões")]
    public int graphWidth  = 200;
    public int graphHeight = 120;

    [Header("Cores")]
    public Color backgroundColor = new Color(0.08f, 0.08f, 0.12f, 0.90f);
    public Color lineColor        = new Color(1.00f, 0.85f, 0.00f, 1.00f);
    public Color gridColor        = new Color(0.20f, 0.20f, 0.30f, 1.00f);
    public Color axisColor        = new Color(0.50f, 0.50f, 0.60f, 1.00f);

    [Header("Janela de dados")]
    public int maxDataPoints = 100;

    [Header("Referência")]
    public RawImage rawImage;

    Texture2D _tex;
    readonly List<float> _fitnessHistory = new List<float>();
    float _globalMax = 1f;

    void Awake()
    {
        Instance = this;
        _tex = new Texture2D(graphWidth, graphHeight, TextureFormat.RGBA32, false);
        _tex.filterMode = FilterMode.Point;
        FillBackground();
        _tex.Apply();
        if (rawImage != null)
            rawImage.texture = _tex;
    }

    public void RecordFitness(float fitness)
    {
        _fitnessHistory.Add(fitness);
        if (_fitnessHistory.Count > maxDataPoints)
            _fitnessHistory.RemoveAt(0);
        if (fitness > _globalMax)
            _globalMax = fitness;
        RedrawGraph();
    }

    void RedrawGraph()
    {
        FillBackground();
        DrawGridLines();
        DrawAxes();
        DrawLine();
        _tex.Apply();
    }

    void FillBackground()
    {
        Color[] pixels = new Color[graphWidth * graphHeight];
        for (int i = 0; i < pixels.Length; i++)
            pixels[i] = backgroundColor;
        _tex.SetPixels(pixels);
    }

    void DrawGridLines()
    {
        for (int g = 1; g <= 3; g++)
        {
            int y = (int)(graphHeight * g / 4f);
            for (int x = 0; x < graphWidth; x++)
                _tex.SetPixel(x, y, gridColor);
        }
    }

    void DrawAxes()
    {
        for (int x = 0; x < graphWidth; x++)
            _tex.SetPixel(x, 1, axisColor);
        for (int y = 0; y < graphHeight; y++)
            _tex.SetPixel(1, y, axisColor);
    }

    void DrawLine()
    {
        int count = _fitnessHistory.Count;
        if (count < 2) return;

        int denom = Mathf.Max(maxDataPoints - 1, count - 1);

        for (int i = 1; i < count; i++)
        {
            int x0 = (int)((float)(i - 1) / denom * (graphWidth  - 1));
            int x1 = (int)((float)(i)     / denom * (graphWidth  - 1));
            int y0 = NormalizeY(_fitnessHistory[i - 1]);
            int y1 = NormalizeY(_fitnessHistory[i]);
            DrawSegment(x0, y0, x1, y1);
        }
    }

    int NormalizeY(float value)
    {
        float t = Mathf.Clamp01(value / _globalMax);
        return (int)(t * (graphHeight - 4)) + 2;
    }

    void DrawSegment(int x0, int y0, int x1, int y1)
    {
        int dx = Mathf.Abs(x1 - x0);
        int dy = Mathf.Abs(y1 - y0);
        int sx = x0 < x1 ? 1 : -1;
        int sy = y0 < y1 ? 1 : -1;
        int err = dx - dy;

        while (true)
        {
            SetThickPixel(x0, y0);
            if (x0 == x1 && y0 == y1) break;
            int e2 = 2 * err;
            if (e2 > -dy) { err -= dy; x0 += sx; }
            if (e2 <  dx) { err += dx; y0 += sy; }
        }
    }

    void SetThickPixel(int x, int y)
    {
        _tex.SetPixel(x, y,     lineColor);
        _tex.SetPixel(x, y + 1, lineColor);
    }
}
