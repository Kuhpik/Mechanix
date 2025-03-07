using UnityEngine;

public class UnitView : MonoBehaviour
{
    [SerializeField] private UnitAnimator animator;

    private IUnit model;

    public void Initialize(IUnit model)
    {
        this.model = model;
        model.OnUpdated += ModelUpdated;
    }

    private void OnDestroy()
    {
        model.OnUpdated -= ModelUpdated;
    }

    private void ModelUpdated()
    {
        animator.Animate(model.State);

        transform.position = model.Position;
        transform.forward = model.MoveDirection;
    }
}
