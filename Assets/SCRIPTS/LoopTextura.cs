using UnityEngine;

public class LoopTextura : MonoBehaviour
{
    public float intervalo = 1;

    public Texture2D[] imagenes;
    private int _contador;
    private float _tempo;

    // Use this for initialization
    private void Start()
    {
        if (imagenes.Length > 0)
            GetComponent<Renderer>().material.mainTexture = imagenes[0];
    }

    // Update is called once per frame
    private void Update()
    {
        _tempo += Time.deltaTime;

        if (_tempo >= intervalo)
        {
            _tempo = 0;
            _contador++;
            if (_contador >= imagenes.Length) _contador = 0;
            GetComponent<Renderer>().material.mainTexture = imagenes[_contador];
        }
    }
}