using System;
using AuctionMS.Domain.Entities;

namespace AuctionMS.Domain.Exceptions
{
    public class InvalidAuctionStateTransitionException : Exception
    {
        public AuctionState CurrentState { get; }
        public AuctionState NewState { get; }

        public InvalidAuctionStateTransitionException(AuctionState currentState, AuctionState newState)
            : base($"Transición de estado no válida: no se puede cambiar de {currentState} a {newState}")
        {
            CurrentState = currentState;
            NewState = newState;
        }
    }
}
