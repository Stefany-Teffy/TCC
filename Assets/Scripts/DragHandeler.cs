using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

// ararstar e soltar
public class DragHandeler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler {

	public static GameObject itemBeingDragged;
	public static int valor;
	public Vector3 startPosition;
	public static Transform startParent;
	public GameObject b1;
	public GameObject b10;
	public GameObject b100;

	#region IBeginDragHandler implementation

	public void OnBeginDrag (PointerEventData eventData)
	{
		itemBeingDragged = gameObject;
		startPosition = transform.position;
		startParent = transform.parent;
		//GetComponent<CanvasGroup> ().blocksRaycasts = false;

	}

	#endregion

	#region IDragHandler implementation

	public void OnDrag (PointerEventData eventData)
	{
		transform.position = Input.mousePosition;
	}

	#endregion

	#region IEndDragHandler implementation

	public void OnEndDrag (PointerEventData eventData)
	{

		if (transform.parent.tag == "lixo") {
			
			if (startParent.tag == "1" || startParent.tag == "10" || startParent.tag == "100") {
				Instantiate (transform, startParent);
				transform.name = transform.name.Replace("(Clone)", "");
				transform.name.Trim();
			}
			Destroy (itemBeingDragged);
		}
		if (startParent.tag == "1" && startParent.childCount == 0) {
			Transform blocoNovo = Instantiate(b1, startParent).transform;
			//blocoNovo.name = blocoNovo.name.Replace("(Clone)", "");
			blocoNovo.name.Trim();
		} else if (startParent.tag == "10" && startParent.childCount == 0) {
			Transform blocoNovo = Instantiate(b10, startParent).transform;
			blocoNovo.name = blocoNovo.name.Replace("(Clone)", "");
			blocoNovo.name.Trim();
		} else if (startParent.tag == "100" && startParent.childCount == 0) {
			Transform blocoNovo = Instantiate(b100, startParent).transform;
			blocoNovo.name = blocoNovo.name.Replace("(Clone)", "");
			blocoNovo.name.Trim();
		}
			itemBeingDragged = null;
			GetComponent<CanvasGroup> ().blocksRaycasts = true;
		if (transform.parent == startParent && transform.parent.tag!="lixo") {
				transform.position = startPosition;
			}

		ExecuteEvents.ExecuteHierarchy < IHasChanged> (gameObject, null, (x, y) => x.HasChanged ());



	}

	#endregion
}