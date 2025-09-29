using UnityEngine;

public class AcelerAuto : MonoBehaviour
{
    public float acelPorSeg;
    public float velMax;
    public float tiempRecColl;


    private bool _avil = true;
    private ReductorVelColl _obstaculo;
    private float _tempo;
    private float _velocidad;

    // Use this for initialization
    private void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {
        /*
        if(Velocidad < VelMax)
        {
            Velocidad += AcelPorSeg * Time.deltaTime;
        }
        */

        //Debug.Log("Velocidad: "+rigidbody.velocity.magnitude);

        if (_avil)
        {
            _tempo += Time.deltaTime;
            if (_tempo > tiempRecColl)
            {
                _tempo = 0;
                _avil = false;
            }
        }
    }

    private void FixedUpdate()
    {
        /*
        //this.rigidbody.MovePosition(this.transform.position + this.transform.forward * Velocidad);
        if(rigidbody.velocity.magnitude < VelMax)
            rigidbody.velocity += transform.forward * AcelPorSeg * Time.deltaTime;
            */


        /*
        if(Velocidad < VelMax)
        {
            Velocidad += AcelPorSeg * Time.fixedDeltaTime;
        }

        rigidbody.MovePosition(this.transform.position + this.transform.forward * Velocidad);
        */

        if (_velocidad < velMax) _velocidad += acelPorSeg * Time.fixedDeltaTime;

        GetComponent<Rigidbody>().AddForce(transform.forward * _velocidad);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!_avil)
        {
            _obstaculo = collision.transform.GetComponent<ReductorVelColl>();
            if (_obstaculo != null)
                //Velocidad -= Obstaculo.ReduccionVel;
                //if(Velocidad < 0)
                //Velocidad = 0;
                GetComponent<Rigidbody>().linearVelocity /= 2;
            _obstaculo = null;
        }
    }

    public void Chocar(ReductorVelColl obst)
    {
        GetComponent<Rigidbody>().linearVelocity /= 2;
    }
}