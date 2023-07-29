namespace TowerDefense
{
    public enum AiWalkerBehaviour
    {
        /// <summary>
        /// Ничего не делать.
        /// </summary>
        None,

        /// <summary>
        /// Патрулировать по заданному маршруту, вперёд по списку точек.
        /// </summary>
        RoutePatrolOnward,

        /// <summary>
        /// Патрулировать по заданному маршруту, назад по списку точек.
        /// </summary>
        RoutePatrolBackward,

        /// <summary>
        /// Пройти до конца по заданному маршруту, вперёд по списку точек.
        /// </summary>
        RouteJourneylOnward,

        /// <summary>
        /// Пройти до конца по заданному маршруту, назад по списку точек.
        /// </summary>
        RouteJourneyBackward,
    }
}
