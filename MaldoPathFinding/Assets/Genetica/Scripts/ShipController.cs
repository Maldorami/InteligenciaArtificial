using UnityEngine;

public class ShipController : MonoBehaviour {
    [SerializeField]
    GameObject fire;
    [SerializeField]
    float throtle;
    [SerializeField]
    float steerSpeed;
    [SerializeField]
    Rigidbody _rg;

    [SerializeField]
    Agent agent;

    private void Start()
    {
        _rg = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        fire.SetActive(false);        
        if (Input.GetKey(KeyCode.W)) ApplyThrotle();
        if (Input.GetKey(KeyCode.A)) SteerLeft();
        if (Input.GetKey(KeyCode.D)) SteerRight();
    }

    public void ApplyThrotle()
    {
        _rg.AddForce(transform.up * throtle);
        fire.SetActive(true);
    }

    public void SteerLeft()
    {
        transform.Rotate(new Vector3(0.0f, 0.0f, steerSpeed * Time.deltaTime));
    }
    public void SteerRight()
    {
        transform.Rotate(new Vector3(0.0f, 0.0f, -steerSpeed * Time.deltaTime));
    }
    

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Start")
        {
            //Debug.Log("<color=green>LLEGO!</color>");
            return;
        }

        if (collision.gameObject.tag == "Finish")
        {
            Debug.Log("<color=green>LLEGO!</color>");
        }
            else
        {
            Destroy(gameObject);
        }
    }

    public void StartTest(Agent Agent)
    {
        this.agent = Agent;



    }
}
