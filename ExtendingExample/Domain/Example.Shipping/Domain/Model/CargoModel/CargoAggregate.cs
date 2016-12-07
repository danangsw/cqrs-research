using EventFlow.Aggregates;
using EventFlow.Extensions;
using Example.General.Extension;
using Example.Shipping.Domain.Model.CargoModel.Entities;
using Example.Shipping.Domain.Model.CargoModel.Events;
using Example.Shipping.Domain.Model.CargoModel.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Example.Shipping.Domain.Model.CargoModel
{
    public class CargoAggregate : AggregateRoot<CargoAggregate, CargoId>
    {
        private readonly CargoState _state = new CargoState();

        public CargoAggregate(CargoId id) : base(id)
        {
            _state.Init();
            Register(_state);
        }

        public Route Route => _state.Route;
        public Itinerary Itinerary => _state.Itinerary;

        public void Book(Route route)
        {
            Specs.AggregateIsNew.ThrowDomainErrorIfNotStatisfied(this);
            Emit(new CargoBookedEvent(route));
        }

        public void SetItinerary(Itinerary itinerary)
        {
            Specs.AggregateIsCreated.ThrowDomainErrorIfNotStatisfied(this);
            Route.Specification().ThrowDomainErrorIfNotStatisfied(itinerary);


            var listTransportLeg = Itinerary.GetTransportLegsNotInCurrentCollectionBasedOnId(itinerary);

            foreach (var transportLeg in listTransportLeg)
            {
                DeleteTransportLeg(transportLeg);
            }


            foreach (var transportLeg in itinerary.TransportLegs)
            {
                if(Itinerary.TransportLegs.Contains(transportLeg, new GenericCompare<TransportLeg>(x => x.Id)))
                {
                    UpdateTransportLeg(transportLeg);
                }else
                {
                    AddTransportLeg(transportLeg);
                }
            }

            Emit(new CargoItinerarySetEvent(itinerary));
        }

        public void AddTransportLeg(TransportLeg transportLeg)
        {
            Emit(new TransportLegAddedEvent(transportLeg));
        }

        public void UpdateTransportLeg(TransportLeg transportLeg)
        {
            Emit(new TransportLegUpdatedEvent(transportLeg));  
        }

        public void DeleteTransportLeg(TransportLeg transportLeg)
        {
            Emit(new TransportLegDeletedEvent(transportLeg));
        }
    }
}
