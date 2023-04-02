using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Scripts
{
    public class Table : Subject, IDropHandler
    {

        [SerializeField] private Chairs chairs;
        [SerializeField] private GameObject currentWaitressSprite;

        public bool idle { get; private set; }
        public bool pending { get; private set; }
        public bool serving { get; private set; }

        public Chairs _chairs { get; private set; }

        private GuestsRenderer _guestsRendrer;

        public Waitress currentWaitress { get; private set; }
        public Guests currentGuests { get; private set; }

        private void Awake()
        {
            _chairs = chairs;
            idle = false;
            pending = false;
            serving = false;

            _guestsRendrer = gameObject.GetComponent<GuestsRenderer>();
        }

        private void Start()
        { 
            IdleStart();
        }

        public void IdleStart()
        {
            idle = true;
            NotifyObservers(Actions.TableIdle);
        }

        public void IdleStopped()
        {
            idle = false;
            PendingStart();
        }

        private void PendingStart()
        {
            currentGuests = _guestsRendrer.RenderGuests();
            currentGuests.SetGuestsInCafeStatus();
            pending = true;
            NotifyObservers(Actions.TablePending);
        }

        public void PendingStopped()
        {
            pending = false;
            currentGuests.RemoveGuestsInCafeStatus();
            _guestsRendrer.RemoveGuests();
            IdleStart();
        }

        private void ServingStart(Waitress waitress)
        {
            serving = true;
            currentWaitress = waitress;
            currentWaitress.ChangeStatus(WaitressStatuses.Serving);
            currentWaitress.AssignToTable(this);
            currentWaitressSprite.GetComponent<SpriteRenderer>().sprite = currentWaitress._mainSprite;

            NotifyObservers(Actions.TablePendingStop);
            NotifyObservers(Actions.TableServing);
        }

        public void ServingStopped()
        {
            serving = false;
            currentWaitress.ChangeStatus(WaitressStatuses.Resting);
            currentWaitress.AssignToTable(null);
            currentWaitressSprite.GetComponent<SpriteRenderer>().sprite = null;

            currentGuests.RemoveGuestsInCafeStatus();
            _guestsRendrer.RemoveGuests();

            IdleStart();
        }

        public void OnDrop(PointerEventData eventData)
        {
            if (this.pending)
            {
                pending = false;
                ServingStart(eventData.pointerDrag.GetComponent<WaitressDragAndDrop>().assignedWaitress);
            }
        }

        private void OnMouseOver()
        {
            if(idle == false)
            {
                GuestsOnTableTooltips._instance.ShowTooltip(this);
            }
        }
        private void OnMouseExit()
        {
            GuestsOnTableTooltips._instance.HideTooltip();
        }
    }
}