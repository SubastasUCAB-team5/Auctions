using System.Collections.Generic;
using AuctionMS.Domain.Entities;

namespace AuctionMS.Domain.ValueObjects
{
    public static class AuctionStateTransitions
    {
        private static readonly Dictionary<AuctionState, List<AuctionState>> ValidTransitions = new()
        {
            { AuctionState.Active, new List<AuctionState> { AuctionState.Ended, AuctionState.Completed, AuctionState.Canceled } },
            { AuctionState.Ended, new List<AuctionState>() },
            { AuctionState.Completed, new List<AuctionState>() },
            { AuctionState.Canceled, new List<AuctionState>() }
        };

        public static bool IsValidTransition(AuctionState currentState, AuctionState newState)
        {
            // No se permite cambiar al mismo estado
            if (currentState == newState)
                return false;

            // Verificar si la transición es válida
            return ValidTransitions.ContainsKey(currentState) && 
                   ValidTransitions[currentState].Contains(newState);
        }

        public static bool IsImmutable(AuctionState state)
        {
            // Las subastas en subasta o vendidos no se pueden modificar
            return state == AuctionState.Ended || state == AuctionState.Completed || state == AuctionState.Canceled;
        }
    }
}
