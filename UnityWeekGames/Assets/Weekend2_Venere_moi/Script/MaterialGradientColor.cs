using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

[RequireComponent(typeof(MeshFilter))]
public class MaterialGradientColor : MonoBehaviour, InterfaceClickOn
{
    Mesh _Mesh;

    [SerializeField] SO_Emotion Emotion;


    [Range(0, 1)] public float Level;
    [SerializeField] Color originalColor;

    float minHeight, maxheight;


    public VisualEffect c_Effect;


    Vector3[] vertices;
    Color[] colorsUvs;

    private void Start()
    {
        _Mesh = GetComponent<MeshFilter>().mesh;
        vertices = new Vector3[_Mesh.vertexCount];

        //Set the max and min value;
        for (int i = 0; i < vertices.Length; i++)
        {
            vertices[i] = new Vector3(_Mesh.vertices[i].x, _Mesh.vertices[i].y, _Mesh.vertices[i].z);
            if (vertices[i].y > maxheight)
                maxheight = vertices[i].y;
            if (vertices[i].y < minHeight)
                minHeight = vertices[i].y;

        }
        if (Emotion.particuleEmote != null)
        {
            
            c_Effect = gameObject.AddComponent<VisualEffect>();
            c_Effect.visualEffectAsset = Emotion.particuleEmote;
        }

    }

    private void Update()
    {
        MeshColorSet();
        _Mesh.colors = colorsUvs;

        if (c_Effect != null)
        {
            c_Effect.SetFloat("SpawnerNumber", Level * 10f);
            c_Effect.SetFloat("MaxSpawnSpeed", (0.5f / Level));

        }

    }

    void MeshColorSet()
    {
        colorsUvs = new Color[vertices.Length];

        for (int i = 0; i < vertices.Length; i++)
        {
            float heigt = Mathf.InverseLerp(minHeight, maxheight, vertices[i].y);

            if (Level >= heigt)
                colorsUvs[i] = Emotion.EmotionColor;
            else
                colorsUvs[i] = originalColor;
        }


    }


    void InterfaceClickOn.Onclick()
    {
        Level += 0.01f;
    }

    void InterfaceClickOn.Onrelease()
    {

    }
    void InterfaceClickOn.CollideCharacter(float powerffect)
    {
         Level += powerffect;
    }
    private void OnCollisionEnter(Collision other) {

         InterfaceClickOn Ivener;

        if (other.collider.TryGetComponent<InterfaceClickOn>(out Ivener))
        {
            Ivener.CollideCharacter(1f);
        }   
    }



}

