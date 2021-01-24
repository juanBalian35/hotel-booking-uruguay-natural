using System;

namespace Domain
{
    public class Session
    {
        public Guid Token { get; set; }

        public Administrator Administrator { get; set; }

        public override bool Equals(object obj)
        {
            if (!(obj is Session))
            {
                return false;
            }

            Session session = obj as Session;
            return this.Token == session.Token;
        }
    }
}
