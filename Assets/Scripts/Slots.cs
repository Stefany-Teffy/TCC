// transforma os blocos e destroi quando necessario
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Slots : MonoBehaviour, IDropHandler {
	// Referências aos blocos que podem ser movidos
	public GameObject b1;
	public GameObject b10;
	public GameObject b100;
	 // Referências aos slots
	public GameObject slot1;
	public GameObject slot10;
	public GameObject slot100;
	// Contadores de blocos movidos para os slots
	public int nblocos1, nblocos10;
	// Transform dos slots
	[SerializeField] Transform slots;

	// Propriedade que retorna o item no slot
	public GameObject item{
		get {
			if (transform.childCount > 0) {
				return transform.GetChild (0).gameObject;
			}
			return null;
		}
	}

	#region IDropHandler implementation
	// Método chamado quando um bloco é solto no slot
	public void OnDrop (PointerEventData eventData) 
    { 
		// Verifica as condições e move os blocos conforme necessário
		if (transform.tag == "0" || transform.childCount == 0) {
			DragHandeler.itemBeingDragged.transform.SetParent (transform);
			ExecuteEvents.ExecuteHierarchy < IHasChanged> (gameObject, null, (x, y) => x.HasChanged ());
		} else if (transform.tag == "lixo") {
			DragHandeler.itemBeingDragged.transform.SetParent (transform);
			ExecuteEvents.ExecuteHierarchy < IHasChanged> (gameObject, null, (x, y) => x.HasChanged ());
			Destroy (DragHandeler.itemBeingDragged);
		} else {
			DragHandeler.itemBeingDragged.transform.position = transform.position;
			ExecuteEvents.ExecuteHierarchy < IHasChanged> (gameObject, null, (x, y) => x.HasChanged ());
		}
		// Verifica se há mais de 10 blocos no slot de 1
		var blocos1 = GameObject.FindGameObjectsWithTag ("1");
		var blocos10 = GameObject.FindGameObjectsWithTag ("10");
		if (blocos1.Length > 10) {
			nblocos1 = 0;
			for (var j = 0; j <= 10; j++) {
				if (blocos1 [j].transform.parent.tag == "0") {
					nblocos1 = nblocos1 + 1;
					if (nblocos1 == 10) {
						for (var i = 0; i <= 10; i++) {
							if (blocos1 [i].transform.parent.tag == "0") {
								Destroy (blocos1 [i]);
							}
						}
						Instantiate (b10, transform);
						Instantiate(b1, DragHandeler.startParent);
					}
				}
			}
		}
		// Verifica se há mais de 10 blocos no slot de 10
		else if(blocos10.Length > 10) {
			nblocos10 = 0;
			for(var j=0; j<=10; j++) 
			{
				if(blocos10[j].transform.parent.tag == "0") {
					nblocos10 = nblocos10 + 1;
					if(nblocos10 == 10){
						for (var i = 0; i <= 10; i++) {
							if (blocos10 [i].transform.parent.tag == "0") {
								Destroy (blocos10 [i]);
							}
						}
						Instantiate (b100, transform);
						Instantiate (b10, DragHandeler.startParent);
					}
				}
			}
		}
	}

	#endregion
}
