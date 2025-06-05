using UnityEngine;

public class AbilityView : MonoBehaviour
{
    private Ability model;
    private ParticleSystem particle;

    void OnDestroy()
    {
        if (model == null)
            return;

        model.OnCastStarted -= CastStarted;
        model.OnCastEnded -= CastStopped;
    }

    public void Initialize(ParticleSystem particlePrefab, Ability model)
    {
        this.model = model;
        particle = Instantiate(particlePrefab.gameObject, transform).GetComponent<ParticleSystem>();

        var particlesMain = particle.main;
        particlesMain.playOnAwake = false;

        model.OnCastStarted += CastStarted;
        model.OnCastEnded += CastStopped;
    }

    private void CastStarted(IUnit caster)
    {
        if (particle != null)
        {
            transform.position = caster.Position;
            gameObject.SetActive(true);
            particle.Play();
        }
    }

    private void CastStopped()
    {
        if (particle != null)
        {
            particle.Stop();
            gameObject.SetActive(false);
        }
    }
}
