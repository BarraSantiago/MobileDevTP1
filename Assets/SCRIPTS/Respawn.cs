using UnityEngine;

public class Respawn : MonoBehaviour
{
    public float angMax = 90; //angulo maximo antes del cual se reinicia el camion

    public float rangMinDer;
    public float rangMaxDer;
    public float tiempDeNoColision = 2;
    private readonly int _verifPorCuadro = 20;
    private int _contador;
    private CheakPoint _cpAct;
    private CheakPoint _cpAnt;

    private bool _ignorandoColision;
    private float _tempo;

    //--------------------------------------------------------//

    // Use this for initialization
    private void Start()
    {
        /*
        //a modo de prueba
        TiempDeNoColision = 100;
        IgnorarColision(true);
        */

        //restaura las colisiones
        Physics.IgnoreLayerCollision(8, 9, false);
    }

    // Update is called once per frame
    private void Update()
    {
        if (_cpAct != null)
        {
            _contador++;
            if (_contador == _verifPorCuadro)
            {
                _contador = 0;
                if (angMax < Quaternion.Angle(transform.rotation, _cpAct.transform.rotation)) Respawnear();
            }
        }

        if (_ignorandoColision)
        {
            _tempo += T.GetDT();
            if (_tempo > tiempDeNoColision) IgnorarColision(false);
        }
    }

    //--------------------------------------------------------//

    public void Respawnear()
    {
        GetComponent<Rigidbody>().linearVelocity = Vector3.zero;

        gameObject.GetComponent<CarController>().SetGiro(0f);

        if (_cpAct.Habilitado())
        {
            if (GetComponent<Visualizacion>().ladoAct == Visualizacion.Lado.Der)
                transform.position = _cpAct.transform.position +
                                     _cpAct.transform.right * Random.Range(rangMinDer, rangMaxDer);
            else
                transform.position = _cpAct.transform.position +
                                     _cpAct.transform.right * Random.Range(rangMinDer * -1, rangMaxDer * -1);
            transform.forward = _cpAct.transform.forward;
        }
        else if (_cpAnt != null)
        {
            if (GetComponent<Visualizacion>().ladoAct == Visualizacion.Lado.Der)
                transform.position = _cpAnt.transform.position +
                                     _cpAnt.transform.right * Random.Range(rangMinDer, rangMaxDer);
            else
                transform.position = _cpAnt.transform.position +
                                     _cpAnt.transform.right * Random.Range(rangMinDer * -1, rangMaxDer * -1);
            transform.forward = _cpAnt.transform.forward;
        }

        IgnorarColision(true);

        //animacion de resp
    }

    public void Respawnear(Vector3 pos)
    {
        GetComponent<Rigidbody>().linearVelocity = Vector3.zero;

        gameObject.GetComponent<CarController>().SetGiro(0f);

        transform.position = pos;

        IgnorarColision(true);
    }

    public void Respawnear(Vector3 pos, Vector3 dir)
    {
        GetComponent<Rigidbody>().linearVelocity = Vector3.zero;

        gameObject.GetComponent<CarController>().SetGiro(0f);

        transform.position = pos;
        transform.forward = dir;

        IgnorarColision(true);
    }

    public void AgregarCp(CheakPoint cp)
    {
        if (cp != _cpAct)
        {
            _cpAnt = _cpAct;
            _cpAct = cp;
        }
    }

    private void IgnorarColision(bool b)
    {
        //no contempla si los dos camiones respawnean relativamente cerca en el espacio 
        //temporal y uno de ellos va contra el otro, 
        //justo el segundo cancela las colisiones e inmediatamente el 1º las reactiva, 
        //entonces colisionan, pero es dificil que suceda. 

        Physics.IgnoreLayerCollision(8, 9, b);
        _ignorandoColision = b;
        _tempo = 0;
    }
}