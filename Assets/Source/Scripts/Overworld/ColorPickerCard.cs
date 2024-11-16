using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ColorPickerCard : DraggableCard
{

    public override void OnEndDrag(PointerEventData eventData)
    {
        StartCoroutine(TakeSnapShot());
    }

    private IEnumerator TakeSnapShot()
    {
        yield return new WaitForEndOfFrame();
        Rect viewRect = Camera.main.pixelRect;
        Texture2D tex = new Texture2D((int)viewRect.width, (int)viewRect.height, (true ? TextureFormat.ARGB32 : TextureFormat.RGB24), false);
        tex.ReadPixels(viewRect, 0, 0, false);
        tex.Apply(false);

        Vector3 texturePosition = Camera.main.ScreenToViewportPoint(transform.position);


        Color color = tex.GetPixelBilinear(texturePosition.x, texturePosition.y);
        transform.SetParent(_grid);

        GetComponent<Image>().color = color;
        Card card = FindObjectOfType<ColorPickerDeck>().ClosestCardToColor(color);
        FindObjectOfType<DeckUI>().AddCardToGrid(card);
        FullDeck.Instance.Cards.Add(card);
        FullDeck.Instance.Cards.Remove(_card);
        Destroy(gameObject);
    }
}
