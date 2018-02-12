using UnityEngine;
using DG.Tweening;
using System.Collections.Generic;

[System.Serializable]
public class BattleTransition
{
    public float Distort;
    public Texture2D TransitionTexture;
}

public class BattleSceneTransition : MonoBehaviour
{
    [SerializeField] private float _transitionDuration = 2.5f;

    [SerializeField] private int _whichTransition;

    [SerializeField] private List<BattleTransition> _transitions;

    [SerializeField] private Material _transitionMaterial;

    

    private void OnEnable()
    {
        StartTransition(_whichTransition);
    }

    private void StartTransition(int textureIndex = 0)
    {
        _transitionMaterial.SetFloat("_Cutoff", 0f);
        _transitionMaterial.SetTexture("_TransitionTex", _transitions[textureIndex].TransitionTexture);
        _transitionMaterial.SetFloat("_Distort", _transitions[textureIndex].Distort);
        _transitionMaterial.DOFloat(1f, "_Cutoff", _transitionDuration).onComplete += ()=> ReverseTransition(textureIndex);

        //ReverseTransition(textureIndex);
    }

    private void ReverseTransition(int textureIndex = 0)
    {
        _transitionMaterial.SetFloat("_Cutoff", 1f);
        _transitionMaterial.SetTexture("_TransitionTex", _transitions[textureIndex].TransitionTexture);
        _transitionMaterial.SetFloat("_Distort", _transitions[textureIndex].Distort);
        _transitionMaterial.DOFloat(0f, "_Cutoff", _transitionDuration);
    }

    void OnRenderImage(RenderTexture src, RenderTexture dst)
    {
        if (_transitionMaterial != null)
            Graphics.Blit(src, dst, _transitionMaterial);
    }

    private void OnDisable()
    {
        _transitionMaterial.SetFloat("_Cutoff", 0f);
    }
}
