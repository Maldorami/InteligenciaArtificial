using UnityEngine.UI;
using UnityEngine;

public class PedidoHUD : MonoBehaviour {

    public Text cantTotalPedidoText;
    public Text cantMochilaText;
    public Text cantDepoText;
    public Text capacidadMochila;

    public MinerMovement miner;

    public InputField inputField;
    public Button DeliveryButton;

    private void Update()
    {
        cantDepoText.text = "Piedras ya depositadas\n" + miner.piedrasEnElDeposito.ToString();
        cantMochilaText.text = "Piedras en mochila\n" + miner.cantPiedrasEnMochila.ToString();
        cantTotalPedidoText.text = "Objetivo de piedras\n" + miner.objetivoPiedrasAObtener.ToString();
        capacidadMochila.text = "Capacidad de mochila\n" + miner.capacidadMochila.ToString();

		DeliveryButton.interactable = (miner.StateMachine.GetState() == (int)State.Quieto);
    }

    public void Delivery()
    {
        miner.NuevoPedidoDePiedras(System.Convert.ToInt32(inputField.text));
    }

}
