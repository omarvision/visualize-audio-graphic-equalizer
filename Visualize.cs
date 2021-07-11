using UnityEngine;

public class Visualize : MonoBehaviour
{
    public float cubeScale = 500f;
    public int numSamples = 128;
    private AudioSource aud = null;
    private float[] samples = null;
    private GameObject[] cubes = null;

    private void Start()
    {
        aud = this.GetComponent<AudioSource>();

        samples = new float[numSamples];
        cubes = new GameObject[numSamples];

        CreateCubes();
    }
    private void Update()
    {
        GetSamples();
        ShowSamples();
    }
    private void CreateCubes()
    {
        for (int i = 0; i < numSamples; i++)
        {
            GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
            cube.name = "cube" + i.ToString().PadLeft(3, '0');
            cube.transform.parent = this.transform;
            cube.transform.position = new Vector3(i, 0, 0);

            cubes[i] = cube;
        }
    }
    private void GetSamples()
    {
        aud.GetSpectrumData(samples, 0, FFTWindow.Blackman);
    }
    private void ShowSamples()
    {
        for (int i = 0; i < numSamples; i++)
        {
            float val = cubeScale * samples[i];
            cubes[i].transform.localScale = new Vector3(1, val, val * 0.25f);
        }
    }
}
