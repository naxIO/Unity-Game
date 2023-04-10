﻿// HELLO THERE
using System.Reflection;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DropSwapItem : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler
{
	public GUIItemCollector GUIItem;

	public void Start ()
	{
		if (GUIItem == null)
			GUIItem = this.GetComponent<GUIItemCollector> ();
	}

	public void OnDrop (PointerEventData data)
	{
		if (GUIItem == null || GUIItem.Item == null)
			return;
		
		if (GUIItem.currentInventory) {
			GUIItemCollector itemDrop = GetDropSprite (data);
			if (itemDrop != null && itemDrop.Item != null) {
				
				ItemCollector tmp = new ItemCollector ();
				GUIItem.currentInventory.CopyCollector (tmp, GUIItem.Item);
				if ((GUIItem.Type == "Stock" || GUIItem.Type == "Inventory") && itemDrop.Type != "Shop") {
					
					// Is Networking.

					if (GUIItem.currentInventory != itemDrop.currentInventory) {
						// Difference Inventory
						
						if (GUIItem.currentInventory.character && GUIItem.currentInventory.character.IsMine)
						{
							// swap to me
							UnitZ.playerManager.PlayingCharacter.inventory.PutCollector(itemDrop.Item, GUIItem.Item.InventoryIndex);
							UnitZ.playerManager.PlayingCharacter.inventory.PutCollectorSync(tmp, itemDrop.Item.InventoryIndex);
						} else
						{
							// swap to another
							UnitZ.playerManager.PlayingCharacter.inventory.PutCollectorSync(itemDrop.Item, GUIItem.Item.InventoryIndex);
							UnitZ.playerManager.PlayingCharacter.inventory.PutCollector(tmp, itemDrop.Item.InventoryIndex);
						}
						
					} else
					{
						// Same Inventory
						
						if (GUIItem.currentInventory.character && GUIItem.currentInventory.character.IsMine)
						{
							// is mine
							GUIItem.currentInventory.SwarpCollector(GUIItem.Item, itemDrop.Item);
						} else
						{
							// is server
							UnitZ.playerManager.PlayingCharacter.inventory.PutCollectorSync(itemDrop.Item, GUIItem.Item.InventoryIndex);
							UnitZ.playerManager.PlayingCharacter.inventory.PutCollectorSync(tmp, itemDrop.Item.InventoryIndex);
						}
					}
					
				}
			}
		}
	}

	public void OnPointerEnter (PointerEventData data)
	{

	}

	public void OnPointerExit (PointerEventData data)
	{

	}

	private GUIItemCollector GetDropSprite (PointerEventData data)
	{
		var originalObj = data.pointerDrag;

		if (originalObj == null) {
			return null;
		}
		if (originalObj.GetComponent<DragShortcut> ())
			return null;
		
		if (originalObj.GetComponent<GUIItemCollector> ()) {
			return originalObj.GetComponent<GUIItemCollector> ();
		}
		return null;
	}

}