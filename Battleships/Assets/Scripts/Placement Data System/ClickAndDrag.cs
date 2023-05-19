using UnityEngine;

public class ClickAndDrag : MonoBehaviour
{
    [SerializeField] private bool draggable = false; // to stop being able to drag after all pawn placement confirmation.
    private bool dragging = false;
    private Vector3 offset;

    [SerializeField] private PotentialShipPlacement potentialShipPlacement;
    private Pawn currentPawn;
    private SpriteRenderer pawnSP;
    private Vector3 cubeCenter;

    private void Update() {
        if (dragging && draggable) {
            transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition) + offset;
            pawnSP = currentPawn.GetComponent<SpriteRenderer>();

            if (potentialShipPlacement.GetPawnOrientation() == PawnOrientation.HORIZONTAL) {
                pawnSP.sprite = currentPawn.horizontalSprite;
            } else {
                pawnSP.sprite = currentPawn.verticalSprite;
            }
            potentialShipPlacement.SetPawnSize(currentPawn.GetPawnSize());
        }
    }

    private void OnMouseDown() {
        currentPawn = GetComponent<Pawn>();
        offset = transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
        dragging = true;
        Debug.Log("clicked.");
        Debug.Log("Dragging pawn: " + currentPawn.name);
    }
    private void OnMouseUp() {
        dragging = false;
        SnapToCube();
        Debug.Log("Dropped.");
    }
    private void SnapToCube() {
        if (potentialShipPlacement.GetCurrentHighlightedCubeVisual() != null) {
            cubeCenter = potentialShipPlacement.GetCurrentHighlightedCenterPoint();
            currentPawn.transform.position = cubeCenter;
        }
    }
    public Pawn GetCurrentPawn() {
        return currentPawn;
    }
}
