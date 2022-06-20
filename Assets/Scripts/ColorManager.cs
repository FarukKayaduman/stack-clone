using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorManager : MonoBehaviour
{

    [SerializeField] private byte minColorValue = 100;
    [SerializeField] private byte maxColorValue = 240;

    [SerializeField] private byte rColor;
    [SerializeField] private byte gColor;
    [SerializeField] private byte bColor;

    [SerializeField] private byte rChange = 20;
    [SerializeField] private byte gChange = 20;
    [SerializeField] private byte bChange = 20;

    private byte selectedRGB;

    // Create an instance of the object
    public static ColorManager m_Instance = null;
    //Singleton
    public static ColorManager Instance
    {
        get
        {
            if (m_Instance == null)
            {
                m_Instance = (ColorManager)FindObjectOfType(typeof(ColorManager));
            }
            return m_Instance;
        }
    }

    public Color RandomColorInRange()
    {
        selectedRGB = (byte)Random.Range(0, 5);

        if (selectedRGB == 0)
        {
            rColor = (byte)Random.Range(minColorValue, maxColorValue);
            rColor -= (byte)(rColor % rChange);

            gColor = minColorValue;
            bColor = maxColorValue;
        }
        else if (selectedRGB == 1)
        {
            rColor = (byte)Random.Range(minColorValue, maxColorValue);
            rColor -= (byte)(rColor % rChange);

            gColor = maxColorValue;
            bColor = minColorValue;
        }
        else if (selectedRGB == 2)
        {
            gColor = (byte)Random.Range(minColorValue, maxColorValue);
            gColor -= (byte)(gColor % gChange);

            rColor = minColorValue;
            bColor = maxColorValue;
        }
        else if (selectedRGB == 3)
        {
            gColor = (byte)Random.Range(minColorValue, maxColorValue);
            gColor -= (byte)(gColor % gChange);

            rColor = maxColorValue;
            bColor = minColorValue;
        }
        else if (selectedRGB == 4)
        {
            bColor = (byte)Random.Range(minColorValue, maxColorValue);
            bColor -= (byte)(bColor % bChange);

            rColor = minColorValue;
            gColor = maxColorValue;
        }
        else // (selectedRGB == 5)
        {
            bColor = (byte)Random.Range(minColorValue, maxColorValue);
            bColor -= (byte)(bColor % bChange);

            rColor = maxColorValue;
            gColor = minColorValue;
        }

        if (rColor == minColorValue)
            rColor += rChange;
        else if (gColor == minColorValue)
            gColor += gChange;
        else if (bColor == minColorValue)
            bColor += bChange;

        return new Color32(rColor, gColor, bColor, 255); ;
    }

    public void UpdatePieceColor(GameObject piece)
    {
        Color32 pieceColor = piece.GetComponent<MeshRenderer>().material.color;

        if (gColor == minColorValue && bColor == maxColorValue)
        {
            Mathf.Clamp(rColor -= rChange, minColorValue, maxColorValue);

            if (rColor == minColorValue)
                gColor += gChange;
        }
        else if (gColor == maxColorValue && bColor == minColorValue)
        {
            Mathf.Clamp(rColor += rChange, minColorValue, maxColorValue);

            if (rColor == maxColorValue)
                gColor -= gChange;
        }
        else if (rColor == minColorValue && bColor == maxColorValue)
        {
            Mathf.Clamp(gColor += gChange, minColorValue, maxColorValue);

            if (gColor == maxColorValue)
                bColor -= bChange;
        }
        else if (rColor == maxColorValue && bColor == minColorValue)
        {
            Mathf.Clamp(gColor -= gChange, minColorValue, maxColorValue);

            if (gColor == minColorValue)
                bColor += bChange;
        }
        else if (rColor == minColorValue && gColor == maxColorValue)
        {
            Mathf.Clamp(bColor -= bChange, minColorValue, maxColorValue);

            if (bColor == minColorValue)
                rColor += rChange;
        }
        else if (rColor == maxColorValue && gColor == minColorValue)
        {
            Mathf.Clamp(bColor += bChange, minColorValue, maxColorValue);

            if (bColor == maxColorValue)
                rColor -= rChange;
        }

        piece.GetComponent<MeshRenderer>().material.color = new Color32(rColor, gColor, bColor, 255);
    }
}
