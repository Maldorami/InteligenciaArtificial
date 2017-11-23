using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RNShipController : MonoBehaviour
{

    public GameObject fire;
    public float throtle;
    public float steerSpeed;
    public Transform resetPoint;

    Rigidbody _rg;

    [SerializeField]
    float airTime = 0;
    [SerializeField]
    bool isOnCollision = true;


    public float puntaje = 0;

    public float distModif = 1;
    public float rotModif = 1;
    public float airTimeModif = 1;
    public float impactToFinishModif = 1;
    public float objectiveReward = 1;
    public float impactVelocity = 0;

    public Transform target;

    public int inputs;
    public int outputs;
    public int hiddenLayers;
    public int neuronasPorLayer;
    public float pendiente;
    RNRedNeuronal brain;

    public RNCromosoma cromosoma;

    List<float> inputList;
    List<float> outputList;

    float timer = 0;


    public void InicializarAgent()
    {
        brain = new RNRedNeuronal(inputs, outputs, hiddenLayers, neuronasPorLayer, pendiente);
        brain.CrearRedNeuronal();
        cromosoma = brain.ObtenerPesos();

        _rg = GetComponent<Rigidbody>();

        inputList = new List<float>();
        outputList = new List<float>();
    }

    public void ActualizarCromosoma(RNCromosoma newCromosoma)
    {
        cromosoma = newCromosoma;
        ActualizarPesos();
    }

    public void ActualizarPesos()
    {
        brain.SetearPesos(cromosoma);
    }

    private void OnDisable()
    {
        _rg.isKinematic = true;
        _rg.isKinematic = false;

        _rg.velocity = Vector3.zero;
    }

    private void OnEnable()
    {
        isOnCollision = false;

        transform.position = resetPoint.position;
        transform.rotation = resetPoint.rotation;

        airTime = 0;
        impactVelocity = 50f;
    }

    private void FixedUpdate()
    {
        fire.SetActive(false);

        if (!isOnCollision) airTime += Time.deltaTime;

        timer += Time.fixedDeltaTime;

        inputList.Add((target.transform.position - transform.position).normalized.x);
        inputList.Add((target.transform.position - transform.position).normalized.y);
        inputList.Add(transform.up.normalized.x);
        inputList.Add(transform.up.normalized.y);
        inputList.Add(_rg.velocity.normalized.x);
        inputList.Add(_rg.velocity.normalized.y);

        outputList = brain.UpdateRN(inputList);

        if (!isOnCollision)
        {
            ApplyThrotle(outputList[0]);
            SteerLeft(outputList[1]);
            SteerRight(outputList[2]);
        }

        inputList.Clear();
        outputList.Clear();

    }

    public void ApplyThrotle(float modif)
    {
        _rg.AddForce(transform.up * throtle * modif, ForceMode.Acceleration);
        fire.SetActive(true);
    }

    public void SteerLeft(float modif)
    {
        transform.Rotate(new Vector3(0.0f, 0.0f, steerSpeed * modif * Time.deltaTime));
    }
    public void SteerRight(float modif)
    {
        transform.Rotate(new Vector3(0.0f, 0.0f, -steerSpeed * modif * Time.deltaTime));
    }


    public void CalcularPuntaje()
    {
        float distFit = 0;
        float rotFit = transform.rotation.z;
        distFit = Vector3.Distance(transform.position, target.transform.position);

        if (impactVelocity < 1) impactVelocity = 1;

        puntaje = distModif / distFit + impactToFinishModif / impactVelocity + airTime * airTimeModif;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag != "Start") isOnCollision = true;
        
        if(impactVelocity == 0)
            impactVelocity = collision.relativeVelocity.magnitude;

        if (collision.gameObject.tag == "Finish" && impactVelocity - objectiveReward > 1)
            impactVelocity -= objectiveReward;

    }
}
