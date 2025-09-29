using UnityEngine;

public class BolsaAnim : MonoBehaviour
{
    public float giroVel = 1;

    public Vector3 amlitud = Vector3.zero;

    //public float AmplitudVertical = 1;
    public float velMov = 1;

    public bool giro = true;
    public bool movVert = true;
    private bool _iniciado;

    private Vector3 _posIni;

    //float Aumento = 0;
    private bool _subiendo = true;

    //para que inicien a destiempo
    private float _tiempInicio;
    private Vector3 _vAuxGir = Vector3.zero;

    private Vector3 _vAuxPos = Vector3.zero;

    // Use this for initialization
    private void Start()
    {
        _posIni = transform.position;

        _tiempInicio = Random.Range(0, 2);
    }

    // Update is called once per frame
    private void Update()
    {
        if (_iniciado)
        {
            if (giro)
            {
                _vAuxGir = Vector3.zero;
                _vAuxGir.y = T.GetDT() * giroVel;
                transform.localEulerAngles += _vAuxGir;
            }

            if (movVert)
            {
                if (_subiendo)
                {
                    transform.localPosition += amlitud.normalized * Time.deltaTime * velMov;

                    if ((transform.position - _posIni).magnitude > amlitud.magnitude / 2)
                    {
                        _subiendo = false;
                        transform.localPosition -= amlitud.normalized * Time.deltaTime * velMov;
                    }
                }
                else
                {
                    transform.localPosition -= amlitud.normalized * Time.deltaTime * velMov;
                    if ((transform.position - _posIni).magnitude > amlitud.magnitude / 2)
                    {
                        _subiendo = true;
                        transform.localPosition += amlitud.normalized * Time.deltaTime * velMov;
                    }
                }
            }
        }
        else
        {
            _tiempInicio -= Time.deltaTime;
            if (_tiempInicio <= 0)
                _iniciado = true;
        }
    }
}