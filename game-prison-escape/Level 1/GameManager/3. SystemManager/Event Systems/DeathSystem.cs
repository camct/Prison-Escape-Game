using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace ECS_Framework
{
    /// <summary>
    /// System that manages entity death events, triggering actions depending on the entity type.
    /// </summary>
    public class DeathEventSystem : System
    {
        private List<Entity> _entities;

        /// <summary>
        /// Initializes a new instance of the <see cref="DeathEventSystem"/> class.
        /// </summary>
        public DeathEventSystem()
        {
            _entities = new List<Entity>();
        }

        /// <summary>
        /// Adds an entity to the system if it has both a StateComponent and an AnimatedComponent.
        /// </summary>
        /// <param name="entity">The entity to be added.</param>
        public override void AddEntity(Entity entity)
        {
            if (entity.GetComponent<StateComponent>() != null && entity.GetComponent<AnimatedComponent>() != null)
            {
                _entities.Add(entity);
            }
        }

        /// <summary>
        /// Removes an entity from the system.
        /// </summary>
        /// <param name="entity">The entity to be removed.</param>
        public override void RemoveEntity(Entity entity)
        {
            _entities.Remove(entity);
        }

        /// <summary>
        /// Updates the system, checking for entities in a dead state and triggering the appropriate action.
        /// </summary>
        /// <param name="gameTime">The current game time.</param>
        public override void Update(GameTime gameTime)
        {
            for (int i = _entities.Count - 1; i >= 0; i--)
            {
                Entity entity = _entities[i];
                StateComponent stateComponent = entity.GetComponent<StateComponent>();
                AnimatedComponent animatedComponent = entity.GetComponent<AnimatedComponent>();

                if (stateComponent.CurrentSuperState == SuperState.IsDead)
                {
                    ActionAnimation deathAnimation = animatedComponent.GetCurrentAnimation();

                    // Check if the animation has completed
                    if (deathAnimation.IsFinished)
                    {
                        if (entity.GetComponent<EntityTypeComponent>().Type == EntityType.Player)
                        {
                            MessageBus.Publish(new ReloadLevelMessage());
                        }
                        else
                        {
                            // Remove the entity
                            MessageBus.Publish(new DestroyEntityMessage(entity));
                        }
                    }
                }
            }
        }
    }
}