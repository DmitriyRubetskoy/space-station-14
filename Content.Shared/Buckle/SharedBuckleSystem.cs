using Content.Shared.Buckle.Components;
using Content.Shared.Standing;
using Robust.Shared.GameObjects;
using Robust.Shared.Physics.Dynamics;

namespace Content.Shared.Buckle
{
    public abstract class SharedBuckleSystem : EntitySystem
    {
        public override void Initialize()
        {
            base.Initialize();
            SubscribeLocalEvent<SharedBuckleComponent, PreventCollideEvent>(PreventCollision);
            SubscribeLocalEvent<SharedBuckleComponent, DownAttemptEvent>(HandleDown);
            SubscribeLocalEvent<SharedBuckleComponent, StandAttemptEvent>(HandleStand);
        }

        private void HandleStand(EntityUid uid, SharedBuckleComponent component, StandAttemptEvent args)
        {
            if (component.Buckled)
            {
                args.Cancel();
            }
        }

        private void HandleDown(EntityUid uid, SharedBuckleComponent component, DownAttemptEvent args)
        {
            if (component.Buckled)
            {
                args.Cancel();
            }
        }

        private void PreventCollision(EntityUid uid, SharedBuckleComponent component, PreventCollideEvent args)
        {
            if (args.BodyB.Owner.Uid != component.LastEntityBuckledTo) return;

            component.IsOnStrapEntityThisFrame = true;
            if (component.Buckled || component.DontCollide)
            {
                args.Cancel();
            }
        }
    }
}
