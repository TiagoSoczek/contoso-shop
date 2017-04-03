using System;
using Contoso.Shop.Model.AccessControl;

namespace Contoso.Shop.Model.Shared
{
    public abstract class AuditedEntity : Entity
    {
        public User CreatedBy { get; private set; }
        public int CreatedById { get; private set; }
        public DateTimeOffset CreatedAt { get; private set; }

        public User UpdatedBy { get; private set; }
        public int? UpdatedById { get; private set; }
        public DateTimeOffset? UpdatedAt { get; private set; }

        internal void MarkAsNew(User user)
        {
            if (user == null)
            {
                throw Error.ArgumentNull(nameof(user));
            }

            CreatedAt = Clock.Now;
            CreatedById = user.Id;

            // TODO: User is a detached entity, set here will cause to be inserted on database
            // CreatedBy = user;
        }

        internal void MarkAsUpdated(User user)
        {
            if (user == null)
            {
                throw Error.ArgumentNull(nameof(user));
            }

            UpdatedAt = Clock.Now;
            UpdatedById = user.Id;

            // TODO: User is a detached entity, set here will cause to be inserted on database
            // UpdatedBy = user;
        }
    }
}