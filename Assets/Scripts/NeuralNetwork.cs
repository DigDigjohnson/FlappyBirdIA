using System;
using UnityEngine;

[Serializable]
public class NeuralNetwork
{
    public int[] layers;
    public float[][] neurons;
    public float[][][] weights;
    public float fitness;

    public NeuralNetwork(int[] layers)
    {
        this.layers = layers;
        InitNeurons();
        InitWeights();
    }

    public NeuralNetwork(NeuralNetwork copy)
    {
        layers = copy.layers;
        InitNeurons();
        InitWeights();
        CopyWeights(copy.weights);
    }

    void InitNeurons()
    {
        neurons = new float[layers.Length][];
        for (int i = 0; i < layers.Length; i++)
            neurons[i] = new float[layers[i]];
    }

    void InitWeights()
    {
        weights = new float[layers.Length - 1][][];
        for (int i = 0; i < layers.Length - 1; i++)
        {
            weights[i] = new float[layers[i + 1]][];
            for (int j = 0; j < layers[i + 1]; j++)
            {
                weights[i][j] = new float[layers[i]];
                for (int k = 0; k < layers[i]; k++)
                    weights[i][j][k] = UnityEngine.Random.Range(-1f, 1f);
            }
        }
    }

    void CopyWeights(float[][][] src)
    {
        for (int i = 0; i < weights.Length; i++)
            for (int j = 0; j < weights[i].Length; j++)
                for (int k = 0; k < weights[i][j].Length; k++)
                    weights[i][j][k] = src[i][j][k];
    }

    public float[] FeedForward(float[] inputs)
    {
        for (int i = 0; i < inputs.Length; i++)
            neurons[0][i] = inputs[i];

        for (int i = 1; i < layers.Length; i++)
        {
            for (int j = 0; j < layers[i]; j++)
            {
                float sum = 0f;
                for (int k = 0; k < layers[i - 1]; k++)
                    sum += neurons[i - 1][k] * weights[i - 1][j][k];
                neurons[i][j] = (float)Math.Tanh(sum);
            }
        }
        return neurons[neurons.Length - 1];
    }

    public void Mutate(float mutationRate, float mutationStrength)
    {
        for (int i = 0; i < weights.Length; i++)
            for (int j = 0; j < weights[i].Length; j++)
                for (int k = 0; k < weights[i][j].Length; k++)
                    if (UnityEngine.Random.value < mutationRate)
                        weights[i][j][k] += UnityEngine.Random.Range(-mutationStrength, mutationStrength);
    }
}
