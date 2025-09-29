using UnityEngine;

public class Aceleracion : MonoBehaviour
{
    public Transform manoDer;
    public Transform manoIzq;

    public float alturaMedia; //valor en eje Y que calibra el 0 de cada pedal

    public float sensAcel = 1;
    public float sensFren = 1;

    public Transform camion; //lo que va a conducir

    //pedales
    public Transform pedalAcel;
    public Transform pedalFren;
    public float sensivPed = 1;
    private float _acelerado;
    private float _difDer;


    private float _difIzq;

    private float _frenado;
    private Vector3 _pAclPosIni;
    private Vector3 _pFrnPosIni;

    //---------------------------------------------------------//

    // Use this for initialization
    private void Start()
    {
        _pAclPosIni = pedalAcel.localPosition;
        _pFrnPosIni = pedalFren.localPosition;
    }

    // Update is called once per frame
    private void Update()
    {
        _difDer = manoDer.position.y - alturaMedia;
        _difIzq = manoIzq.position.y - alturaMedia;

        //acelerar
        if (_difDer > 0)
        {
            _acelerado = _difDer * sensAcel * Time.deltaTime;

            camion.position += _acelerado * camion.forward;

            pedalAcel.localPosition = _pAclPosIni - pedalAcel.forward * sensivPed * _acelerado;
        }

        //PedalFren.localPosition = PAclPosIni;
        //frenar
        if (_difIzq > 0)
        {
            _frenado = _difIzq * sensFren * Time.deltaTime;

            camion.position -= _frenado * camion.forward;

            pedalFren.localPosition = _pFrnPosIni - pedalFren.forward * sensivPed * _frenado;
        }
        //PedalFren.localPosition = PFrnPosIni;
    }
}